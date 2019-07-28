using src.Helpers;

namespace src.Level
{
    using System;
    using UnityEngine;

    namespace src.Level
    {
        public struct LevelData
        {
            public int levelNumber;
            public Count destructibleWallCount;
            public Count upgradeCount;
            public Count enemyCount;
            public GameObject[] enemiesPrefab;
            public GameObject[] upgradesPrefab;
            public GameObject[] destructibleWallsPrefab;
            public GameObject[] indestructibleWallsPrefab;
        }

        public static class LevelResource
        {
            private static readonly GameObject[] AllUpgrades =
                {PrefabAtlas.FlamesIncreaseUpgrade, PrefabAtlas.BombsIncreaseUpgrade, PrefabAtlas.SpeedIncreaseUpgrade};
            private static readonly GameObject[] SnowWallsDestructible =
                {PrefabAtlas.DestructibleSnow, PrefabAtlas.DestructibleHighSnow};
            private static readonly GameObject[] SnowWallsIndestructible = {PrefabAtlas.IndestructibleWoodCrate};
            
            /* Used to store information about the level. */
            private static readonly LevelData[] LevelData =
            {
                new LevelData
                {
                    levelNumber = 1,
                    destructibleWallCount = new Count(150, 250),
                    upgradeCount = new Count(0, 5),
                    enemyCount = new Count(20, 50),
                    enemiesPrefab = new[] {PrefabAtlas.GreenEnemy, PrefabAtlas.RedEnemy},
                    upgradesPrefab = AllUpgrades,
                    destructibleWallsPrefab = SnowWallsDestructible,
                    indestructibleWallsPrefab = SnowWallsIndestructible
                }
            };

            /*
             * Return data from level data, if it overflows, reset index.
             */
            public static LevelData GetLevelData(int level)
            {
                return LevelData[level % LevelData.Length];
            }
        }
    }
}