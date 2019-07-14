using src.Base;

namespace src.Upgrade
{
    public class BombFlamesUpgrade : UpgradeBase
    {
        public override void PerformUpgrade()
        {
            var bombManager = GameManager.GetBombsUtilManager();
            bombManager.IncreasePower();
        }
    }
}