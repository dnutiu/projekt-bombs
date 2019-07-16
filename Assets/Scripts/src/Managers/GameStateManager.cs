namespace src.Managers
{
    public class GameStateManager
    {
        public static GameStateManager Instance { get; } = new GameStateManager();
        public bool IsGamePaused { get; internal set; }
        public bool IsPlayerMovementForbidden { get; internal set; }
        public int Level { get; private set; } = 1;


        public void IncreaseLevel()
        {
            Level += 1;
        }
        
        
    }
}