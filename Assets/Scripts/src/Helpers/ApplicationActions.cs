using src.Managers;
using UnityEngine;

namespace src.Helpers
{
    public static class ApplicationActions
    {
        private static GameStateManager _gameStateManager = GameStateManager.Instance;
        
        public static void QuitGame()
        {
            Application.Quit();
        }

        public static void PauseGame()
        {
            _gameStateManager.IsGamePaused = true;
            Time.timeScale = 0f;
        }

        public static void UnpauseGame()
        {
            _gameStateManager.IsGamePaused = false;
            Time.timeScale = 1f; 
        }

        public static void HandlePauseKey()
        {
            if (_gameStateManager.IsGamePaused)
            {
                UnpauseGame();
            }
            else
            {
                PauseGame();
            }
        }

        public static void ForbidPlayerMovement()
        {
            _gameStateManager.IsPlayerMovementForbidden = true;
        }

        public static void AllowPlayerMovement()
        {
            _gameStateManager.IsPlayerMovementForbidden = false;
        }
    }
}