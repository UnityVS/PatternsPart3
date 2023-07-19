using Assets.Scripts.FactoryPattern;
using Assets.Scripts.MediatorPattern;
using Assets.Scripts.UI;
using UnityEngine;

namespace Assets.Scripts.Core
{
    public class Bootstrapper : MonoBehaviour
    {
        [SerializeField] private Player player;
        [SerializeField] private UIGameView gameView;
        [SerializeField] private UILoseView loseView;
        [SerializeField] private AbstractFactoryIcons abstractFactoryIcons;
        [SerializeField] private EnemyFactory enemyFactory;
        private Experience _playerExperience;
        private Health _playerHealth;

        private void Awake()
        {
            player.Initialize();
            _playerExperience = player.PlayerExperience;
            _playerHealth = player.PlayerHealth;
            UIMediator uiMediator = new UIMediator(_playerExperience, _playerHealth, gameView, loseView, abstractFactoryIcons);
            gameView.Inititalize(uiMediator);
            loseView.Inititalize(uiMediator);
            _playerExperience.Initialize(uiMediator);
            _playerHealth.Initialize(uiMediator);
            abstractFactoryIcons.Initialize(uiMediator);
            enemyFactory.Initialize();
            uiMediator.Initialize();
        }

        [ContextMenu("Damaged")] //in editor
        public void PlayerDamaged() => _playerHealth.SubtractHealth(10);

        [ContextMenu("AddExperience")] //in editor
        public void AddExperience() => _playerExperience.AddExperience(3);
    }
}
