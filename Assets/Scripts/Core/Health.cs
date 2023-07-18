using Assets.Scripts.MediatorPattern;

namespace Assets.Scripts.Core
{
    public class Health
    {
        private int _maxValue;
        private UIMediator _uiMediator;

        public Health(int value) => CurrentValue = _maxValue = value;

        public int CurrentValue { get; private set; }

        public int FullHealthValue => _maxValue;

        public void Initialize(UIMediator uiMediator) => _uiMediator = uiMediator;

        public void AddHealth(int value)
        {
            CurrentValue += value;
            if (CurrentValue >= _maxValue)
                CurrentValue = _maxValue;
            _uiMediator.DoUpdateUIHealth?.Invoke();
        }

        public void SubtractHealth(int value)
        {
            CurrentValue -= value;
            if (CurrentValue < 0)
            {
                CurrentValue = 0;
                _uiMediator.PlayerLose();
            }
            _uiMediator.DoUpdateUIHealth?.Invoke();
        }

        public void HealthsReset() => CurrentValue = _maxValue;
    }
}
