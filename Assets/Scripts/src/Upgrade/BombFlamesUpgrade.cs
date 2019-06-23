namespace src.Upgrade
{
    public class BombFlamesUpgrade : UpgradeBase
    {
        public override void PerformUpgrade()
        {
            var bombManager = gameManager.GetBombsUtilManager();
            bombManager.IncreasePower();
        }
    }
}