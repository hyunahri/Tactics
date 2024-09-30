using System.Collections.Generic;
using CoreLib.Complex_Types;
using Game;
using Items;
using UnityEditor;
using UnityEngine;

namespace World
{
    public class HarvestNode : IWorldInteractable
    {
        //Data
        //The items received on harvest and a range of amounts
        public List<Pair<Item, Range>> HarvestableItems = new List<Pair<Item, Range>>();

        
        //State
        public int DateLastHarvested = -99;
        public bool CanHarvest() => GameState.Current.Day - DateLastHarvested >= 7;
        
        
        //Interaction
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