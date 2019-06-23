using src.Base;
using src.Helpers;
using UnityEngine;

namespace src.Managers
{
    public class UpgradeManager : GameplayComponent
    {
        public GameObject[] upgradePrefabs;

        public GameObject GetUpgradePrefab()
        {
            return upgradePrefabs.ChoseRandom();
        }
    }
}