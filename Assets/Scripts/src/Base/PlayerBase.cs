using System;
using UnityEngine;

namespace src.Base
{
    public abstract class PlayerBase : GameplayComponent, IExplosable
    {
        public float movementSpeed = 4f;
        
        /* Movement */
        protected Rigidbody2D Rigidbody2d;

        protected void Start()
        {
            Rigidbody2d = GetComponent<Rigidbody2D>();
        }

        public void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Explosion"))
            {
                onExplosion();
            }
        }

        public void onExplosion()
        {
            Debug.Log("Player hit by explosion");
        }
    }
}