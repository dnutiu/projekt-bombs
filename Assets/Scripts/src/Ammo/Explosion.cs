using src.Base;
using src.Managers;

namespace src.Ammo
{
    public class Explosion : GameplayComponent
    {
        private readonly BombsUtilManager _bombUtil = BombsUtilManager.instance;

        public void Start()
        {
            Destroy(gameObject, _bombUtil.ExplosionDuration);
        }
    }
}
