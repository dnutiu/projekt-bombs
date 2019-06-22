using UnityEngine;

namespace src.Managers
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;
        private LevelManager _levelManager;
        private UpgradeManager _upgradeManager;

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
            _upgradeManager = GetComponent<UpgradeManager>();

            InitGame();
        }

        public UpgradeManager GetUpgradeManager()
        {
            return _upgradeManager;
        }

        private void InitGame()
        {
            _levelManager.InitLevel();
        }
    }
}