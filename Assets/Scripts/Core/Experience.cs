using Assets.Scripts.MediatorPattern;
using UnityEngine;

namespace Assets.Scripts.Core
{
    public class Experience
    {
        private const string _experience = "PlayerLevel";
        private int _levelExperience;
        private int _experienceValue;
        private int _experienceMaxToLevelUP = 5;
        private UIMediator _uiMediator;

        public int LevelExperience => _levelExperience;

        public Experience() => _levelExperience = PlayerPrefs.GetInt(_experience, 1);

        public void Initialize(UIMediator uiMediator) => _uiMediator = uiMediator;

        public void AddExperience(int value)
        {
            _experienceValue += value;
            if (_experienceValue >= _experienceMaxToLevelUP)
            {
                _experienceValue = 0;
                _levelExperience++;
            }
            _uiMediator.DoUpdateUILevel?.Invoke();
        }

        public void SubtractExperience(int value)
        {
            _experienceValue -= value;
            if (_experienceValue < _experienceMaxToLevelUP)
                _experienceValue = 0;
            _uiMediator.DoUpdateUILevel?.Invoke();
        }

        public void ResetLevel() => _levelExperience = PlayerPrefs.GetInt(_experience, 1);
    }
}
