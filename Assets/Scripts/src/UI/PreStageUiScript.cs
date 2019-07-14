using src.Base;
using src.Managers;
using UnityEngine;
using UnityEngine.UI;

namespace src.UI
{
    public class PreStageUiScript : GameplayComponent
    {
        private readonly GameStateManager _gameStateManager = GameStateManager.Instance;
        private Text _stageText;
        
        public void Start()
        {
            _stageText = GetComponentInChildren<Text>();
#if UNITY_ANDROID || UNITY_IOS
            _stageText.fontSize = 50;
#endif
            _stageText.text = $"Stage {_gameStateManager.Level}";
        }
    }
}