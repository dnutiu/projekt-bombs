using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    BombStatsManager bombUtil = BombStatsManager.Instance;

    public void Start()
    {
        Destroy(gameObject, bombUtil.ExplosionDuration);
    }
}
