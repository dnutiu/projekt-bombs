using UnityEngine;

namespace src.Helpers
{
    public static class PrefabAtlas
    {
        /* UI */
        public static readonly GameObject PreStageUi =
            Resources.Load<GameObject>("UI/PreStageUI");
        
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
        public static readonly GameObject GoldenBombUpgrade = 
            Resources.Load<GameObject>("GoldenBombUpgrade/FlameUpgrade");

        /* Enemies */
        public static readonly GameObject GreenEnemy = Resources.Load<GameObject>("Enemies/SnowEnemyRandom");
        public static readonly GameObject RedEnemy = Resources.Load<GameObject>("Enemies/SnowEnemyCollision");
    }
}