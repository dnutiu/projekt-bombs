using System.Collections.Generic;
using src.Base;
using src.Helpers;
using src.Wall;
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

            public int RandomIntRange()
            {
                return Mathf.FloorToInt(Random.Range(Min, Max));
            }
        }

        public int columns = 30;
        public int rows = 20;

        /* Specifies how many objects we want per level. */
        public Count destructibleWallCount = new Count(150, 350);
        public Count upgradesCount = new Count(0, 5);
        public Count enemyCount = new Count(20, 50);

        /* Holds the starting position of the player */
        public Transform startPosition;
        public GameObject indestructibleWallPrefab;
        public GameObject destructibleWallPrefab;

        /* Used to group spawned objects */
        public Transform boardHolder;

        /* Holds the available positions */
        private readonly List<Vector3> _freeGridPositions = new List<Vector3>();
        private readonly List<GameObject> _destructibleWalls = new List<GameObject>();

        /* Test only */
        public void Awake()
        {
            InitBoard();
            SetupLevel();
            SetupExit();
            SetupUpgrades();
        }

        private void SetupUpgrades()
        {
            var count = upgradesCount.RandomIntRange();
            for (var i = 0; i < count; i++)
            {
                /* Get the destructible wall script and make it to spawn the upgrade */
                var wall = _destructibleWalls.PopRandom().GetComponent<DestructibleWall>();
                wall.SpawnsUpgrade();
            }
        }

        private void SetupExit()
        {
            /* Get the destructible wall script and make it to spawn the exit */
            var wall = _destructibleWalls.PopRandom().GetComponent<DestructibleWall>();
            wall.SpawnsExit();
        }

        public void InitBoard()
        {
            _freeGridPositions.Clear();
            /* We want to iterate over the X axis taking into consideration the startPosition's offset */
            for (var x = startPosition.position.x; x < columns; x++)
            {
                for (var y = startPosition.position.y; y > rows * -1; y--)
                {
                    /* We want the following positions to be a safe zone. */
                    /* Don't place anything on starting position */
                    if (Mathf.RoundToInt(x) == 0 && Mathf.RoundToInt(y) == 0)
                    {
                        continue;
                    }

                    /* Don't place anything on X=1 and Y=0 */
                    if (Mathf.RoundToInt(x) == 1 && Mathf.RoundToInt(y) == 0)
                    {
                        continue;
                    }

                    /* Don't place anything on X=0 and Y=1 */
                    if (Mathf.RoundToInt(x) == 0 && Mathf.RoundToInt(y) == -1)
                    {
                        continue;
                    }

                    /* Place indestructible tiles */
                    if (PlaceIndestructibleTile(x, y))
                    {
                        continue;
                    }

                    /* Add position to _gridPositions */
                    _freeGridPositions.Add(new Vector3(x, y, 0f));
                }
            }
        }

        public void SetupLevel()
        {
            var random = new Random();
            var numberOfDestructilbeWallsToPlace = destructibleWallCount.RandomIntRange();

            _freeGridPositions.ShuffleList();
            foreach (var nextPosition in _freeGridPositions)
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
            _destructibleWalls.Add(instance);
            instance.transform.SetParent(boardHolder);
        }

        private bool PlaceIndestructibleTile(float x, float y)
        {
            Debug.Log($"PlaceIndestructibleTile: x:{x} y:{y}");
            var absX = Mathf.RoundToInt(x);
            var absY = Mathf.RoundToInt(y);

            if (absX % 2 == 0 || absY % 2 == 0) return false;

            var instance =
                Instantiate(indestructibleWallPrefab, new Vector3(x, y, 0f), Quaternion.identity);
            instance.transform.SetParent(boardHolder);
            return true;
        }
    }
}