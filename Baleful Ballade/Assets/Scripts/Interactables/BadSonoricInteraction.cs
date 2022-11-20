using UnityEngine;

namespace Interactables
{
    public class BadSonoricInteraction : SonoricInteraction
    {
        [SerializeField] protected override string eventName => "event:/UI/Cancel";
    }
}
