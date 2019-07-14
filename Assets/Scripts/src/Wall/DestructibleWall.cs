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
        private Animator _animator;

        private void Start()
        {
            _upgradeManager = GameManager.Instance.GetUpgradeManager();
            _animator = GetComponentInChildren<Animator>();
           // _animator.speed = 0;
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
                var instance = Instantiate(upgrade, currentPosition, Quaternion.identity);
                _upgradeManager.RegisterUpgradeAsUnclaimed(instance);
            }
        }

        private void PlayDestroyAnimation()
        {
           // _animator.speed = 10;
        }

        public void onExplosion()
        {
            DebugHelper.LogInfo($"Destructible wall hit by explosion {transform.position}");
            PlayDestroyAnimation();
            SpawnSomething();
            Destroy(gameObject);
        }
    }
}