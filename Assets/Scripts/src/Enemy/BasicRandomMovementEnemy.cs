using UnityEngine;

namespace src.Enemy
{
    public class EnemyThatRandomChangeDirection : EnemyBase
    {
        /* Enemy that will move randomly */
        protected new void Start()
        {
            Speed = 4f;
            base.Start();
        }

        protected new void FixedUpdate()
        {
            var pos = transform.position;
            var x = pos.x;
            var y = pos.y;
            if (gameStateManager.IsGamePaused || gameStateManager.IsPlayerMovementForbidden) { return; }
            if (x == Mathf.Floor(x) &&
                y == Mathf.Floor(y))
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