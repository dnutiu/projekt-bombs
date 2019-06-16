﻿using src.Ammo;
using src.Base;
using src.Helpers;
using UnityEngine;

namespace src.Player
{
    public class PlayerController : PlayerBase
    {
        private Transform _respawnPosition;
        private BombsSpawner _bombsSpawner;
        private Animator _animator;
        private static readonly int AnimHorizontal = Animator.StringToHash("AnimHorizontal");
        private static readonly int AnimVertical = Animator.StringToHash("AnimVertical");

        protected new void Start()
        {
            base.Start();

            _respawnPosition = GameObject.Find("RespawnPosition").transform;
            _bombsSpawner = GameObject.Find("BombSpawner").GetComponent<BombsSpawner>();
            _animator = GetComponentInChildren<Animator>();
            /* Always start at the starting point. */
            Respawn();
        }

        private void FixedUpdate()
        {
            HandleMovement();
        }

        private void Update()
        {
            HandleBomb();
        }

        private void HandleMovement()
        {
#if UNITY_EDITOR || UNITY_STANDALONE || UNITY_WEBGL
            var horizontal = Input.GetAxisRaw("Horizontal");
            var vertical = Input.GetAxisRaw("Vertical");
            var movementVector = new Vector2(horizontal, vertical).NormalizeToCross();
            
            _animator.SetFloat(AnimHorizontal, movementVector.x);
            _animator.SetFloat(AnimVertical, movementVector.y);
            
            
            rigidbody2d.MovePosition(rigidbody2d.position + movementSpeed * Time.deltaTime * movementVector);
#elif UNITY_IOS || UNITY_ANDROID
    // Phone movement is not supported yet.
#elif UNITY_PS4 || UNITY_XBOXONE
    // Console movement is not supported yet.
#endif
        }

        private void PlaceBomb()
        {
            _bombsSpawner.PlaceBomb(transform);
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
            transform.SetPositionAndRotation(_respawnPosition.position, Quaternion.identity);
        }

        public void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("Bomb"))
            {
                
                other.isTrigger = false;
            }
        }
    }
}