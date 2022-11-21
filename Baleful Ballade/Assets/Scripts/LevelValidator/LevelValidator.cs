using FMOD.Studio;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelValidator : MonoBehaviour
{
   [SerializeField] LevelValidatorScriptableObject levelValidatorScriptableObject;

    [SerializeField] List<string> melodies = new List<string>();
    [SerializeField] bool levelStarted =false;
    [SerializeField] bool levelFinished;

    protected EventInstance fullMelodySound; 

    private void Start()
    {
        levelValidatorScriptableObject.touchItemAction += ListenMelodiesParts;
        //fullMelodySound = FMODUnity.RuntimeManager.CreateInstance(levelValidatorScriptableObject.completeMelody);
        //levelValidatorScriptableObject.levelCompleteEvent += ReproduceCompleteMelody;

        if (levelValidatorScriptableObject.associatedLevel != 4)
            levelValidatorScriptableObject.startPuzzleEvent += StartPuzzle;
        else
            StartPuzzle();
    }    


    public void ListenMelodiesParts(string melody)
    {
         if(melodies.Count< levelValidatorScriptableObject.soundsList.Count && levelStarted)
            melodies.Add(melody);

         if(!levelFinished)
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
        {
            levelValidatorScriptableObject.levelCompleteEvent?.Invoke();
            levelFinished = true;
            if (levelValidatorScriptableObject.associatedLevel == 3)
                levelValidatorScriptableObject.pictureEvent?.Invoke();
        }
    }

    public void ReproduceCompleteMelody()=>
            StartCoroutine(ActivateInteractionsOnMelodyEnd());

    public void StartPuzzle() =>
        levelStarted = true;

    public IEnumerator ActivateInteractionsOnMelodyEnd()
    {
        yield return new WaitForSeconds(2f);
        fullMelodySound.start();
        PLAYBACK_STATE pS =PLAYBACK_STATE.PLAYING;

        while(pS != PLAYBACK_STATE.STOPPED)
        {
            yield return new WaitForSeconds(0.1f);
            fullMelodySound.getPlaybackState(out pS);
        }      

        levelValidatorScriptableObject.melodyFinishedEvent?.Invoke();
        
    }
}


