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
            Resources.Load<GameObject>("DevMocks/SpeedUpgrade");
        public static readonly GameObject BombsIncreaseUpgrade = 
            Resources.Load<GameObject>("DevMocks/BombsUpgrade");
        public static readonly GameObject FlamesIncreaseUpgrade = 
            Resources.Load<GameObject>("DevMocks/FlameUpgrade");

        /* Enemies */
        public static readonly GameObject GreenEnemy = Resources.Load<GameObject>("DevMocks/Dumber");
        public static readonly GameObject RedEnemy = Resources.Load<GameObject>("DevMocks/Dumb");
    }
}