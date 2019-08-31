using src.Interfaces;
using src.Managers;
using src.Player;
using src.Upgrade;
using UnityEngine;

namespace src.Base
{
    public class UpgradeBase : GameplayComponent, IUpgrade
    {
        private UpgradeManager _upgradeManager;
        protected PlayerController PlayerToUpgrade;

        public void Start()
        {
            _upgradeManager = UpgradeManager.instance;
        }

        public virtual void PerformUpgrade()
        {
            throw new System.NotImplementedException();
        }

        public void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.CompareTag("Player")) return;
            PlayerToUpgrade = other.GetComponent<PlayerController>();
            PerformUpgrade();
            _upgradeManager.ClaimUpgrade(gameObject);
            Destroy(gameObject);
        }
    }
}