using src.Base;

namespace src.Upgrade
{
    /* Adds one firepower and one bomb. */
    public class GoldenBombUpgrade : UpgradeBase
    {
        public override void PerformUpgrade()
        {
            var bombManager = GameManager.GetBombsUtilManager();
            bombManager.IncreaseAllowedBombs();
            bombManager.IncreasePower();
        }
    }
}