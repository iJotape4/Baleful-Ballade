using FMOD.Studio;
using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interactables
{
    public class Level3Event : MonoBehaviour
    {

        [SerializeField] public LevelValidatorScriptableObject levelValidatorScriptableObject;
        protected EventInstance interactionSound;
        [SerializeField]  protected string eventName;

        public PARAMETER_ID pitchParameterId;
        public EventInstance instance;

        [EventRef]
        public string fmodEvent;
        protected virtual void Start()
        {
            if (Application.isPlaying)
            {
                instance = RuntimeManager.CreateInstance(fmodEvent);
                EventDescription pitchEventDescription;
                instance.getDescription(out pitchEventDescription);
                PARAMETER_DESCRIPTION pitchParameterDescription;
                pitchEventDescription.getParameterDescriptionByName("Flauta", out pitchParameterDescription);
                pitchParameterId = pitchParameterDescription.id;
                // levelValidatorScriptableObject.levelCompleteEvent += CallDeactivateInteraction;
                levelValidatorScriptableObject.levelCompleteEvent += StartFinalEvent;
                DontDestroyOnLoad(this.gameObject);
            }
        }

        void StartFinalEvent()
        {
            instance.start();
        }




    }

}
