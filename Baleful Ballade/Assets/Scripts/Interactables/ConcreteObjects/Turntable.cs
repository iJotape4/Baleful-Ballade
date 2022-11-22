using System.Collections;
using UnityEngine;

namespace Interactables
{
    public class Turntable : SonoricInteraction
    {
        protected override string eventName => eventSoundPath;
        [SerializeField] public bool alreadyTouched;

        // Start is called before the first frame update
        protected override void Start()
        {
            base.Start();
            AddOutlineEvents();

            if (levelValidatorScriptableObject.associatedLevel == 1 && Application.isPlaying)
            {
                CallDeactivateInteraction();
                levelValidatorScriptableObject.letterEvent += CallReactivateInteraction;
                levelValidatorScriptableObject.letterEvent += CallHintCounter;
            }
            else
                StartCoroutine(HintCounter());
        }

        public override void Interact()
        {
            interactionSound.start();
            levelValidatorScriptableObject.startPuzzleEvent?.Invoke();
            alreadyTouched = true;
        }

        public IEnumerator HintCounter() 
        {
            while (!alreadyTouched)
            {
                yield return new WaitForSeconds(15f);
                EnableFlashAnimation();
            }
            yield return null;
        }
        // Update is called once per frame

        private void CallHintCounter()=>
            StartCoroutine(HintCounter());
    }
}
