namespace Assets.Scripts.src.Enemy
{
    public class EnemyThatChangeDirectionOnlyOnCollision : EnemyBase
    {

        //Momentan lasam asa, o sa difere probabil la animatii and stats, nu stiu sigur
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
