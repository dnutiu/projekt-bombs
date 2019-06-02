using System;
using src.Base;
using UnityEngine;

namespace src.Player
{
    public class PlayerController : PlayerBase
    {
        public Transform respawnPosition;


        public GameObject bombPrefab;

        protected new void Start()
        {
            base.Start();

            /* Always start at the starting point. */
            Respawn();
        }

        private void Update()
        {
            HandleMovement();
            HandleBomb();
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

            Rigidbody2d.position += movementSpeed * Time.deltaTime * movementVector;
#elif UNITY_IOS || UNITY_ANDROID
    // Phone movement is not supported yet.
#elif UNITY_PS4 || UNITY_XBOXONE
    // Console movement is not supported yet.
#endif
        }

        private void PlaceBomb()
        {
            Instantiate(bombPrefab, new Vector3(Mathf.RoundToInt(transform.position.x),
                    bombPrefab.transform.position.y, 0f),
                bombPrefab.transform.rotation);
        }

        private void HandleBomb()
        {
#if UNITY_EDITOR || UNITY_STANDALONE || UNITY_WEBGL
            if (Input.GetKeyDown(KeyCode.Space))
            {
                PlaceBomb();
            }
#elif UNITY_IOS || UNITY_ANDROID
            // Phone bomb placement is not supported yet.
#elif UNITY_PS4 || UNITY_XBOXONE
            // Console bomb placement is not supported yet.
#endif
        }

        private void Respawn()
        {
            transform.position = respawnPosition.position;
        }
    }
}