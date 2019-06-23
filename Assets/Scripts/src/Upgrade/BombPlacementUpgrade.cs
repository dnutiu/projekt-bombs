namespace src.Upgrade
{
    public class BombPlacementUpgrade : UpgradeBase
    {
        public override void PerformUpgrade()
        {
            var bombManager = gameManager.GetBombsUtilManager();
            bombManager.IncreaseAllowedBombs();
        }
    }
}