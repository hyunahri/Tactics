using System;
using System.Collections.Generic;
using CoreLib.Complex_Types;
using Febucci.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using World;
using World.NPCS;
using Sprite = UnityEngine.ProBuilder.Shapes.Sprite;

namespace Dialogue
{
    public class Spout : MonoBehaviour, IWorldInteractable
    {
        private Canvas worldCanvas;
        public TextMeshProUGUI textBox;
        public Image portraitImage;
        public TypewriterByCharacter typewriter;
        public GameObject ButtonPrompt;
        
        [Header("Speaker")]
        public string SpeakerName;
        public NPCSpriteContainer SpeakerSprites;
        private bool hasSpeakerSprites => SpeakerSprites != null;
        
        [Header("Settings")] 
        public bool IsProximitySpout = false;


        [Header("State")] 
        public bool IsShowing = false;
        public int lineIndex = 0;
        
        [Header("Dialogue")]
        [SerializeField] private List<Line> dialogueLines;
        public int LineCount => dialogueLines.Count;
        
        
        private void Awake()
        {
            typewriter = textBox.gameObject.GetComponent<TypewriterByCharacter>();
            worldCanvas = textBox.canvas;
            lineIndex = 0;
            Show(false);
        }
        
        public void Show(bool doShow)
        {
            if (doShow && IsShowing)
            {
                return;
            }
            IsShowing = doShow;
            worldCanvas.enabled = doShow;
            RefreshUI();
        }
        
        public void NextLine()
        {
            if(!IsShowing)
                return;
            lineIndex++;
            if (lineIndex >= LineCount)
            {
                ResetProgress();
                Show(false);
            }
            RefreshUI();
        }

        public void ResetProgress()
        {
            lineIndex = 0;
        }

        public void RefreshUI()
        {
            var line = dialogueLines[lineIndex];
            typewriter.ShowText(line.Text);
            if (hasSpeakerSprites)
                portraitImage.sprite = SpeakerSprites.GetSprite(line.Emotion);
            else
                portraitImage.enabled = false;
        }


        
        //IWorldInteractable
        public string GetName()
        {
            return "Spout placeholder";
        }

        public Sprite GetInteractionIcon()
        {
            throw new System.NotImplementedException();
        }

        public bool CanInteract()
        {
            return true;
        }

        public void Interact()
        {
            if (IsShowing) 
            {
                NextLine();
                return;
            }
            ResetProgress();
            Show(true);
        }

        public void SetAsPriorityInteraction(bool isPriority)
        {
           ButtonPrompt.SetActive(isPriority);
           if (IsProximitySpout)
           {
               Show(isPriority);
           }
        }

        public Vector3 GetInteractionPosition()
        {
            return transform.position;
        }

        public void OnPlayerEntersInteractionRange()
        {
            if(IsProximitySpout)
                Show(true);
        }

        public void OnPlayerExitsInteractionRange()
        {
            Show(false);
        }
    }


}