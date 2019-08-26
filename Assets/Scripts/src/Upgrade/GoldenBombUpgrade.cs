using src.Base;
using src.Managers;

namespace src.Upgrade
{
    /* Adds one firepower and one bomb. */
    public class GoldenBombUpgrade : UpgradeBase
    {
        public override void PerformUpgrade()
        {
            var bombManager = _playerToUpgrade.GetComponent<BombsUtilManager>();
            bombManager.IncreaseAllowedBombs();
            bombManager.IncreasePower();
        }
    }
}