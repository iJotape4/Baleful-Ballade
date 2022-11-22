using FMOD.Studio;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Mainmenu
{
    public class MainMenuManager : MonoBehaviour
{
        [SerializeField] GameObject settingsPanel;
         protected EventInstance buttonSond = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/UI/Botones");

        public enum SceneNames
        {
            MainMenu, Credits, Dungeon
        }

        public void Awake()=>       
            settingsPanel.SetActive(false);
        

        public void PlayGame()
        {
            buttonSond.start();
            SceneManager.LoadScene($"Level{LevelManager.CurrentLevel}", LoadSceneMode.Single);
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
        
    }
}
