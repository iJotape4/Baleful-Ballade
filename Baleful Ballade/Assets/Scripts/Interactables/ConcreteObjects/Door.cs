using FMOD.Studio;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Interactables
{
    [RequireComponent(typeof(Animator))]
    public class Door : SonoricInteraction
    {
        protected EventInstance doorSoundOpening;
        protected EventInstance doorSoundDisable;

        protected string doorOpeningEvent = "event:/Puzzle_3/Puzzle_3_half_2";
        protected string doorDisabledEvent = "event:/UI/Cancel";

        protected string animTriggerOpenDoor = "OpenDoor";

        protected override string eventName => "event:/Puzzle_3/Puzzle_3_half_2";

        public override void Interact()
        {
            if (activeInteraction)
            {
                interactionSound.start();
                GoNextLevel();

            }
            else
                doorSoundDisable.start();
        }

        protected override void Start()
        {
            base.Start();
            levelValidatorScriptableObject.levelCompleteEvent += OpenDoor;
            doorSoundOpening = FMODUnity.RuntimeManager.CreateInstance(doorOpeningEvent);
            doorSoundDisable = FMODUnity.RuntimeManager.CreateInstance(doorDisabledEvent);
        }

        private void OpenDoor()
        {
            doorSoundOpening.start();
            activeInteraction = true;
            AddOutlineEvents();
;            GetComponent<Animator>().SetTrigger(animTriggerOpenDoor);
            Debug.Log("Level Complete");
            //Animation 
        }

        private void GoNextLevel()=>       
            SceneManager.LoadScene(levelValidatorScriptableObject.nextSceneName, LoadSceneMode.Single);
        
    }
}
