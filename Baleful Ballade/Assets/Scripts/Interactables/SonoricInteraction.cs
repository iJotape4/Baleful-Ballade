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
        [SerializeField] protected GameObject flashSprite;
        [SerializeField] protected GameObject flash;
        [SerializeField] protected Outline outline;
        [SerializeField] protected EventTrigger eventTrigger;
        [SerializeField] protected Button button;
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

            button = GetComponent<Button>();
            button.onClick.AddListener(Interact);

            outline = GetComponent<Outline>();
            outline.effectColor = Color.yellow;
            outline.effectDistance = new Vector2(0.3f, 0.3f);
            outline.enabled = false;

            eventTrigger = GetComponent<EventTrigger>();

            flashSprite = Resources.Load<GameObject>("Flash");


            if (Application.isPlaying)
            {
                interactionSound = FMODUnity.RuntimeManager.CreateInstance(eventName);
               // levelValidatorScriptableObject.levelCompleteEvent += CallDeactivateInteraction;
                levelValidatorScriptableObject.melodyFinishedEvent += CallReactivateInteraction;

                flash = Instantiate(flashSprite, transform);              
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
            button.enabled = false;
        protected void CallReactivateInteraction() =>
            button.enabled = true;

        protected void EnableFlashAnimation()=>
            flash.GetComponent<Animator>().SetTrigger("Turn");  
    }
}
