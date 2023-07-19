using Assets.Scripts.Core;
using Assets.Scripts.FactoryPattern;
using Assets.Scripts.UI;
using System;

namespace Assets.Scripts.MediatorPattern
{
    public class UIMediator : IDisposable
    {
        private Experience _playerExperience;
        private Health _playerHealth;
        private UIGameView _gameView;
        private UILoseView _loseView;
        private AbstractFactoryIcons _iconsFactory;
        public Action DoUpdateUIHealth;
        public Action DoUpdateUILevel;

        public UIMediator(Experience playerExperience, Health playerHealth, UIGameView gameView, UILoseView loseView, AbstractFactoryIcons iconsFactory)
        {
            _iconsFactory = iconsFactory;
            _playerExperience = playerExperience;
            _playerHealth = playerHealth;
            _gameView = gameView;
            _loseView = loseView;
            DoUpdateUIHealth = gameView.UpdateInfoHealthUI;
            DoUpdateUILevel = _gameView.UpdateInfoExperienceUI;
        }

        public int CurrentHealthValue => _playerHealth.CurrentValue;
        public int FullHealthValue => _playerHealth.FullHealthValue;
        public int ExperienceLevel => _playerExperience.LevelExperience;

        public void Initialize()
        {
            _gameView.UpdateInfoHealthUI();
            _gameView.UpdateInfoExperienceUI();
        }

        public void PlayerLose()
        {
            _gameView.HideWindow();
            _loseView.ShowLoseWindow();
        }

        public void RestartGame()
        {
            _playerHealth.HealthsReset();
            _playerExperience.ResetLevel();
            _loseView.HideLoseWindow();
            _gameView.ShowWindow();
            DoUpdateUIHealth?.Invoke();
            DoUpdateUILevel?.Invoke();
        }

        public void SetCoinImages()
        {
            _gameView.CoinImage.sprite = _iconsFactory.SpawnItem(ECoinIconStyle.CoinGameView);
            _loseView.CoinImage.sprite = _iconsFactory.SpawnItem(ECoinIconStyle.CoinLoseView);
        }

        public void Dispose()
        {
            DoUpdateUIHealth = null;
            DoUpdateUILevel = null;
        }
    }
}
