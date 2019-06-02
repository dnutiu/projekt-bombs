using System;
using System.Collections.Generic;
using src.Base;
using src.Helpers;
using UnityEngine;
using Random = UnityEngine.Random;

namespace src.Managers
{
    public class LevelManager : GameplayComponent
    {
        public class Count
        {
            public readonly int Min;
            public readonly int Max;

            public Count(int min, int max)
            {
                Min = min;
                Max = max;
            }
        }

        public int columns = 30;
        public int rows = 20;

        /* Specifies how many objects we want per level. */
        public Count destructibleWallCount = new Count(150, 350);
        public Count enemyCount = new Count(20, 50);

        /* Holds the starting position of the player */
        public Transform startPosition;
        public GameObject indestructibleWallPrefab;
        public GameObject destructibleWallPrefab;

        /* Used to group spawned objects */
        public Transform boardHolder;

        /* Holds the available positions */
        private readonly List<Vector3> _gridPositions = new List<Vector3>();

        /* Test only */
        public void Awake()
        {
            InitBoard();
            SetupLevel();
        }

        public void InitBoard()
        {
            _gridPositions.Clear();
            /* We want to iterate over the X axis taking into consideration the startPosition's offset */
            for (var x = startPosition.position.x; x < columns; x++)
            {
                for (var y = startPosition.position.y; y > rows * -1; y--)
                {
                    /* We want the following positions to be a safe zone. */
                    /* Don't place anything on starting position */
                    if (Math.Abs(x) < 0.001 && Math.Abs(y) < 0.001)
                    {
                        continue;
                    }

                    /* Don't place anything on X=1 and Y=0 */
                    if (Math.Abs(x - 1) < 0.001 && Math.Abs(y) < 0.001)
                    {
                        continue;
                    }

                    /* Don't place anything on X=0 and Y=1 */
                    if (Math.Abs(x) < 0.001 && Math.Abs(y - 1) < 0.001)
                    {
                        continue;
                    }

                    /* Place indestructible tiles */
                    if (PlaceIndestructibleTile(x, y))
                    {
                        continue;
                    }

                    /* Add position to _gridPositions */
                    _gridPositions.Add(new Vector3(x, y, 0f));
                }
            }
        }

        public void SetupLevel()
        {
            var random = new Random();
            var numberOfDestructilbeWallsToPlace =
                Mathf.FloorToInt(Random.Range(destructibleWallCount.Min, destructibleWallCount.Max));

            _gridPositions.ShuffleList();
            foreach (var nextPosition in _gridPositions)
            {
                if (numberOfDestructilbeWallsToPlace == 0)
                {
                    break;
                }
                PlaceDestructibleTile(nextPosition);
                numberOfDestructilbeWallsToPlace -= 1;
            }
        }

        private void PlaceDestructibleTile(Vector3 position)
        {
            var instance = Instantiate(destructibleWallPrefab, position, Quaternion.identity);
            instance.transform.SetParent(boardHolder);
        }

        private bool PlaceIndestructibleTile(float x, float y)
        {
            var absX = Mathf.FloorToInt(x);
            var absY = Mathf.FloorToInt(y);

            if (absX % 2 == 0 || absY % 2 == 0) return false;

            var instance =
                Instantiate(indestructibleWallPrefab, new Vector3(x, y, 0f), Quaternion.identity);
            instance.transform.SetParent(boardHolder);
            return true;
        }
    }
}