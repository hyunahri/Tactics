using System.Collections.Generic;
using CoreLib.Complex_Types;
using Game;
using Items;
using UnityEditor;
using UnityEngine;
using Sprite = UnityEngine.ProBuilder.Shapes.Sprite;

namespace World
{
    /// <summary>
    /// World interactable that provides items when harvested.
    /// Needs time to reset after being harvested
    /// 
    /// </summary>
    public class HarvestNode : IWorldInteractable
    {
        //Data
        //The items received on harvest and a range of amounts
        public List<Pair<Item, Range>> HarvestableItems = new List<Pair<Item, Range>>();

        
        //State
        public int DateLastHarvested = -99;
        public bool CanHarvest() => GameState.Current.Day - DateLastHarvested >= 7;
        
        
        //Interaction
        public string GetName() => throw new System.NotImplementedException();
        public Sprite GetInteractionIcon() => throw new System.NotImplementedException();

        public bool CanInteract() => CanHarvest();
        public void Interact()
        {
            if (!CanHarvest())
                return;
            DateLastHarvested = GameState.Current.Day;
            //todo when inventory is added
        }
    }
}