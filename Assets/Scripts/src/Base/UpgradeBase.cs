using src.Interfaces;
using src.Managers;
using UnityEngine;

namespace src.Base
{
    public class UpgradeBase : GameplayComponent, IUpgrade
    {
        protected GameManager GameManager;
        private UpgradeManager _upgradeManager;

        public void Start()
        {
            GameManager = GameManager.Instance;
            _upgradeManager = UpgradeManager.Instance;
        }

        public virtual void PerformUpgrade()
        {
            throw new System.NotImplementedException();
        }

        public void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.CompareTag("Player")) return;
            PerformUpgrade();
            _upgradeManager.ClaimUpgrade(gameObject);
            Destroy(gameObject);
        }
    }
}