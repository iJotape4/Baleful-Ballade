using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interactables
{
    public class Letter : SonoricInteraction
    {
        protected override string eventName => eventSoundPath;

        // Start is called before the first frame update
        protected override void Start()
        {
            base.Start();
            AddOutlineEvents();
        }

        public override void Interact()
        {
            base.Interact();
            levelValidatorScriptableObject.letterEvent?.Invoke();

            gameObject.SetActive(false);
           
        }

        // Update is called once per frame
        void Update()
        {
            if(Application.isPlaying && flash!=null)
                EnableFlashAnimation();
        }
        
    }
}
