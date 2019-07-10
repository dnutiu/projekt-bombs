using System.Collections;
using src.Helpers;
using UnityEngine;

namespace src.Managers
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;
        private LevelManager _levelManager;
        private UpgradeManager _upgradeManager;
        private BombsUtilManager _bombsUtilManager;
        private GameObject _preStageUi;

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
            _bombsUtilManager = BombsUtilManager.Instance;
            _preStageUi = GameObject.Find("PreStageUI");
        }

        public void Start()
        {
            StartCoroutine(PreInitGame());
            InitGame();
        }

        public UpgradeManager GetUpgradeManager()
        {
            return _upgradeManager;
        }

        public BombsUtilManager GetBombsUtilManager()
        {
            return _bombsUtilManager;
        }

        private void InitGame()
        {
            _levelManager.InitLevel();
        }
        private IEnumerator PreInitGame()
        {
            _preStageUi.SetActive(true);
            yield return new WaitForSeconds(1f);
            _preStageUi.SetActive(false);
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
    }
}