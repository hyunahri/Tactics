using System;
using System.Collections.Generic;
using CoreLib;
using CoreLib.Complex_Types;
using CoreLib.Events;
using Game;
using Items;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using Range = CoreLib.Complex_Types.Range;
using Sprite = UnityEngine.ProBuilder.Shapes.Sprite;

namespace World
{
    /// <summary>
    /// World interactable that provides items when harvested.
    /// Needs time to reset after being harvested
    /// 
    /// </summary>
    public class HarvestNode : MonoBehaviour, IWorldInteractable
    {
        
        [Header("Data")]
     
        public string Name;
        //The items received on harvest and a range of amounts
        public List<Pair<ItemTemplate, int2>> HarvestableItems = new ();

        
        [Header("State")]
        public int DateLastHarvested = -99;
        public bool CanHarvest() => GameState.Current.Day - DateLastHarvested >= 7;
        
        
        [Header("Interaction")]
        public UnityEvent<bool> OnSetAsPriorityInteraction = new UnityEvent<bool>();
        public UnityEvent<bool> OnCanInteractChanged = new UnityEvent<bool>();
        public string GetName() => String.IsNullOrEmpty(Name) ? "Harvest Node" : Name;
        public Sprite GetInteractionIcon() => throw new System.NotImplementedException();
        
        private bool lastCanInteract = false;
        public bool CanInteract() => CanHarvest();
        public void Interact()
        {
            if (!CanHarvest())
                return;
            
            DateLastHarvested = GameState.Current.Day;
            foreach (var item in HarvestableItems)
            {
                var amount = RNG.rng.Next(item.Item2.x, item.Item2.y);
                for (int i = 0; i < amount; i++)
                    GameState.Current.Player.Inventory.AddItem(item.Item1.ToInstance() as Item);
            }
        }
        public void SetAsPriorityInteraction(bool isPriority) => OnSetAsPriorityInteraction.Invoke(isPriority);
        public Vector3 GetInteractionPosition() => transform.position;
        public void OnPlayerEntersInteractionRange()
        {
        }

        public void OnPlayerExitsInteractionRange()
        {
        }

        private void OnDay()
        {
            if (lastCanInteract == CanInteract()) return;
            lastCanInteract = CanInteract();
            OnCanInteractChanged.Invoke(lastCanInteract);
        }
        
        
        //
        private void OnEnable()
        {
            StaticEvents_Time.OnDay.AddListener(OnDay);
            SetAsPriorityInteraction(false);
        }
        
        private void OnDisable()
        {
            StaticEvents_Time.OnDay.RemoveListener(OnDay);
        }
    }
}