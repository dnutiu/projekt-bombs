using UnityEngine;

using src.Base;
using src.Helpers;

namespace src.Wall
{
    public class DestructibleWall : GameplayComponent, IExplosable
    {
        private bool _spawnExit;
        private bool _spawnUpgrade;
        public GameObject explosionPrefab;
        public GameObject exitDoorPrefab;

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
                // TODO: Get and instantiate upgrade from manager
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