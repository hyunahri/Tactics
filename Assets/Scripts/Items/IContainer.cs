using System;
using System.Collections.Generic;
using System.Linq;
using CoreLib.Complex_Types;
using UnityEngine;

namespace Items
{
    /// <summary>
    /// Imported from space game, might be useful for inventory management,
    /// IContent will probably need some adjustment based on Item.cs
    /// </summary>
  public interface IContainer
    {
        public IReadOnlyDictionary<IContent, int> GetContentDict();
        public bool CheckContentValidForContainer(IContent content);
        public int ContainerTotalSpace { get; }
        public int ContainerRemainingSpace { get; }
        public bool TryAddContent(IContent content, int amount, out int amountNotAdded);
        public void Clear();
    }

    public interface IContent
    {
        public string ContentType { get; } //set up a check for this in IContainer.CheckContentValidForContainer
        public int ContentSize { get; }
    }

    public static class ContainerHelper
    {
        private static float backingField = 0f;
        public static float Property
        {
            get => backingField;
            set
            {
                Debug.Log($"Wow it's {value} now! Used to be {backingField}, that sure was shit.");
                backingField = value;
            }
        }
        
        private static void Foo() => Property = 2f;

        public static void OptimizeContainers(List<IContainer> containers, Func<IContent, float> valueFunc = null)
        {
            DefaultDict<IContent, int> totalContent = new DefaultDict<IContent, int>(() => 0);
            foreach (var container in containers)
            {
                foreach (var pair in container.GetContentDict()) 
                    totalContent[pair.Key] += pair.Value;
                container.Clear();
            }   //Default to size
            valueFunc ??= x => x.ContentSize;
            DistributeContentAmongContainers(totalContent, containers, valueFunc);
        }

        /// <param name="handleLeftoversAction">will only be called if there are actually leftovers.</param>
        /// <param name="containers"></param>
        /// <param name="valueFunc">determines the relative value of the content. Bigger number is higher priority</param>
        /// <param name="content"></param>
        public static void DistributeContentAmongContainers(Dictionary<IContent, int> content, List<IContainer> containers, Func<IContent, float> valueFunc, Action<Dictionary<IContent, int>> handleLeftoversAction = null)
        {
            // Contents to distribute, aggregated from all containers
            int totalItemCount = content.Sum(x => x.Value);
            #if UNITY_EDITOR  //Quick check to see if there's enough space to store all the items
            int totalMass = content.Sum(x => x.Key.ContentSize * x.Value);
            int totalSpace = containers.Sum(x => x.ContainerRemainingSpace);
            if (totalMass > totalSpace) Debug.Log("Not enough space to store all the items, should have leftovers");
            #endif
            
            int smallestItemSize = content.Min(x => x.Key.ContentSize);
            
            //Distribute the items, filling the largest first
            foreach (IContainer container in containers.OrderByDescending(m => m.ContainerTotalSpace))
            { //Get rid of invalid types for this container beforehand so less needs to be checked each iteration
                var validatedTypes = content
                    .Where(x => container.CheckContentValidForContainer(x.Key) && x.Value > 0)
                    .Select(x => x.Key)
                    .OrderBy(valueFunc);
                
                var itemStack = new Stack<IContent>();

                //smallest at the bottom 
                foreach (var entry in validatedTypes)
                    itemStack.Push(entry);

                while (container.ContainerRemainingSpace >= smallestItemSize && itemStack.Any())
                {
                    //Should be the most valuable unchecked item
                    IContent item;
                    do item = itemStack.Pop();
                    while (item.ContentSize > container.ContainerRemainingSpace && itemStack.Any());

                    // If no item fits, break the loop
                    if (item.ContentSize > container.ContainerRemainingSpace) break;

                    //Try to dump the entire amount in at once and see what fits
                    int availableQuantity = content[item];
                    container.TryAddContent(item, availableQuantity , out int amountNotAdded);
                    content[item] = amountNotAdded;
                    totalItemCount -= availableQuantity - amountNotAdded;
                }
                if (totalItemCount == 0) break;
            }

            if (totalItemCount <= 0) return;
            if(handleLeftoversAction == null)
                throw new Exception("Not all items were distributed and no method was provided to deal with leftovers");
            handleLeftoversAction?.Invoke(content);
        }
    }
}