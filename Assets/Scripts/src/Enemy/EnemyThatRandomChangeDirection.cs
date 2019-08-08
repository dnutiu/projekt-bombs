using UnityEngine;

namespace Assets.Scripts.src.Enemy
{
    public class EnemyThatRandomChangeDirection : EnemyBase
    {
        protected new void Start()
        {
            Speed = 4f;
            base.Start();
        }

        protected new void FixedUpdate()
        {
            if (gameStateManager.IsGamePaused || gameStateManager.IsPlayerMovementForbidden) { return; }
            if (transform.position.x == Mathf.Floor(transform.position.x) &&
                transform.position.y == Mathf.Floor(transform.position.y))
            {
                if (RandomChange())
                {
                    HandleChangeDirection();
                }
                else
                {
                    Rigidbody2d.MovePosition(Rigidbody2d.position + Speed * Time.deltaTime * Direction);
                }
            }
            else
            {
                Rigidbody2d.MovePosition(Rigidbody2d.position + Speed * Time.deltaTime * Direction);
            }
        }

        private bool RandomChange()
        {
            var random = new System.Random();
            var randomNumber = random.Next(0, 100);
            return randomNumber <= 25;
        }

        private void HandleChangeDirection()
        {
            Direction = ChooseRandomExceptCertainDirection(Direction);
            MoveToCenterOfTheCell();
            Rigidbody2d.MovePosition(Rigidbody2d.position + Speed * Time.deltaTime * Direction);
        }
    }
}