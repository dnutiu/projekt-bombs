using src.Base;
using src.Helpers;
using src.Interfaces;
using UnityEngine;

namespace src.Upgrade
{
    public class TestUpgrade : GameplayComponent, IUpgrade
    {
        public void PerformUpgrade()
        {
            DebugHelper.LogError("Test upgrade works.");
        }

        public void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.CompareTag("Player")) return;
            
            DebugHelper.LogWarning("TestUpgrade WORKS!");
            Destroy(gameObject);
        }
    }
}