using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "Level (x) Validator", menuName = "ScriptableObjects/LevelValidator", order = 1)]
public class LevelValidatorScriptableObject : ScriptableObject
{
    public UnityAction<string> touchItemAction;
    public UnityAction levelCompleteEvent;
    public UnityAction melodyFinishedEvent;

    [SerializeField] public List<string> soundsList = new List<string>();
    [SerializeField] public string nextSceneName;

    [SerializeField] public string completeMelody;
}
