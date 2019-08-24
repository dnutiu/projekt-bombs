using System.Collections.Generic;
using src.Base;
using UnityEngine;

namespace src.Managers
{
    public sealed class BombsUtilManager : GameplayComponent
    {
        public static BombsUtilManager instance;
        
        public int power = 3;
        public int allowedBombs = 2;
        public int placedBombs = 0;
        public float timer = 3.0f;
        public float explosionDuration = 0.55f;

        
        private readonly HashSet<Vector3> _usedPosition = new HashSet<Vector3>();
        private const int MaxPower = 7;
        private const int MaxAllowedBombs = 10;
        
        public void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else if (instance != null)
            {
                Destroy(gameObject);
            }
        }

        public void IncreasePower()
        {
            if (power <= MaxPower)
            {
                power++;
            }
        }

        public void IncreaseAllowedBombs()
        {
            if (allowedBombs <= MaxAllowedBombs)
            {
                allowedBombs++;
            }
        }

        public void RegisterBomb(Vector3 position)
        {
            if (CanPlaceBomb(position))
            {
                placedBombs++;
                _usedPosition.Add(position);
            }
        }

        public void UnregisterBomb(Vector3 position)
        {
            if (_usedPosition.Contains(position))
            {
                placedBombs--;
                _usedPosition.Remove(position);
            }
        }

        public bool CanPlaceBomb(Vector3 position)
        {
            return (!_usedPosition.Contains(position) && (placedBombs < allowedBombs));
        }
    }
}