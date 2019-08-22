using src.Base;
using UnityEngine;

namespace src.Enemy
{
    public class CollisionMovementEnemy : EnemyBase
    /* Enemy that will change direction only on collision. */
    {
        protected new void Start()
        {
            Speed = 4f;
            base.Start();
        }

        protected override void HandleMovement()
        {
            Rigidbody2d.MovePosition(Rigidbody2d.position + Speed * Time.deltaTime * Direction);
        }
    }
}
