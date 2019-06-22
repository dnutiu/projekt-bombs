using System;
using UnityEngine;

using src.Base;
using src.Helpers;
using src.Managers;

namespace src.Wall
{
    public class DestructibleWall : GameplayComponent, IExplosable
    {
        private bool _spawnExit;
        private bool _spawnUpgrade;
        private UpgradeManager _upgradeManager;
        public GameObject explosionPrefab;
        public GameObject exitDoorPrefab;

        private void Start()
        {
            _upgradeManager = GameManager.Instance.GetUpgradeManager();
        }

        public void SpawnsExit()
        {
            _spawnExit = true;
        }

        public void SpawnsUpgrade()
        {
            _spawnUpgrade = true;
        }

        public float XCoordinate => transform.position.x;
        public float YCoordinate => transform.position.y;

        private void BeforeDestroy()
        {
            var currentPosition = transform.position;
            Destroy(GetComponent<SpriteRenderer>());
            Instantiate(explosionPrefab, currentPosition, Quaternion.identity);
            if (_spawnExit)
            {
                DebugHelper.LogInfo($"Destructible spawned exit {transform.position}");
                Instantiate(exitDoorPrefab, currentPosition, Quaternion.identity);
            }
            else if (_spawnUpgrade)
            {
                DebugHelper.LogInfo($"Destructible spawned upgrade {transform.position}");
                var upgrade = _upgradeManager.GetUpgradePrefab();
                Instantiate(upgrade, currentPosition, Quaternion.identity);
            }
        }

        public void onExplosion()
        {
            DebugHelper.LogInfo($"Destructible wall hit by explosion {transform.position}");
            BeforeDestroy();
            Destroy(gameObject);
        }
    }
}