using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Fade : MonoBehaviour
{
    [SerializeField] LevelValidatorScriptableObject levelValidatorScriptableObject;
    Image fade; 
    private float alpha =0f;
    [SerializeField] float fadeVelocity =0.3f;
    [SerializeField] bool enableFade;

    private void Start()
    {
        fade = GetComponent<Image>();
        fade.color = new Color(0, 0, 0, alpha); 
        levelValidatorScriptableObject.goNextLevelEvent += FadeIn;
    }

    public void FadeIn()=>
        enableFade=true;

    private void Update()
    {
        if (!enableFade)
            return;

        fade.color = new Color(0, 0, 0, alpha);
        alpha += fadeVelocity* Time.deltaTime;

        if (fade.color.a >= 1)
            SceneManager.LoadScene(levelValidatorScriptableObject.nextSceneName, LoadSceneMode.Single);

    }

}
