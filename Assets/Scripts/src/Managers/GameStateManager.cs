using src.Base;
using UnityEngine;

namespace src.Managers
{
    public class GameStateManager : GameplayComponent
    {
        public static GameStateManager instance;
        public bool IsGamePaused { get; internal set; }
        public bool IsPlayerMovementForbidden { get; internal set; }
        public int Level { get; private set; } = 1;
        
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

        public void IncreaseLevel()
        {
            Level += 1;
        }
        
        
    }
}