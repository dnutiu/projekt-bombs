using System.Collections;
using System.Collections.Generic;
using src.Helpers;
using src.Managers;
using UnityEngine;

namespace src.Base
{
    public abstract class EnemyBase : GameplayComponent, IExplosable
    {
        private readonly Vector2[] _directions = {Vector3.up, Vector3.down, Vector3.left, Vector3.right};
        private readonly GameStateManager _gameStateManager = GameStateManager.instance;

        protected Rigidbody2D Rigidbody2d { get; set; }
        private Collider2D Collider2D { get; set; }
        private Animator Animator { get; set; }
        protected float Speed { get; set; }
        protected Vector2 Direction { get; set; }

        private readonly List<Vector3> _allowedDirections = new List<Vector3>();
        private bool _isStuck;
        private bool _isDead;
    
        private static readonly int AnimExplode = Animator.StringToHash("animExplode");

        // Start is called before the first frame update
        protected void Start()
        {
            Direction = _directions.ChoseRandom();
            Rigidbody2d = GetComponent<Rigidbody2D>();
            Collider2D = GetComponent<Collider2D>();
            Animator = GetComponentInChildren<Animator>();
        }

        // Update is called once per frame
        protected void FixedUpdate()
        {
            if (_gameStateManager.IsGamePaused || _gameStateManager.IsPlayerMovementForbidden || _isDead)
            {
                return;
            }

            if (_isStuck)
            {
                Unstuck();
            }
            HandleMovement();
        }

        /// <summary>
        ///  This function is implemented by subclasses and should provided personalized movement logic.
        /// </summary>
        protected abstract void HandleMovement();

        public void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Explosion"))
            {
                OnExplosion();
            }
        }

        public void OnExplosion()
        {
            Collider2D.enabled = false;
            _isDead = true;
            Animator.SetTrigger(AnimExplode);
            Destroy(gameObject, 1);
        }

        public void OnCollisionEnter2D(Collision2D col)
        {
            MoveToCenterOfTheCell();
            Unstuck();
        }

        public void OnCollisionStay2D(Collision2D col)
        {
            MoveToCenterOfTheCell();
            Unstuck();
        }

        protected void MoveToCenterOfTheCell()
        {
            var position = transform.position;
            var absX = Mathf.RoundToInt(position.x);
            var absY = Mathf.RoundToInt(position.y);
            var newPosition = new Vector2(absX, absY);
            transform.SetPositionAndRotation(newPosition, Quaternion.identity);
        }

        protected Vector2 ChooseRandomDirection()
        {
            return _directions.ChoseRandom();
        }

        protected Vector2 ChooseRandomExceptCertainDirection(Vector2 direction)
        {
            return _directions.ChoseRandomExcept(direction);
        }

        private void Unstuck()
        {
            _allowedDirections.Clear();
            StartCoroutine(CheckForObstacle(Vector3.down));
            StartCoroutine(CheckForObstacle(Vector3.left));
            StartCoroutine(CheckForObstacle(Vector3.up));
            StartCoroutine(CheckForObstacle(Vector3.right));
            if (_allowedDirections.Count == 0)
            {
                _isStuck = true;
            }
            else
            {
                Direction = _allowedDirections.PeekRandom();
                _isStuck = false;
            }
        }

        private IEnumerator CheckForObstacle(Vector3 direction)
        {
            const int layerMask = 1 << 8; // Block layer
            var currentPosition = transform.position;

            var hit = Physics2D.Raycast(new Vector2(currentPosition.x + 0.5f, currentPosition.y + 0.5f), 
                direction, 1, layerMask);

            if (!hit.collider)
            {
                _allowedDirections.Add(direction);
            }

            yield return new WaitForSeconds(0.05f);
        }
    }
}