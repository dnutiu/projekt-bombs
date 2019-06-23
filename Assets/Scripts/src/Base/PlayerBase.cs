using System;
using src.Helpers;
using UnityEngine;

namespace src.Base
{
    public abstract class PlayerBase : GameplayComponent, IExplosable
    {
        public float movementSpeed = 4f;
        
        /* Movement */
        protected Rigidbody2D rigidbody2d;

        protected void Start()
        {
            rigidbody2d = GetComponent<Rigidbody2D>();
        }

        public void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Explosion"))
            {
                onExplosion();
            }
            if (other.CompareTag("Enemy"))
            {
                OnContactWithEnemy();
            }
        }

        public void onExplosion()
        {
            DebugHelper.LogInfo("Player hit by explosion");
        }

        private void OnContactWithEnemy()
        {
            DebugHelper.LogInfo("Player hit by enemy");
        }

        public void IncreaseSpeed(float speed)
        {
            movementSpeed += speed;
        }
    }
}