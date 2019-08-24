using src.Base;

namespace src.Player
{
    public class PlayerUpgrade : GameplayComponent
    {
        public delegate void IncreaseSpeedDelegate(float speed);
        public static PlayerUpgrade instance;
        public event IncreaseSpeedDelegate PlayerSpeed;
        public const float MaxPlayerSpeed = 8f;
        private float _movementSpeed = 4f;

        public void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else if (instance != null)
            {
                Destroy(gameObject);
            }
        }

        public float GetMovementSpeed()
        {
            return _movementSpeed;
        }
        
        public void IncreaseSpeed(float speed)
        {
            if (_movementSpeed >= MaxPlayerSpeed)
            {
                return;
            }
            _movementSpeed += speed;
            PlayerSpeed?.Invoke(speed);
        }
    }
}