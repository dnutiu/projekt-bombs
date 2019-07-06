using src.Ammo;
using src.Base;
using src.Helpers;
using src.Interfaces;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace src.Player
{
    public class PlayerController : PlayerBase
    {
        private Transform _respawnPosition;
        private BombsSpawner _bombsSpawner;
        private Animator _animator;
        private PlayerUpgrade _playerUpgrade;
        private static readonly int AnimHorizontal = Animator.StringToHash("AnimHorizontal");
        private static readonly int AnimVertical = Animator.StringToHash("AnimVertical");

        protected new void Start()
        {
            base.Start();

            _respawnPosition = GameObject.Find("RespawnPosition").transform;
            _bombsSpawner = GameObject.Find("BombSpawner").GetComponent<BombsSpawner>();
            _animator = GetComponentInChildren<Animator>();
            _playerUpgrade = PlayerUpgrade.Instance;

            movementSpeed = _playerUpgrade.GetMovementSpeed();
            _playerUpgrade.PlayerSpeed += IncreaseSpeed;


            /* Always start at the starting point. */
            Respawn();
        }

        private void FixedUpdate()
        {
            if (ApplicationActions.IsGamePaused) {return;}
            HandleMovement();
        }

        private void Update()
        {
            if (ApplicationActions.IsGamePaused) {return;}
            HandleBomb();
        }

        private void HandleMovement()
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


            rigidbody2d.MovePosition(rigidbody2d.position + movementSpeed * Time.deltaTime * movementVector);
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
            if (CrossPlatformInputManager.GetButton("PlaceBomb"))
            {
                PlaceBomb();
            }
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