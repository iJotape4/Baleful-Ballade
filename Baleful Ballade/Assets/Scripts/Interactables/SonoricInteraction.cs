using UnityEngine;
using FMOD.Studio;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using System.Collections;

namespace Interactables
{
    [ExecuteAlways, RequireComponent(typeof( Button)), RequireComponent(typeof(Image)), RequireComponent(typeof(Outline)), RequireComponent(typeof(EventTrigger))]
    public abstract class SonoricInteraction : InteractableObject
    {
        protected EventInstance interactionSound;
        protected abstract string eventName { get; }
        [SerializeField] protected string eventSoundPath;
        [SerializeField] protected Sprite sprite;
        [SerializeField] protected Outline outline;
        [SerializeField] protected EventTrigger eventTrigger;
        [SerializeField] protected Image image;
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
            if (levelValidatorScriptableObject == null)
                FindLevelScriptableValidator();            

            GetComponent<Button>().onClick.AddListener(Interact);
            image = GetComponent<Image>();

            outline = GetComponent<Outline>();
            outline.effectColor = Color.white;
            outline.effectDistance = new Vector2(3f, 3f);
            outline.enabled = false;

            eventTrigger = GetComponent<EventTrigger>();

            if (Application.isPlaying)
            {
                interactionSound = FMODUnity.RuntimeManager.CreateInstance(eventName);
                levelValidatorScriptableObject.levelCompleteEvent += CallDeactivateInteraction;
                levelValidatorScriptableObject.melodyFinishedEvent += CallReactivateInteraction;
            }
        }      

        protected virtual void ShowOutline( bool show)=>
            outline.enabled = show;

        protected void FindLevelScriptableValidator()
        {
            string name = SceneManager.GetActiveScene().name;
            string num = name.Substring(name.Length - 1);

            foreach (var x in Resources.FindObjectsOfTypeAll<LevelValidatorScriptableObject>())
            {
               string temp =  x.name.Split(" ", System.StringSplitOptions.None)[1];

                if (temp == num)
                {
                    levelValidatorScriptableObject = x;
                    return;
                }
            }

            Debug.LogWarning(string.Format("Scriptable object is null in {0}", this.gameObject.name));
        }

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

        protected void CallDeactivateInteraction() =>
            image.raycastTarget = false;
        protected void CallReactivateInteraction() =>
            image.raycastTarget = true;
    }
}
