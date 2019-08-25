using src.Base;

namespace src.Player
{
    public class PlayerUpgrade : GameplayComponent
    {
        /* Events & Delegates */
        public delegate void IncreaseSpeedDelegate(float speed);
        public event IncreaseSpeedDelegate PlayerSpeed;
        
        /* Variables */
        public const float MaxPlayerSpeed = 8f;
        private float _movementSpeed = 4f;

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