using src.Base;
using src.Helpers;
using UnityEngine.SceneManagement;

namespace src.UI
{
    public class MainMenuActions : GameplayComponent
    {
        public void PlayGame()
        {
            SceneManager.LoadScene("GameScene", LoadSceneMode.Single);
        }

        public void QuitGame()
        {
            ApplicationActions.QuitGame();
        }
    }
}