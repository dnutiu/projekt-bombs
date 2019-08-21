using System;
using src.Base;
using UnityEngine;

namespace src.Enemy
{
    public class RandomMovementEnemy : EnemyBase
    {
        /* Enemy that will move randomly */
        protected new void Start()
        {
            Speed = 4f;
            base.Start();
        }

        protected override void HandleMovement()
        {
            var pos = transform.position;
            var x = pos.x;
            var y = pos.y;
            if (Math.Abs(x - Mathf.Floor(x)) < 0.1 && Math.Abs(y - Mathf.Floor(y)) < 0.1)
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