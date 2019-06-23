using src.Player;
using UnityEngine;

namespace src.Upgrade
{
    public class PlayerSpeedUpgrade : UpgradeBase
    {
        public override void PerformUpgrade()
        {
            /* TODO: Refactor to use a player manager. */
            var player = GameObject.Find("Player").GetComponent<PlayerController>();
            player.IncreaseSpeed(.5f);
        }
    }
}