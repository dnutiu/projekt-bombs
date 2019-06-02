using System;
using src.Base;
using UnityEngine;

namespace src.Player
{
    public class PlayerController : PlayerBase
    {

        public GameObject bombPrefab;

        Transform playerTransform;

        //O sa fie mai tarziu folosita cand o sa avem numar de bombe allowed
        bool canPlaceBombs = true;

        protected new void Start()
        {
            base.Start();
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
            playerTransform.position = movementVector;
#elif UNITY_IOS || UNITY_ANDROID
    // Phone movement is not supported yet.
#elif UNITY_PS4 || UNITY_XBOXONE
    // Console movement is not supported yet.
#endif
        }

        private void PlaceBomb()
        {
#if UNITY_EDITOR || UNITY_STANDALONE || UNITY_WEBGL
            if (bombPrefab)
            {
                Instantiate(bombPrefab, new Vector3(Mathf.RoundToInt(playerTransform.position.x),
                    bombPrefab.transform.position.y, Mathf.RoundToInt(playerTransform.position.z)),
                    bombPrefab.transform.rotation);
            }
#elif UNITY_IOS || UNITY_ANDROID
            // Phone movement is not supported yet.
#elif UNITY_PS4 || UNITY_XBOXONE
            // Console movement is not supported yet.
#endif
        }

        private void HandleBomb()
        {
            if (canPlaceBombs && Input.GetKeyDown(KeyCode.Space))
            {
                PlaceBomb();
            }
        }
    }
}