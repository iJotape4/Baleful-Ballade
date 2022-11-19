using UnityEngine;
using FMOD.Studio;

namespace Interactables
{
    public abstract class SonoricInteraction : InteractableObject
    {
        protected EventInstance interactionSound;
        protected abstract string eventName { get; }

        public override void Interact()=>
            interactionSound.start();

        protected virtual void Start()=>       
            interactionSound = FMODUnity.RuntimeManager.CreateInstance(eventName);
    }
}
