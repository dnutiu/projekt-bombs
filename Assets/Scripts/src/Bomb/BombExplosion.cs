using src.Base;
using src.Managers;

namespace src.Bomb
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
