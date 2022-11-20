using UnityEngine;

namespace Interactables
{
    public class ObjectLevel34 : SonoricInteraction
    {
        [SerializeField] protected override string eventName => eventSoundPath;

        protected override void Start()
        {
            base.Start();
            AddOutlineEvents();
        }
    }

}
