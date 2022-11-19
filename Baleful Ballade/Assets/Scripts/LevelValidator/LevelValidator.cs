using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelValidator : MonoBehaviour
{
   [SerializeField] LevelValidatorScriptableObject levelValidatorScriptableObject;

    [SerializeField] List<string> melodies = new List<string>();

    private void Start()=>    
        levelValidatorScriptableObject.touchItemAction += ListenMelodiesParts;


    public void ListenMelodiesParts(string melody)
    {

        melodies.Add(melody);
        CheckMelodiesList();
    }

    public void CheckMelodiesList()
    {
       for(int i=0; i<  melodies.Count; i++)
       {
            if (melodies[i] != levelValidatorScriptableObject.soundsList[i])
            {
                melodies.Clear();
                return;
            }
       }
       if(melodies.Count == levelValidatorScriptableObject.soundsList.Count)
        levelValidatorScriptableObject.levelCompleteEvent?.Invoke();
    }

}
