using UnityEngine;

namespace Interactables
{
    public class Turntable : SonoricInteraction
    {
        protected override string eventName => eventSoundPath;

        // Start is called before the first frame update
        protected override void Start()
        {
            base.Start();
            AddOutlineEvents();
            eventSoundPath = levelValidatorScriptableObject.completeMelody;
        }

        public override void Interact()
        {
            base.Interact();
            levelValidatorScriptableObject.startPuzzleEvent?.Invoke();
        }

        // Update is called once per frame


    }
}
