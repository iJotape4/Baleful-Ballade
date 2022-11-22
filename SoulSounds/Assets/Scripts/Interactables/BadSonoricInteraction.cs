using UnityEngine;

namespace Interactables
{
    public class BadSonoricInteraction : SonoricInteraction
    {
        [SerializeField] protected override string eventName => eventSoundPath;
        protected override void Start()
        {
            base.Start();
            AddOutlineEvents();
        }
    }
}
