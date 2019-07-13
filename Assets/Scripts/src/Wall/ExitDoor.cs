using System;
using src.Base;
using src.Managers;
using UnityEngine;

namespace src.Wall
{
    public class ExitDoor : GameplayComponent
    {
        private GameManager _gameManager;
        private Collider2D _collider2D;

        private void Start()
        {
            _gameManager = GameManager.Instance;
            _collider2D = GetComponent<Collider2D>();
        }

        /* Trigger the next level and destroy itself. */
        private void OnTriggerStay2D(Collider2D other)
        {
            if (!other.CompareTag("Player"))
            {
                return;
            }
            Destroy(gameObject, 1f);
            _gameManager.StartNextLevel();
        }
    }
}