using UnityEngine;

namespace Interactables
{
    public class ObjectLevel31 : SonoricInteraction
    {
        [SerializeField] protected override string eventName => eventSoundPath;

        protected override void Start()
        {
            base.Start();
            AddOutlineEvents();
        }
    }

}
