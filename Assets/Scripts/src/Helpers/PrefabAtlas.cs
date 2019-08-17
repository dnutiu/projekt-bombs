using UnityEngine;

namespace src.Helpers
{
    public static class PrefabAtlas
    {
        /* Snow Walls */
        public static readonly GameObject DestructibleHighSnow =
            Resources.Load<GameObject>("Walls/destructible_high_snow");
        public static readonly GameObject DestructibleSnow = 
            Resources.Load<GameObject>("Walls/destructible_snow");
        public static readonly GameObject IndestructibleWoodCrate =
            Resources.Load<GameObject>("Walls/indestructible_crate");

        /* Upgrades */
        public static readonly GameObject SpeedIncreaseUpgrade = 
            Resources.Load<GameObject>("Ammo/SpeedUpgrade");
        public static readonly GameObject BombsIncreaseUpgrade = 
            Resources.Load<GameObject>("Ammo/BombUpgrade");
        public static readonly GameObject FlamesIncreaseUpgrade = 
            Resources.Load<GameObject>("Ammo/FlameUpgrade");

        /* Enemies */
        public static readonly GameObject GreenEnemy = Resources.Load<GameObject>("Enemies/SnowEnemyRandom");
        public static readonly GameObject RedEnemy = Resources.Load<GameObject>("Enemies/SnowEnemyCollision");
    }
}