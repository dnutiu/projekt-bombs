using System;
using src.Base;
using UnityEngine;

namespace src.Player
{
    public class Player : PlayerBase
    {
        /* Movement */
        private Rigidbody2D _rigidbody2d;

        private void Start()
        {
            _rigidbody2d = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            HandleMovement();
        }

        private void HandleMovement()
        {
#if UNITY_EDITOR || UNITY_STANDALONE || UNITY_WEBGL
            var horizontal = Input.GetAxis("Horizontal");
            var vertical = Input.GetAxis("Vertical");

            // Restrict movement in only one axis at the same time.
            if (Math.Abs(vertical) > 0.00001)
            {
                horizontal = 0;
            }
            else
            {
                vertical = 0;
            }

            var movementVector = new Vector2(horizontal, vertical);

            _rigidbody2d.position += movementSpeed * Time.deltaTime * movementVector;
#elif UNITY_IOS || UNITY_ANDROID
    // Phone movement is not supported yet.
#elif UNITY_PS4 || UNITY_XBOXONE
    // Console movement is not supported yet.
#endif
        }
    }
}