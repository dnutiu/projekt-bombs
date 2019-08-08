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

        protected new void FixedUpdate()
        {
            base.FixedUpdate();
        }

    }
}
