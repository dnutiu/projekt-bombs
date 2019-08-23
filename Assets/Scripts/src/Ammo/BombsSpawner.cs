using System;
using src.Base;
using src.Managers;
using UnityEngine;

namespace src.Ammo
{
    public class BombsSpawner : GameplayComponent
    {
        public GameObject bombPrefab;

        private BombsUtilManager _bombsUtil;

        public void Start()
        {
            _bombsUtil = BombsUtilManager.instance;
        }

        public void PlaceBomb(Transform location)
        {
            var position1 = location.position;
            var absX = Mathf.RoundToInt(position1.x);
            var absY = Mathf.RoundToInt(position1.y);
            var position = new Vector2(absX, absY);
            if (_bombsUtil.CanPlaceBomb(position))
            {
                Instantiate(bombPrefab, position, Quaternion.identity);
                _bombsUtil.PlaceBomb(position);
            }
        }
    }
}
