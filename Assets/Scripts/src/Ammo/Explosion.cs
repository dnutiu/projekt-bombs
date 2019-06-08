using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    BombStatsUtil bombUtil = BombStatsUtil.Instance;

    public void Start()
    {
        Destroy(gameObject, bombUtil.ExplosionDuration);
    }
}
