using src.Interfaces;
using src.Managers;
using src.Player;
using src.Upgrade;
using UnityEngine;

namespace src.Base
{
    public class UpgradeBase : GameplayComponent, IUpgrade
    {
        protected GameManager gameManager;
        private UpgradeManager _upgradeManager;
        protected PlayerController _playerToUpgrade;

        public void Start()
        {
            gameManager = GameManager.instance;
            _upgradeManager = UpgradeManager.instance;
        }

        public virtual void PerformUpgrade()
        {
            throw new System.NotImplementedException();
        }

        public void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.CompareTag("Player")) return;
            _playerToUpgrade = other.GetComponent<PlayerController>();
            PerformUpgrade();
            _upgradeManager.ClaimUpgrade(gameObject);
            Destroy(gameObject);
        }
    }
}