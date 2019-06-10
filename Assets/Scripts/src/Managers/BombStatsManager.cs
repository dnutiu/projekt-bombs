namespace src.Managers
{
    public sealed class BombStatsManager
    {
        private const int MaxPower = 7;

        public int Power { get; private set; } = 3;

        public float Timer { get; } = 3.0f;

        public float ExplosionDuration { get; } = 0.55f;

        private BombStatsManager()
        {
        }

        public static BombStatsManager Instance { get; } = new BombStatsManager();

        public void IncreasePower()
        {
            if (Power <= MaxPower)
            {
                Power++;
            }
        }
    }
}