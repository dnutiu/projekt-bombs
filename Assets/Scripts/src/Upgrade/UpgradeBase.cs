using src.Base;
using src.Interfaces;
using src.Managers;
using UnityEngine;

namespace src.Upgrade
{
    public class UpgradeBase : GameplayComponent, IUpgrade
    {
        protected GameManager gameManager;

        public void Start()
        {
            gameManager = GameManager.Instance;
        }

        public virtual void PerformUpgrade()
        {
            throw new System.NotImplementedException();
        }

        public void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.CompareTag("Player")) return;
            PerformUpgrade();
            Destroy(gameObject);
        }
    }
}