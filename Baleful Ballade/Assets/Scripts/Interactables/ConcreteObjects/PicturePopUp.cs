using System.Collections;
using UnityEngine;

namespace Interactables
{
    public class PicturePopUp : MonoBehaviour
    {
        [SerializeField] LevelValidatorScriptableObject levelValidatorScriptableObject;
        [SerializeField] GameObject popUpGO;
        [SerializeField] GameObject textPopUp;
        [SerializeField] GameObject button;

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

            yield return new WaitForSeconds(7f);
            button.SetActive(true);

            yield return null;
        }
    }
}
