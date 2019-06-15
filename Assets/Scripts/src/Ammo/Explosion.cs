using src.Base;
using src.Managers;

namespace src.Ammo
{
    public class Explosion : GameplayComponent
    {
        private readonly BombsUtilManager _bombUtil = BombsUtilManager.Instance;

        public void Start()
        {
            Destroy(gameObject, _bombUtil.ExplosionDuration);
        }
    }
}
