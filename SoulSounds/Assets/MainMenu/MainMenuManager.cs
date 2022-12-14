using FMOD.Studio;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Mainmenu
{
    public class MainMenuManager : MonoBehaviour
{
        [SerializeField] GameObject settingsPanel;
        protected EventInstance buttonSond;

        [SerializeField] Image fade;
        private float alpha = 0f;
        [SerializeField] float fadeVelocity = 0.3f;
        [SerializeField] bool enableFade;
        [SerializeField] bool startedCoroutine;
        
        [SerializeField] TextMeshProUGUI textPopUp;
        [SerializeField] float delay;
        public string fullText;
        public string currentText = "";

        [SerializeField] GameObject music;
        protected EventInstance introSound;

        public enum SceneNames
        {
            MainMenu, Credits, Dungeon
        }

        public void Awake()
        {
            buttonSond =FMODUnity.RuntimeManager.CreateInstance("event:/SFX/UI/Botones");
            introSound = FMODUnity.RuntimeManager.CreateInstance("event:/Musica/TextoIntro");
            settingsPanel.SetActive(false);
        }       
        

        public void PlayGame()
        {
            buttonSond.start();
            enableFade=true;
           
        }       
        

        public void Options()
        {
            buttonSond.start();
            settingsPanel.SetActive(settingsPanel.activeSelf ? false : true);
        }       
        

        public void Credits()
        {
            buttonSond.start();
            SceneManager.LoadSceneAsync(((int)SceneNames.Credits), LoadSceneMode.Single);
        }      
        
        public void Quit()
        {
            buttonSond.start();
            Application.Quit();
        }


        private void Update()
        {
            if (!enableFade  || startedCoroutine)
                return;

            fade.color = new Color(0, 0, 0, alpha);
            alpha += fadeVelocity * Time.deltaTime;

            if (fade.color.a >= 1)
                StartCoroutine(History());

        }

        public IEnumerator History()
        {
            Destroy(music);
            introSound.start();

            startedCoroutine = true;
            textPopUp.enabled=true;
            for (int i = 0; i < fullText.Length; i++)
            {
                currentText = fullText.Substring(0, i);
                textPopUp.GetComponentInChildren<TextMeshProUGUI>().text = currentText;
                yield return new WaitForSeconds(delay);
            }
            yield return new WaitForSeconds(2f);
            introSound.stop(STOP_MODE.ALLOWFADEOUT);
            SceneManager.LoadScene($"Level{LevelManager.CurrentLevel}", LoadSceneMode.Single);
        }

    }
}
