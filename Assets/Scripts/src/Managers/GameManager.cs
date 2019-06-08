using UnityEngine;

namespace src.Managers
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance = null;
        private LevelManager _levelManager;

        public void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else if (Instance != null)
            {
                Destroy(gameObject);
            }

            /* Don't destroy when reloading the scene */
            DontDestroyOnLoad(gameObject);

            _levelManager = GetComponent<LevelManager>();

            InitGame();
        }

        private void InitGame()
        {
            _levelManager.InitLevel();
        }
    }
}