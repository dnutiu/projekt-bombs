using UnityEngine;

using src.Base;

namespace src.Wall
{
    public class DestructibleWall : GameplayComponent, IExplosable
    {
        private bool _spawnExit;
        private bool _spawnUpgrade;

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

        public void OnDestroy()
        {
            if (_spawnExit)
            {
                // TODO Spawn an exit
            }
            else if (_spawnUpgrade)
            {
                // TODO Spawn an upgrade, use composition to UpgradeManager
                // to get random / desired upgrade
            }
        }

        public void onExplosion()
        {
            Debug.Log("Destructible wall hitted by explosion");
        }
    }
}