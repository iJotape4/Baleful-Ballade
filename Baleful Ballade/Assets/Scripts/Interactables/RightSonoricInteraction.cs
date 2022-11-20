using UnityEngine;

namespace Interactables
{
    public class RightSonoricInteraction : SonoricInteraction
    {
        [SerializeField] protected override string eventName => "event:/UI/Okay";
        [SerializeField] public int melodyPartIndex;

    }
}
