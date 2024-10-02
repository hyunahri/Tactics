using System;
using System.Collections.Generic;
using CoreLib.Complex_Types;
using UnityEngine;
using World;

namespace Monitoring
{
    public class InteractionMonitor : MonoBehaviour
    {
        
        public string Priority;
        [SerializeField]public List<Pair<string,int>> PendingInteractions = new List<Pair<string, int>>();


        private void Start()
        {
            PlayerInteractionManager.OnPendingInteractionsChanged.AddListener(OnChange);
        }

        public void OnChange()
        {
            Priority = PlayerInteractionManager.PriorityInteraction?.Interactable.GetName();
            PendingInteractions.Clear();
            foreach (var interaction in PlayerInteractionManager.PendingInteractions)
            {
                PendingInteractions.Add(new Pair<string, int>(interaction.Interactable.GetName(), (int)interaction.DistanceToPlayer));
            }            
            
            PendingInteractions.Sort((a, b) => a.Item2.CompareTo(b.Item2));
        }
    }
}