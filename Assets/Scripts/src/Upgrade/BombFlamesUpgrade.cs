using src.Base;
using src.Managers;

namespace src.Upgrade
{
    public class BombFlamesUpgrade : UpgradeBase
    {
        public override void PerformUpgrade()
        {
            var bombManager = PlayerToUpgrade.GetComponent<BombsUtilManager>();
            bombManager.IncreasePower();
        }
    }
}