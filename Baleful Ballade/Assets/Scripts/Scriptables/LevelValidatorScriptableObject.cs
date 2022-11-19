using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "Level (x) Validator", menuName = "ScriptableObjects/LevelValidator", order = 1)]
public class LevelValidatorScriptableObject : ScriptableObject
{
    public UnityAction<string> touchItemAction;
    public UnityAction levelCompleteEvent;

    [SerializeField] public List<string> soundsList = new List<string>();
}
