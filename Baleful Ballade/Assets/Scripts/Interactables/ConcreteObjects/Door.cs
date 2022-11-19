using FMOD.Studio;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interactables
{
    public class Door : InteractableObject
    {
        [SerializeField] public LevelValidatorScriptableObject levelValidatorScriptableObject;
        protected EventInstance doorSoundOpening;
        protected EventInstance doorSoundDisable;

        protected string doorOpeningEvent ="";
        protected string doorDisabledEvent ="";

        public override void Interact()
        {
            if (activeInteraction)
                GoNextLevel();
            else
                doorSoundDisable.start();
        }

        private void Start()
        {
            levelValidatorScriptableObject.levelCompleteEvent += OpenDoor;
            doorSoundOpening = FMODUnity.RuntimeManager.CreateInstance(doorOpeningEvent);
            doorSoundDisable = FMODUnity.RuntimeManager.CreateInstance(doorDisabledEvent);
        }

        private void OpenDoor()
        {
            doorSoundOpening.start();
            Debug.Log("Level Complete");
            //Animation 
        }

        private void GoNextLevel()
        {

        }
    }
}
