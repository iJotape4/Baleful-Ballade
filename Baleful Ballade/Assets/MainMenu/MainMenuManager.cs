using UnityEngine;
using UnityEngine.SceneManagement;

namespace Mainmenu
{
    public class MainMenuManager : MonoBehaviour
{
        [SerializeField] GameObject settingsPanel;


        public enum SceneNames
        {
            MainMenu, Credits, Dungeon
        }

        public void Awake()=>       
            settingsPanel.SetActive(false);
        

        public void PlayGame()=>       
           SceneManager.LoadScene($"Level{LevelManager.CurrentLevel}", LoadSceneMode.Single);
        

        public void Options()=>
        
            settingsPanel.SetActive(settingsPanel.activeSelf ? false : true);
        

        public void Credits()=>       
            SceneManager.LoadSceneAsync(((int)SceneNames.Credits), LoadSceneMode.Single);
        
        public void Quit()=>    
            Application.Quit();
        
    }
}
