using FMOD.Studio;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interactables
{
    public class Letter : SonoricInteraction
    {
        protected override string eventName => eventSoundPath;
        [SerializeField] protected GameObject messagePopUp;

        // Start is called before the first frame update
        protected override void Start()
        {
            base.Start();
            AddOutlineEvents();
            if(Application.isPlaying)
            messagePopUp.SetActive(false);
        }

        public override void Interact()
        {
            base.Interact();
            levelValidatorScriptableObject.letterEvent?.Invoke();
            flash.SetActive(false);
            messagePopUp.SetActive(true);          
        }

        // Update is called once per frame
        void Update()
        {
            if(Application.isPlaying && flash!=null)
                EnableFlashAnimation();
        }

        // Called By X button on UI message
        public void DestroyLetter()
        {
            EventInstance buttonSond = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/UI/Botones");
            buttonSond.start();
            gameObject.SetActive(false);
        }
    }
}
