using src.Base;

namespace src.Upgrade
{
    /* Adds one firepower and one bomb. */
    public class GoldenBombUpgrade : UpgradeBase
    {
        public override void PerformUpgrade()
        {
            var bombManager = gameManager.GetBombsUtilManager();
            bombManager.IncreaseAllowedBombs();
            bombManager.IncreasePower();
        }
    }
}