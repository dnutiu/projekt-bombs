using System.Collections;
using Cinemachine;
using src.Base;
using src.Managers;
using UnityEngine;

namespace src.Ammo
{
    public class BombCameraShake : GameplayComponent
    {
        private BombsUtilManager _bombsUtilManager;
        public float amplitudeGain = 3f;
        public float frequencyGain = 3f;

        private int _currentlyShaking;
        private CinemachineVirtualCamera _virtualCamera;
        private CinemachineBasicMultiChannelPerlin _noiseMachine;
        // Start is called before the first frame update
        private void Start()
        {
            _bombsUtilManager = BombsUtilManager.instance;
            _virtualCamera = GetComponent<CinemachineVirtualCamera>();
            _noiseMachine = _virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        }

        public void StartCameraShakeCoro()
        {
            StartCoroutine(_startCameraShake());
        }
        private IEnumerator _startCameraShake()
        {
            SetCameraNoise(amplitudeGain, frequencyGain);
            yield return new WaitForSeconds(_bombsUtilManager.ExplosionDuration);
            SetCameraNoise(0, 0);
        }

        internal void SetCameraNoise(float amplitude, float frequency) {
            /* Fix camera shaking when multiple instances request shake at the same time. */
            if (amplitude > 0 || frequency > 0)
            {
                _currentlyShaking += 1;
            }
            else
            {
                _currentlyShaking -= 1;
            }
            if (_currentlyShaking > 0 && amplitude == 0f || frequencyGain == 0f)
            {
                return;
            }
            
            _noiseMachine.m_AmplitudeGain = amplitude;
            _noiseMachine.m_FrequencyGain = frequency;    
        }
    }
}
