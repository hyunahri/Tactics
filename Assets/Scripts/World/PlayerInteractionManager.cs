using System;
using System.Collections.Generic;
using System.Linq;
using CoreLib.Complex_Types;
using Events;
using Mono;
using UnityEngine;

namespace World
{
    public static class PlayerInteractionManager
    {
        static PlayerInteractionManager()
        {
            GameEvents.OnPlayerMove.AddListener(SortPendingInteractions);
        }
        
        public static HashSet<object> InteractableSuppressors = new HashSet<object>();
        public static bool SuppressInteractions => InteractableSuppressors.Count > 0;
        
        public static CoreEvent OnPendingInteractionsChanged = new CoreEvent();
        public static List<PendingInteraction> PendingInteractions = new List<PendingInteraction>();
        public static PendingInteraction? PriorityInteraction => PendingInteractions.FirstOrDefault();
        
        
        public static void AddPendingInteraction(IWorldInteractable interactable)
        {
            PendingInteraction interaction = new PendingInteraction(interactable);
            PendingInteractions.Add(interaction);
            SortPendingInteractions();
            interactable.OnPlayerEntersInteractionRange();
        }
        
        public static void RemovePendingInteraction(IWorldInteractable interactable)
        {
            if (PendingInteractions.Remove(new PendingInteraction(interactable)))
            {
                interactable.OnPlayerExitsInteractionRange();
                SortPendingInteractions();
            }
            else
                Debug.LogError($"Failed to remove {interactable.GetName()} from pending interactions");
        }
        
        public static void ClearPendingInteractions()
        {
            PendingInteractions.Clear();
            SortPendingInteractions();
        }

        static void SortPendingInteractions()
        {
            var lastPriority = PriorityInteraction;
            PendingInteractions.Sort(); //sort by distance
            if (lastPriority != PriorityInteraction) 
                lastPriority?.Interactable.SetAsPriorityInteraction(false);
            if (PriorityInteraction != null && !SuppressInteractions) 
                PriorityInteraction.Interactable.SetAsPriorityInteraction(true);
            OnPendingInteractionsChanged.Invoke();
        }
    }

    //Wrapper for interactables
    public record PendingInteraction : IComparable
    {
        public PendingInteraction(IWorldInteractable interactable) => Interactable = interactable;

        public IWorldInteractable Interactable { get; set; }
        public float DistanceToPlayer => Vector3.Distance(Interactable.GetInteractionPosition(), PlayerMarker.Instance.transform.position);
        
        public int CompareTo(object obj)
        {
            if (obj is PendingInteraction other)
                return   DistanceToPlayer.CompareTo(other.DistanceToPlayer);
            return 0;
        }
    }
}