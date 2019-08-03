using System;
using System.Collections.Generic;
using src.Base;
using src.Helpers;
using src.Interfaces;
using src.Level.src.Level;
using UnityEngine;

namespace src.Managers
{
    public class UpgradeManager : GameplayComponent, IDynamicLevelData
    {
        public static UpgradeManager Instance;
        private List<GameObject> _unclaimedUpgrades = new List<GameObject>();
        private GameObject[] _upgradePrefabs;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else if (Instance != null)
            {
                Destroy(gameObject);
            }
        }
        
        public void SetLevelData(LevelData levelData)
        {
            _upgradePrefabs = levelData.upgradesPrefab;
        }

        public GameObject GetUpgradePrefab()
        {
            return _upgradePrefabs.ChoseRandom();
        }

        /* Register unclaimed upgrades so then can be destroyed on level changed or other events. */
        public void RegisterUpgradeAsUnclaimed(GameObject instance)
        {
            _unclaimedUpgrades.Add(instance);
        }

        public void ClaimUpgrade(GameObject instance)
        {
            _unclaimedUpgrades.Remove(instance);
        }

        public void DestroyUnclaimedUpgrades()
        {
            foreach (var upgrade in _unclaimedUpgrades)
            {
                Destroy(upgrade);
            }
            _unclaimedUpgrades = new List<GameObject>();
        }
    }
}