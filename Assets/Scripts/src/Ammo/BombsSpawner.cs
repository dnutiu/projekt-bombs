using src.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombsSpawner : MonoBehaviour
{
    public GameObject bombPrefab;

    private BombsUtilManager _bombsUtil = BombsUtilManager.Instance;

    public void PlaceBomb(Transform transform)
    {
        var absX = Mathf.RoundToInt(transform.position.x);
        var absY = Mathf.RoundToInt(transform.position.y);
        Vector3 position = new Vector3(absX, absY, 0);
        if (_bombsUtil.CanPlaceBomb(position))
        {
            Instantiate(bombPrefab, position, Quaternion.identity);
            _bombsUtil.PlaceBomb(position);
        }
    }
}
