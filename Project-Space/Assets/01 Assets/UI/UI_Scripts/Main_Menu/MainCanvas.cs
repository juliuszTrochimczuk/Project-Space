using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI.MainMenu
{
    public class MainCanvas : MonoBehaviour
    {
        [SerializeField] private GameObject openingMenu;
        [SerializeField] private GameObject settingsMenu;
        [SerializeField] private GameObject audioMenu;
        [SerializeField] private GameObject graphicsMenu;
        [SerializeField] private GameObject keybindsMenu;

        private void Awake()
        {
            openingMenu.SetActive(true);
            settingsMenu.SetActive(false);
            audioMenu.SetActive(false);
            graphicsMenu.SetActive(false);
            keybindsMenu.SetActive(false);
        }

        public void ChangeTargetVisibility(GameObject targetMenu) => targetMenu.SetActive(!targetMenu.activeInHierarchy);

        public void ChangeSelfVisibility(GameObject self) => self.SetActive(!self.activeInHierarchy);

        public void LoadScene(string targetScene) => SceneManager.LoadScene(targetScene);

        public void QuitGame() => Application.Quit();
    }
}