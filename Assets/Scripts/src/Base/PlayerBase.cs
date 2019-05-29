using System;
using UnityEngine;

namespace src.Base
{
    public abstract class PlayerBase : GameplayComponent
    {
        public float movementSpeed = 4f;
        
        /* Movement */
        protected Rigidbody2D Rigidbody2d;

        protected void Start()
        {
            Rigidbody2d = GetComponent<Rigidbody2D>();
        }
    }
}