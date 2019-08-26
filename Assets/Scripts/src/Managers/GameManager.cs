using System.Collections;
using src.Helpers;
using src.Level;
using src.Level.src.Level;
using src.Player;
using UnityEngine;

namespace src.Managers
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager instance;
        
        // Inner Components
        private PlayerController _playerController;
        private GameStateManager _gameStateManager;
        private LevelManager _levelManager;
        private UpgradeManager _upgradeManager;

        public void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else if (instance != null)
            {
                Destroy(gameObject);
            }

            /* Don't destroy when reloading the scene */
            DontDestroyOnLoad(gameObject);

            // Load singletons
            _gameStateManager = gameObject.AddComponent<GameStateManager>();
            _levelManager = gameObject.AddComponent<LevelManager>();
            _upgradeManager = gameObject.AddComponent<UpgradeManager>();

            // Load external components
            _playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        }

        public void Start()
        {
            StartLevel();
        }

        private void StartLevel()
        {
            var levelData = LevelResource.GetLevelData(_gameStateManager.Level);
            _levelManager.SetLevelData(levelData);
            _levelManager.InitBoard();

            StartCoroutine(PreInitGame());
            _upgradeManager.SetLevelData(levelData);
            _levelManager.InitLevel();
            _playerController.Respawn();
        }

        public UpgradeManager GetUpgradeManager()
        {
            return _upgradeManager;
        }
        
        private IEnumerator PreInitGame()
        {
            var preStageUi = Instantiate(PrefabAtlas.PreStageUi); // Will destroy itself.
            preStageUi.SetActive(true);
            yield return new WaitForSeconds(1f);
            Destroy(preStageUi);
        }

        private void Update()
        {
            ListenForMetaKeys();
        }

        private static void ListenForMetaKeys()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                ApplicationActions.QuitGame();
            }
            else if (Input.GetKeyDown(KeyCode.P))
            {
                ApplicationActions.HandlePauseKey();
            }
        }

        public void StartNextLevel()
        {
            _levelManager.DestroyLevel();
            _upgradeManager.DestroyUnclaimedUpgrades();
            _gameStateManager.IncreaseLevel();
            StartLevel();
        }
    }
}