using Assets.Scripts.MediatorPattern;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class UILoseView : MonoBehaviour
    {
        [SerializeField] private Button restartButton;
        [SerializeField] private GameObject loseWindow;
        private UIMediator _uiMediator;

        public void Inititalize(UIMediator uiMediator) => _uiMediator = uiMediator;

        private void OnEnable() => restartButton.onClick.AddListener(_uiMediator.RestartGame);

        private void OnDisable() => restartButton.onClick.RemoveListener(_uiMediator.RestartGame);

        public void ShowLoseWindow() => loseWindow.SetActive(true);

        public void HideLoseWindow() => loseWindow.SetActive(false);
    }
}
