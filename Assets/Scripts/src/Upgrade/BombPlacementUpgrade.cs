using src.Base;

namespace src.Upgrade
{
    public class BombPlacementUpgrade : UpgradeBase
    {
        public override void PerformUpgrade()
        {
            var bombManager = GameManager.GetBombsUtilManager();
            bombManager.IncreaseAllowedBombs();
        }
    }
}