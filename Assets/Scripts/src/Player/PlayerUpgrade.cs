namespace src.Player
{
    public class PlayerUpgrade
    {
        public delegate void IncreaseSpeedDelegate(float speed);
        public static PlayerUpgrade Instance = new PlayerUpgrade();
        public event IncreaseSpeedDelegate PlayerSpeed;
        public const float MaxPlayerSpeed = 8f;
        private float _movementSpeed = 4f;

        private PlayerUpgrade()
        {
            
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
            PlayerSpeed.Invoke(speed);
        }
    }
}