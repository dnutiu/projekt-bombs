using System.Collections;
using src.Helpers;
using src.Player;
using UnityEngine;

namespace src.Managers
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;
        private LevelManager _levelManager;
        private UpgradeManager _upgradeManager;
        private BombsUtilManager _bombsUtilManager;
        
        // External Components
        public GameObject preStageUiPrefab;
        private PlayerController _playerController;
        private readonly GameStateManager _gameStateManager = GameStateManager.Instance;

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

            // Load inner components
            _levelManager = GetComponent<LevelManager>();
            _upgradeManager = GetComponent<UpgradeManager>();
            _bombsUtilManager = BombsUtilManager.Instance;

            // Load external components
            _playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        }

        public void Start()
        {
            _levelManager.InitBoard();
            StartLevel();
        }

        private void StartLevel()
        {
            _playerController.Respawn();
            StartCoroutine(PreInitGame());
            _levelManager.InitLevel();
        }

        public UpgradeManager GetUpgradeManager()
        {
            return _upgradeManager;
        }

        public BombsUtilManager GetBombsUtilManager()
        {
            return _bombsUtilManager;
        }
        
        private IEnumerator PreInitGame()
        {
            var preStageUi = Instantiate(preStageUiPrefab); // Will destroy itself.
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