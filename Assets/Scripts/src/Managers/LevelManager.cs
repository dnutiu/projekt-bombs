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
            private readonly int _min;
            private readonly int _max;

            public Count(int min, int max)
            {
                _min = min;
                _max = max;
            }

            public int RandomIntRange()
            {
                return Mathf.FloorToInt(Random.Range(_min, _max));
            }
        }

        public Count DestructibleWallCount
        {
            get => _destructibleWallCount;
            set => _destructibleWallCount = value;
        }

        public Count UpgradesCount
        {
            get => _upgradesCount;
            set => _upgradesCount = value;
        }

        public Count EnemyCount
        {
            get => _enemyCount;
            set => _enemyCount = value;
        }

        /* Used to group spawned objects */
        public Transform boardHolder;

        /* Holds the starting position of the player */
        public Transform startPosition;
        public GameObject indestructibleWallPrefab;
        public GameObject[] destructibleWallPrefabs;

        /* Specifies how many objects we want per level. */
        private Count _destructibleWallCount = new Count(150, 350);
        private Count _upgradesCount = new Count(0, 5);
        private Count _enemyCount = new Count(20, 50);

        /* The size of the board. */
        private const int Columns = 30;
        private const int Rows = 20;

        /* Holds the available positions */
        private readonly List<Vector3> _freeGridPositions = new List<Vector3>();
        private readonly List<GameObject> _destructibleWalls = new List<GameObject>();

        private void SetupUpgrades()
        {
            var count = _upgradesCount.RandomIntRange();
            for (var i = 0; i < count; i++)
            {
                if (_destructibleWalls.Count == 0)
                {
                    DebugHelper.LogWarning("No destructible walls left, cannot spawn upgrade.");
                    continue;
                }

                /* Get the destructible wall script and make it to spawn the upgrade */
                var wall = _destructibleWalls.PopRandom().GetComponent<DestructibleWall>();
                DebugHelper.LogInfo($"Spawned upgrade at: x:{wall.XCoordinate} y:{wall.YCoordinate}");
                wall.SpawnsUpgrade();
            }
        }

        private void SetupExit()
        {
            if (_destructibleWalls.Count == 0)
            {
                Debug.LogWarning("No destructible walls found, cannot spawn exit!");
                return;
            }

            /* Get the destructible wall script and make it to spawn the exit */
            var wall = _destructibleWalls.PopRandom().GetComponent<DestructibleWall>();
            DebugHelper.LogInfo($"Spawned exit at: x:{wall.XCoordinate} y:{wall.YCoordinate}");
            wall.SpawnsExit();
        }

        /* Place the indestructible tiles on the board and saves the
         * unused positions in a list. */
        private void InitBoard()
        {
            _freeGridPositions.Clear();
            /* We want to iterate over the X axis taking into consideration the startPosition's offset */
            for (var x = startPosition.position.x; x < Columns; x++)
            {
                for (var y = startPosition.position.y; y > Rows * -1; y--)
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

        /* Randomly places destructible tiles on the level. */
        private void SetupLevelDestructibleWalls()
        {
            var numberOfDestructilbeWallsToPlace = _destructibleWallCount.RandomIntRange();

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
            DebugHelper.LogInfo($"PlaceDestructibleTile: x:{position.x} y:{position.y}");
            var randomWall = destructibleWallPrefabs.ChoseRandom();
            var instance = Instantiate(randomWall, position, Quaternion.identity);
            _destructibleWalls.Add(instance);
            instance.transform.SetParent(boardHolder);
        }

        private bool PlaceIndestructibleTile(float x, float y)
        {
            DebugHelper.LogInfo($"PlaceIndestructibleTile: x:{x} y:{y}");
            var absX = Mathf.RoundToInt(x);
            var absY = Mathf.RoundToInt(y);

            if (absX % 2 == 0 || absY % 2 == 0) return false;

            var instance =
                Instantiate(indestructibleWallPrefab, new Vector3(x, y, 0f), Quaternion.identity);
            instance.transform.SetParent(boardHolder);
            return true;
        }

        /* Initializes the level. */
        public void InitLevel()
        {
            InitBoard();
            SetupLevelDestructibleWalls();
            SetupExit();
            SetupUpgrades();
        }
    }
}