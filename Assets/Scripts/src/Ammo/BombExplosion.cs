using src.Base;
using src.Helpers;
using src.Managers;

namespace src.Ammo
{
    public class BombExplosion : GameplayComponent
    {
        private readonly BombsUtilManager _bombUtil = BombsUtilManager.instance;

        public void Start()
        {
            Destroy(gameObject, _bombUtil.explosionDuration);
        }
    }
}
