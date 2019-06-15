using src.Base;
using src.Managers;
using UnityEngine;

namespace src.Ammo
{
    public class BombsSpawner : GameplayComponent
    {
        public GameObject bombPrefab;

        private readonly BombsUtilManager _bombsUtil = BombsUtilManager.Instance;

        public void PlaceBomb(Transform transform)
        {
            var absX = Mathf.RoundToInt(transform.position.x);
            var absY = Mathf.RoundToInt(transform.position.y);
            var position = new Vector2(absX, absY);
            if (_bombsUtil.CanPlaceBomb(position))
            {
                Instantiate(bombPrefab, position, Quaternion.identity);
                _bombsUtil.PlaceBomb(position);
            }
        }
    }
}
