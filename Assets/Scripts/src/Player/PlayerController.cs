using System;
using System.ComponentModel;
using src.Base;
using src.Helpers;
using src.Managers;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace src.Player
{
    public class PlayerController : GameplayComponent, IExplosable
    {
        public bool godMode;
        public float movementSpeed = 4f;
        
        /* Components */
        private GameStateManager _gameStateManager;
        private Rigidbody2D _rigidbody2d;
        private Collider2D _collider2D;
        private Transform _respawnPosition;
        private BombsUtilManager _bombsUtilManager;
        private Animator _animator;
        private PlayerUpgrade _playerUpgrade;

        /* Variables */
        private bool _isDead;
        
        /* Animator Variables*/
        private static readonly int AnimHorizontal = Animator.StringToHash("AnimHorizontal");
        private static readonly int AnimVertical = Animator.StringToHash("AnimVertical");
        private static readonly int AnimDeath = Animator.StringToHash("AnimDeath");


        protected void Awake()
        {
            _playerUpgrade = gameObject.AddComponent<PlayerUpgrade>();
            _bombsUtilManager = gameObject.AddComponent<BombsUtilManager>();
        }

        protected void Start()
        {
            _gameStateManager = GameStateManager.instance;

            _rigidbody2d = GetComponent<Rigidbody2D>();
            _collider2D = GetComponent<Collider2D>();
            _animator = GetComponentInChildren<Animator>();
            _respawnPosition = GameObject.Find("RespawnPosition").transform;

            movementSpeed = _playerUpgrade.GetMovementSpeed();
            _playerUpgrade.PlayerSpeed += OnSpeedUpgrade;

#if UNITY_EDITOR
            godMode = true;
#endif
            Respawn();
        }

        private void FixedUpdate()
        {
            if (_gameStateManager.IsGamePaused || _gameStateManager.IsPlayerMovementForbidden || _isDead) {return;}
            HandleMovementInput();
        }

        private void Update()
        {
            if (_gameStateManager.IsGamePaused || _gameStateManager.IsPlayerMovementForbidden || _isDead) {return;}
            HandleItemsInput();
        }

        private void HandleMovementInput()
        {
#if UNITY_STANDALONE || UNITY_WEBGL || UNITY_EDITOR
            HandleKeyboardMovement();
#elif UNITY_IOS || UNITY_ANDROID
            HandleTouchMovement();
#elif UNITY_PS4 || UNITY_XBOXONE
            HandlerControllerMovement();
#endif
        }

        private void HandleItemsInput()
        {
#if UNITY_EDITOR || UNITY_STANDALONE || UNITY_WEBGL
            if (Input.GetKeyDown(KeyCode.Space))
            {
                PlaceBomb();
            }
#elif UNITY_IOS || UNITY_ANDROID
            if (CrossPlatformInputManager.GetButton("PlaceBomb"))
            {
                PlaceBomb();
            }
#elif UNITY_PS4 || UNITY_XBOXONE
            // Console bomb placement is not supported yet.
#endif
        }
        
#if UNITY_STANDALONE || UNITY_WEBGL || UNITY_EDITOR
        private void HandleKeyboardMovement()
        {
            var horizontal = Input.GetAxisRaw("Horizontal");
            var vertical = Input.GetAxisRaw("Vertical");
            var movementVector = new Vector2(horizontal, vertical);
            MovePosition(movementVector);
        }
#endif
        
#if UNITY_IOS || UNITY_ANDROID
        private void HandleTouchMovement()
        {
            var horizontal = 0;
            var vertical = 0;
            if (CrossPlatformInputManager.GetButton("MoveUp"))
            {
                vertical = 1;
            }
            if (CrossPlatformInputManager.GetButton("MoveDown"))
            {
                vertical = -1;
            }
            if (CrossPlatformInputManager.GetButton("MoveRight"))
            {
                horizontal = 1;
            }
            if (CrossPlatformInputManager.GetButton("MoveLeft"))
            {
                horizontal = -1;
            }
            var movementVector = new Vector2(horizontal, vertical);
            MovePosition(movementVector);
        }
#endif

#if UNITY_PS4 || UNITY_XBOXONE
        private void HandlerControllerMovement()
        {
            throw new NotImplementedException();
        }
#endif
        
        private void MovePosition(Vector2 movementVector)
        {
            movementVector = movementVector.NormalizeToCross();
            _animator.SetFloat(AnimHorizontal, movementVector.x);
            _animator.SetFloat(AnimVertical, movementVector.y);
            _rigidbody2d.MovePosition(_rigidbody2d.position + movementSpeed * Time.deltaTime * movementVector);
        }

        private void PlaceBomb()
        {
            var position = transform.position;
            var absX = Mathf.RoundToInt(position.x);
            var absY = Mathf.RoundToInt(position.y);
            var newPosition = new Vector2(absX, absY);
            if (!_bombsUtilManager.CanPlaceBomb(newPosition)) return;
            
            Instantiate(PrefabAtlas.Bomb, newPosition, Quaternion.identity);
            _bombsUtilManager.RegisterBomb(newPosition);
        }
        
        public void Respawn()
        {
            DebugHelper.LogInfo("Player is re-spawning!");
            transform.SetPositionAndRotation(_respawnPosition.position, Quaternion.identity);
            _animator.Play("IdleDown");
        }
        
        private void Die()
        {
            if (godMode)
            {
                return;
            }
            _isDead = true;
            _collider2D.enabled = false;
            _animator.SetBool(AnimDeath, true);
            Destroy(gameObject, 0.7f);
        }

        public void OnTriggerExit2D(Collider2D other)
        {
            /* Turn off bomb trigger making it so you can't pass through. */
            if (other.CompareTag("Bomb"))
            {
                other.isTrigger = false;
            }
        }

        public void OnCollisionEnter2D(Collision2D other)
        {
            if (other.collider.CompareTag("Enemy"))
            {
                OnContactWithEnemy();
            }
        }

        public void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Explosion"))
            {
                OnExplosion();
            }
        }

        public void OnExplosion()
        {
            DebugHelper.LogInfo("Player hit by explosion");
            Die();
        }

        private void OnContactWithEnemy()
        {
            DebugHelper.LogInfo("Player hit by enemy");
            Die();
        }

        private void OnSpeedUpgrade(float speed)
        {
            movementSpeed += speed;
        }
    }
}