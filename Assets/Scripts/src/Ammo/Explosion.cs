using System.Collections;
using System.Collections.Generic;
using src.Managers;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    private readonly BombStatsManager _bombUtil = BombStatsManager.Instance;

    public void Start()
    {
        Destroy(gameObject, _bombUtil.ExplosionDuration);
    }
}
