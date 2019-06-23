using src.Base;
using src.Helpers;
using src.Interfaces;
using UnityEngine;

namespace src.Upgrade
{
    public class TestUpgrade : UpgradeBase
    {
        public void PerformUpgrade()
        {
            DebugHelper.LogWarning("Test upgrade works!");
        }
    }
}