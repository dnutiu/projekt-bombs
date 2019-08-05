using System.Collections.Generic;
using src.Base;
using src.Helpers;
using src.Interfaces;
using src.Level.src.Level;
using src.Managers;
using src.Wall;
using UnityEngine;
using Random = UnityEngine.Random;

namespace src.Level
{
    public class LevelManager : GameplayComponent, IDynamicLevelData
    {
        /** Extracted them here for easy to change reason */
        private const int XMaxEnemyPosition = 5;
        private const int YMinEnemyPosition = -5;

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

        /* Holds references to prefabs for the specified level. */
        private GameObject _indestructibleWallPrefab;
        private GameObject[] _destructibleWallPrefabs;
        private GameObject[] _enemiesPrefab;

        /* Specifies how many objects we want per level. */
        private Count _destructibleWallCount = new Count(150, 350);
        private Count _upgradesCount = new Count(0, 5);
        private Count _enemyCount = new Count(20, 50);

        /* The size of the board. */
        private const int Columns = 30;
        private const int Rows = 20;
        private bool _isBoardInitialized;

        /* Holds the available positions */
        private readonly List<Vector3> _freeGridPositionsBoard = new List<Vector3>();
        private List<Vector3> _freeGridPositions;
        private List<GameObject> _destructibleWalls;
        private List<GameObject> _enemies;

        /* Singletons */
        private GameStateManager _gameStateManager = GameStateManager.Instance;

        public void SetLevelData(LevelData levelData)
        {
            _destructibleWallCount = levelData.destructibleWallCount;
            _upgradesCount = levelData.upgradeCount;
            _enemyCount = levelData.enemyCount;
            _enemiesPrefab = levelData.enemiesPrefab;
            _destructibleWallPrefabs = levelData.destructibleWallsPrefab;
            _indestructibleWallPrefab = levelData.indestructibleWallsPrefab.ChoseRandom();
        }

        /* Modifies walls from _destructibleWalls in order to setup upgrades*/
        private void SetupSpawnables()
        {
            var count = _upgradesCount.RandomIntRange();
            var wallsSize = _destructibleWalls.Count;
            _destructibleWalls.ShuffleList();
            for (var i = 0; i < count; i++)
            {
                if (i > wallsSize - 1)
                {
                    DebugHelper.LogWarning("No destructible walls left, cannot spawn upgrade.");
                    continue;
                }

                /* Get the destructible wall script and make it to spawn the upgrade */
                var wall = _destructibleWalls[i].GetComponent<DestructibleWall>();
                DebugHelper.LogInfo($"Spawned upgrade at: x:{wall.XCoordinate} y:{wall.YCoordinate}");
                wall.SpawnsUpgrade();
            }

            if (count > wallsSize - 2)
            {
                Debug.LogWarning("No destructible walls found, cannot spawn exit!");
                return;
            }

            var exitWall = _destructibleWalls[count + 1].GetComponent<DestructibleWall>();
            DebugHelper.LogInfo($"Spawned exit at: x:{exitWall.XCoordinate} y:{exitWall.YCoordinate}");
            exitWall.SpawnsExit();
        }

        /* Place the indestructible tiles on the board and saves the
         * unused positions in a list. */
        public void InitBoard()
        {
            if (_isBoardInitialized)
            {
                /* We only want to initialize the board once since it doesn't change... for now*/
                return;
            }

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
                    _freeGridPositionsBoard.Add(new Vector3(x, y, 0f));
                }
            }

            _isBoardInitialized = true;
        }

        /* Randomly places destructible tiles on the level. */
        private void SetupLevelDestructibleWalls()
        {
            var numberOfWallsRemaining = _destructibleWallCount.RandomIntRange();
            List<Vector3> usedPositions = new List<Vector3>();
            _freeGridPositions.ShuffleList();
            foreach (var nextPosition in _freeGridPositions)
            {
                if (numberOfWallsRemaining == 0)
                {
                    break;
                }

                usedPositions.Add(nextPosition);
                PlaceDestructibleTile(nextPosition);
                numberOfWallsRemaining -= 1;
            }

            foreach (var usedPosition in usedPositions)
            {
                _freeGridPositions.Remove(usedPosition);
            }
        }

        private void PlaceDestructibleTile(Vector3 position)
        {
            DebugHelper.LogVerbose($"PlaceDestructibleTile: x:{position.x} y:{position.y}");
            var randomWall = _destructibleWallPrefabs.ChoseRandom();
            var instance = Instantiate(randomWall, position, Quaternion.identity);
            _destructibleWalls.Add(instance);
            instance.transform.SetParent(boardHolder);
        }

        private bool PlaceIndestructibleTile(float x, float y)
        {
            DebugHelper.LogVerbose($"PlaceIndestructibleTile: x:{x} y:{y}");
            var absX = Mathf.RoundToInt(x);
            var absY = Mathf.RoundToInt(y);

            if (absX % 2 == 0 || absY % 2 == 0)
            {
                return false;
            }

            var instance =
                Instantiate(_indestructibleWallPrefab, new Vector3(x, y, 0f), Quaternion.identity);
            instance.transform.SetParent(boardHolder);
            return true;
        }

        private void SetupLevelEnemies()
        {
            var numberOfEnemiesToPlace = _enemyCount.RandomIntRange();

            _freeGridPositions.ShuffleList();
            _freeGridPositions.RemoveAll(pos => pos.x <= XMaxEnemyPosition && pos.y >= YMinEnemyPosition);
            foreach (var nextPosition in _freeGridPositions)
            {
                if (numberOfEnemiesToPlace == 0)
                {
                    break;
                }
                PlaceEnemy(nextPosition);
                numberOfEnemiesToPlace -= 1;
            }
        }

        private bool PlaceEnemy(Vector3 position)
        {
            DebugHelper.LogVerbose($"PlaceEnemy: x:{position.x} y:{position.y}");
            var randomEnemy = _enemiesPrefab.ChoseRandom();
            var instance = Instantiate(randomEnemy, position, Quaternion.identity);
            _enemies.Add(instance);
            instance.transform.SetParent(boardHolder);
            return true;
        }

        /* Initializes the level. */
        public void InitLevel()
        {
            DebugHelper.LogInfo($"Initializing level: #{_gameStateManager.Level}");
            _freeGridPositions = new List<Vector3>(_freeGridPositionsBoard);
            _destructibleWalls = new List<GameObject>();
            _enemies = new List<GameObject>();
            SetupLevelDestructibleWalls();
            SetupLevelEnemies();
            SetupSpawnables();
        }

        public void DestroyLevel()
        {
            foreach (var enemy in _enemies)
            {
                Destroy(enemy);
            }

            foreach (var wall in _destructibleWalls)
            {
                Destroy(wall);
            }

            DebugHelper.LogInfo("LevelManager: Cleared level!");
        }
    }
}