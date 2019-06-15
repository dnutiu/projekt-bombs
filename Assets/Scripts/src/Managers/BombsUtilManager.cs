using System.Collections.Generic;
using UnityEngine;

namespace src.Managers
{
    public sealed class BombsUtilManager
    {
        private readonly HashSet<Vector3> _usedPosition = new HashSet<Vector3>();

        private const int MaxPower = 7;
        private const int MaxAllowedBombs = 10;

        public int Power { get; private set; } = 3;

        public int AllowedBombs { get; private set; } = 2;

        public int PlacedBombs { get; private set; } = 0;

        public float Timer { get; } = 3.0f;

        public float ExplosionDuration { get; } = 0.55f;

        private BombsUtilManager()
        {
        }

        public static BombsUtilManager Instance { get; } = new BombsUtilManager();

        public void IncreasePower()
        {
            if (Power <= MaxPower)
            {
                Power++;
            }
        }

        public void IncreaseAllowedBombs()
        {
            if(AllowedBombs <= MaxAllowedBombs)
            {
                AllowedBombs++;
            }
        }

        public void PlaceBomb(Vector3 position)
        {
            if(CanPlaceBomb(position))
            {
                PlacedBombs++;
                _usedPosition.Add(position);
            }
        }

        public void RemoveBomb(Vector3 position)
        {
            if(_usedPosition.Contains(position))
            {
                PlacedBombs--;
                _usedPosition.Remove(position);
            }
        }

        public bool CanPlaceBomb(Vector3 position)
        {
            return (!_usedPosition.Contains(position) && (PlacedBombs < AllowedBombs));
        }
    }
}