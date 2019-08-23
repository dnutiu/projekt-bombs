using src.Ammo;
using src.Base;
using src.Helpers;
using src.Managers;
using UnityEngine;

namespace src.Player
{
    public class PlayerController : GameplayComponent, IExplosable
    {
        private GameStateManager _gameStateManager;
        private Rigidbody2D _rigidbody2d;
        private Collider2D _collider2D;
        private Transform _respawnPosition;
        private BombsSpawner _bombsSpawner;
        private Animator _animator;
        private PlayerUpgrade _playerUpgrade;
        private static readonly int AnimHorizontal = Animator.StringToHash("AnimHorizontal");
        private static readonly int AnimVertical = Animator.StringToHash("AnimVertical");
        private static readonly int AnimDeath = Animator.StringToHash("AnimDeath");


        public bool godMode;
        public float movementSpeed = 4f;
        private bool _isDead;

        protected void Start()
        {   
            _gameStateManager = GameStateManager.instance;
            _playerUpgrade = PlayerUpgrade.instance;
            
            _rigidbody2d = GetComponent<Rigidbody2D>();
            _collider2D = GetComponent<Collider2D>();
            _animator = GetComponentInChildren<Animator>();
            _respawnPosition = GameObject.Find("RespawnPosition").transform;
            _bombsSpawner = GameObject.Find("BombSpawner").GetComponent<BombsSpawner>();

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
            HandleBombInput();
        }

        private void HandleMovementInput()
        {
#if UNITY_STANDALONE || UNITY_WEBGL || UNITY_EDITOR
            var horizontal = Input.GetAxisRaw("Horizontal");
            var vertical = Input.GetAxisRaw("Vertical");
#elif UNITY_IOS || UNITY_ANDROID
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
#elif UNITY_PS4 || UNITY_XBOXONE
//    // Console movement is not supported yet.
#endif

            var movementVector = new Vector2(horizontal, vertical).NormalizeToCross();

            _animator.SetFloat(AnimHorizontal, movementVector.x);
            _animator.SetFloat(AnimVertical, movementVector.y);


            _rigidbody2d.MovePosition(_rigidbody2d.position + movementSpeed * Time.deltaTime * movementVector);
        }

        private void PlaceBomb()
        {
            _bombsSpawner.PlaceBomb(transform);
        }

        private void HandleBombInput()
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

        public void Respawn()
        {
            DebugHelper.LogInfo("Player is respawning!");
            transform.SetPositionAndRotation(_respawnPosition.position, Quaternion.identity);
            _animator.Play("IdleDown");
        }

        public void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("Bomb"))
            {
                other.isTrigger = false;
            }
        }
        
        public void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Explosion"))
            {
                OnExplosion();
            }
            if (other.CompareTag("Enemy"))
            {
                OnContactWithEnemy();
            }
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