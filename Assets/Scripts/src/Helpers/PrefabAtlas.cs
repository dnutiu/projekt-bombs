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
            Resources.Load<GameObject>("Items/SpeedUpgrade");
        public static readonly GameObject BombsIncreaseUpgrade = 
            Resources.Load<GameObject>("Items/BombUpgrade");
        public static readonly GameObject FlamesIncreaseUpgrade = 
            Resources.Load<GameObject>("Items/FlameUpgrade");
        public static readonly GameObject GoldenBombUpgrade = 
            Resources.Load<GameObject>("Items/GoldenBombUpgrade");
        
        /* Items */
        public static readonly GameObject PlayerBomb = 
            Resources.Load<GameObject>("Items/Bomb");
        public static readonly GameObject BombExplosion = 
            Resources.Load<GameObject>("Items/BombExplosion");

        /* Enemies */
        public static readonly GameObject GreenEnemy = Resources.Load<GameObject>("Enemies/SnowEnemyRandom");
        public static readonly GameObject RedEnemy = Resources.Load<GameObject>("Enemies/SnowEnemyCollision");
    }
}