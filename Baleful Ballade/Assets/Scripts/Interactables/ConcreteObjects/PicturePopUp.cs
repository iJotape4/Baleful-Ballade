using System.Collections;
using TMPro;
using UnityEngine;

namespace Interactables
{
    public class PicturePopUp : MonoBehaviour
    {
        [SerializeField] LevelValidatorScriptableObject levelValidatorScriptableObject;
        [SerializeField] GameObject popUpGO;
        [SerializeField] GameObject textPopUp;
        [SerializeField] GameObject button;
        [SerializeField] float delay;
        public string fullText;
        public string currentText ="";

        private void Start()
        {
            levelValidatorScriptableObject.pictureEvent += ShowPicture;
            HideAll();
        }

        void ShowPicture()=>        
            StartCoroutine(ContemplativeMoment());
        
        //Is called by the close button too
        public void SwitchPictureVisibility(bool show) =>
            popUpGO.SetActive(show);
        
        public void HideAll()
        {
            button.SetActive(false);
            textPopUp.SetActive(false);
            SwitchPictureVisibility(false);
        }

        public IEnumerator ContemplativeMoment()
        {
            SwitchPictureVisibility(true);
            yield return new WaitForSeconds(7f);
            textPopUp.SetActive(true);
            for(int i=0; i < fullText.Length; i++)
            {
                currentText = fullText.Substring(0, i);
                textPopUp.GetComponentInChildren<TextMeshProUGUI>().text = currentText;
                yield return new WaitForSeconds(delay);
            }
            button.SetActive(true);

            yield return null;
        }
    }
}
