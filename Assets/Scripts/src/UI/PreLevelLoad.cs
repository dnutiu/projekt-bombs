using src.Managers;
using UnityEngine;
using UnityEngine.UI;

namespace src.UI
{
    public class PreLevelLoad : MonoBehaviour
    {
        private readonly GameStateManager _gameStateManager = GameStateManager.Instance;
        private Text _stageText;
        
        public void Start()
        {
            _stageText = GetComponentInChildren<Text>();
            _stageText.text = $"Stage {_gameStateManager.Level}";
        }
    }
}