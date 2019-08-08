using UnityEngine;

namespace Assets.Scripts.src.Enemy.Dummy
{
    //This kind of enemy is used just for testing purposes
    //This enemy will go just in one direction or stays in place
    //To make it stay in place, don't assign any direction in OneDirection slot or assign Vector2.zero
    public class OneDirectionEnemy : EnemyBase
    {
        public Vector2 OneDirection = Vector2.zero;

        protected new void Start()
        {
            Speed = 4.0f;
            Rigidbody2d = GetComponent<Rigidbody2D>();
        }

        protected new void FixedUpdate()
        {
            if (OneDirection != Vector2.zero)
            {
                if (gameStateManager.IsGamePaused || gameStateManager.IsPlayerMovementForbidden) { return; }
                Rigidbody2d.MovePosition(Rigidbody2d.position + OneDirection * Speed * Time.deltaTime);
            }
        }
    }
}
