using src.Base;
using UnityEngine;

namespace src.Enemy.Dummy
{
    //This kind of enemy is used just for testing purposes
    //This enemy will go just in one direction or stays in place
    //To make it stay in place, don't assign any direction in OneDirection slot or assign Vector2.zero
    public class OneDirectionEnemy : EnemyBase
    {
        protected new void Start()
        {
            Speed = 4.0f;
            Rigidbody2d = GetComponent<Rigidbody2D>();
        }

        protected override void HandleMovement()
        {
        }
    }
}
