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
        private GameObject _explosionPrefab;
        private GameObject _exitDoorPrefab;

        private void Start()
        {
            _upgradeManager = GameManager.Instance.GetUpgradeManager();
            _explosionPrefab = PrefabAtlas.BombExplosion;
            _exitDoorPrefab = PrefabAtlas.ExitDoor;
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

        private void SpawnSomething()
        {
            var currentPosition = transform.position;
            Destroy(GetComponent<SpriteRenderer>());
            Instantiate(_explosionPrefab, currentPosition, Quaternion.identity);
            if (_spawnExit)
            {
                DebugHelper.LogInfo($"Destructible spawned exit {transform.position}");
                Instantiate(_exitDoorPrefab, currentPosition, Quaternion.identity);
            }
            else if (_spawnUpgrade)
            {
                DebugHelper.LogInfo($"Destructible spawned upgrade {transform.position}");
                var upgrade = _upgradeManager.GetUpgradePrefab();
                var instance = Instantiate(upgrade, currentPosition, Quaternion.identity);
                _upgradeManager.RegisterUpgradeAsUnclaimed(instance);
            }
        }

        public void OnExplosion()
        {
            DebugHelper.LogInfo($"Destructible wall hit by explosion {transform.position}");
            SpawnSomething();
            Destroy(gameObject);
        }
    }
}