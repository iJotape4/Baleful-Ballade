using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interactables
{
    public abstract class InteractableObject : MonoBehaviour, IClickable
    {
        protected bool activeInteraction;
        public void Click()=> 
            Interact();
    
        public abstract void Interact();
    }
}
