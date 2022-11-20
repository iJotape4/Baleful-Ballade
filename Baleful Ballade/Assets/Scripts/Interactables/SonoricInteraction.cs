using UnityEngine;
using FMOD.Studio;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace Interactables
{
    [ExecuteAlways, RequireComponent(typeof( Button)), RequireComponent(typeof(Image)), RequireComponent(typeof(Outline)), RequireComponent(typeof(EventTrigger))]
    public abstract class SonoricInteraction : InteractableObject
    {
        protected EventInstance interactionSound;
        protected abstract string eventName { get; }

        [SerializeField] protected Sprite sprite;
        [SerializeField] protected Outline outline;
        [SerializeField] protected EventTrigger eventTrigger;
        EventTrigger.Entry entry;
        EventTrigger.Entry exit;

        [SerializeField] public LevelValidatorScriptableObject levelValidatorScriptableObject;

        public override void Interact()
        {
            levelValidatorScriptableObject.touchItemAction?.Invoke(eventName);
            interactionSound.start();
        }

        protected virtual void Start()
        {
            GetComponent<Button>().onClick.AddListener(Interact);

            outline = GetComponent<Outline>();
            outline.effectColor = Color.white;
            outline.effectDistance = new Vector2(3f, 3f);
            outline.enabled = false;

            eventTrigger = GetComponent<EventTrigger>();

            if (Application.isPlaying)
                interactionSound = FMODUnity.RuntimeManager.CreateInstance(eventName);
        }      

        protected virtual void ShowOutline( bool show)=>
            outline.enabled = show;

        protected virtual void AddOutlineEvents()
        {
            if (Application.isPlaying)
            {
                entry = new EventTrigger.Entry();
                entry.eventID = EventTriggerType.PointerEnter;
                entry.callback.AddListener((data) => { ShowOutline(true); });
                eventTrigger.triggers.Add(entry);

                exit = new EventTrigger.Entry();
                exit.eventID = EventTriggerType.PointerExit;
                exit.callback.AddListener((data) => { ShowOutline(false); });
                eventTrigger.triggers.Add(exit);
            }
        }
    }
}
