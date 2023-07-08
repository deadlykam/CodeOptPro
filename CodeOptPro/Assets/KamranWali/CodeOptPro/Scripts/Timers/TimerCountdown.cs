using UnityEngine;

namespace KamranWali.CodeOptPro.Timers
{
    public class TimerCountdown : MonoBehaviour, ITimer
    {
        private float _time = 1f;
        private float _currentTime = -1f;

        public bool IsTimerDone() => _currentTime == _time;
        public float GetNormalValue() => _currentTime / _time;
        public float GetCurrentTime() => _currentTime;
        public void ResetTimer() => _currentTime = 0f;
        public void StopTimer() => _currentTime = _time;
        public float GetSetupTimeSeconds() => _time;

        public void StartSetup(float timeSeconds)
        {
            _time = timeSeconds;
            _currentTime = timeSeconds;
        }

        public void UpdateTimer(float simSpeed)
        {
            if (_currentTime != _time)
                _currentTime =
                    _currentTime + (simSpeed * Time.deltaTime) >= _time ?
                    _time : _currentTime + (simSpeed * Time.deltaTime);
        }
    }
}