using System.Collections;
using UnityEngine;

namespace Interactables
{
    public class RightSonoricInteraction : SonoricInteraction
    {
        [SerializeField] protected override string eventName => eventSoundPath;

        protected override void Start()
        {
            base.Start();
            AddOutlineEvents();
            StartCoroutine(Flashes());
        }

        IEnumerator Flashes()
        {
            while (gameObject)
            {
                yield return new WaitForSeconds(40f);
                EnableFlashAnimation();
            }
            yield return null;
        }
    }
}
