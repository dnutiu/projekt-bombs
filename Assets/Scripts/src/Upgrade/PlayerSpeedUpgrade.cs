using src.Base;
using src.Player;

namespace src.Upgrade
{
    public class PlayerSpeedUpgrade : UpgradeBase
    {
        public override void PerformUpgrade()
        {
            var player = _playerToUpgrade.GetComponent<PlayerUpgrade>();
            player.IncreaseSpeed(.5f);
        }
    }
}