using Assets.Scripts.MediatorPattern;
using Assets.Scripts.UI;
using UnityEngine;

namespace Assets.Scripts.Core
{
    public class Bootstrapper : MonoBehaviour
    {
        [SerializeField] private Player _player;
        [SerializeField] private UIGameView _gameView;
        [SerializeField] private UILoseView _loseView;
        private Experience _playerExperience;
        private Health _playerHealth;

        private void Awake()
        {
            _player.Initialize();
            _playerExperience = _player.PlayerExperience;
            _playerHealth = _player.PlayerHealth;
            UIMediator uiMediator = new UIMediator(_playerExperience, _playerHealth, _gameView, _loseView);
            _gameView.Inititalize(uiMediator);
            _loseView.Inititalize(uiMediator);
            _playerExperience.Initialize(uiMediator);
            _playerHealth.Initialize(uiMediator);
            uiMediator.Initialize();
        }

        [ContextMenu("Damaged")] //in editor
        public void PlayerDamaged() => _playerHealth.SubtractHealth(10);

        [ContextMenu("AddExperience")] //in editor
        public void AddExperience() => _playerExperience.AddExperience(3);
    }
}
