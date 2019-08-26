using System;
using src.Base;
using src.Helpers;
using src.Managers;
using UnityEngine;

namespace src.Wall
{
    public class ExitDoor : GameplayComponent
    {
        private GameManager _gameManager;

        private void Start()
        {
            _gameManager = GameManager.instance;
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