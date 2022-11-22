using FMOD.Studio;
using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interactables
{
        public class Flutte : SonoricInteraction
        {
        [Range(0.1f, 1f)] public float transitionVelocity;
        public float transitionIncrease;

        PARAMETER_ID pitchParameterId;
        EventInstance instance;

        protected override string eventName => eventSoundPath;

        protected override void Start()
        {
            base.Start();
            if (Application.isPlaying)
            {
                pitchParameterId = GameObject.FindObjectOfType<Level3Event>().pitchParameterId;
                instance = GameObject.FindObjectOfType<Level3Event>().instance;
            }

        }
        public override void Interact()
        {
            transitionIncrease = transitionVelocity;
            StartCoroutine(CallFlutte()); 
        }

        IEnumerator CallFlutte()
        {
            while (transitionIncrease < 1)
            {
                instance.setParameterByID(pitchParameterId, transitionIncrease);
                yield return new WaitForSeconds(transitionVelocity*10f);
                transitionIncrease += transitionVelocity;
            }
            yield return new WaitForSeconds(3f);
            levelValidatorScriptableObject.levelCompleteEvent?.Invoke();
        }

    }
}
