using UnityEngine;

namespace src.Helpers
{
    public static class ApplicationActions
    {
        public static bool IsGamePaused { get; private set; }

        public static void QuitGame()
        {
            Application.Quit();
        }

        public static void PauseGame()
        {
            IsGamePaused = true;
            Time.timeScale = 0f;
        }

        public static void UnpauseGame()
        {
            IsGamePaused = false;
            Time.timeScale = 1f; 
        }

        public static void HandlePauseKey()
        {
            if (IsGamePaused)
            {
                UnpauseGame();
            }
            else
            {
                PauseGame();
            }
        }
    }
}