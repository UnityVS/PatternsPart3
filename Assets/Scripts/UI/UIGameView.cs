using Assets.Scripts.MediatorPattern;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.UI
{
    public class UIGameView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI playerLevelText;
        [SerializeField] private TextMeshProUGUI playerHPText;
        private UIMediator _uiMediator;

        public void Inititalize(UIMediator uiMediator) => _uiMediator = uiMediator;

        public void HideWindow() => gameObject.SetActive(false);

        public void ShowWindow() => gameObject.SetActive(true);

        public void UpdateInfoHealthUI() => playerHPText.text = $"Здоровье: {_uiMediator.CurrentHealthValue} из {_uiMediator.FullHealthValue}";
        public void UpdateInfoExperienceUI() => playerLevelText.text = $"Уровень: {_uiMediator.ExperienceLevel}";
    }
}
