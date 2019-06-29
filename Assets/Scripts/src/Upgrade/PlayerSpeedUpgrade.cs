using src.Player;

namespace src.Upgrade
{
    public class PlayerSpeedUpgrade : UpgradeBase
    {
        public override void PerformUpgrade()
        {
            var player = PlayerUpgrade.Instance;
            player.IncreaseSpeed(.5f);
        }
    }
}