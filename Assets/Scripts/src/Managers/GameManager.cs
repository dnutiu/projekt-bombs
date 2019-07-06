﻿using src.Helpers;
using UnityEngine;

namespace src.Managers
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;
        private LevelManager _levelManager;
        private UpgradeManager _upgradeManager;
        private BombsUtilManager _bombsUtilManager;

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

        private void Update()
        {
            ListenForMetaKeys();
        }

        private static void ListenForMetaKeys()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Application.Quit();
                ApplicationActions.QuitGame();
            }
            else if (Input.GetKeyDown(KeyCode.P))
            {
                ApplicationActions.HandlePauseKey();
            }
        }
    }
}