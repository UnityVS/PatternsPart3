using Assets.Scripts.MediatorPattern;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ECoinIconStyle
{
    CoinGameView,
    CoinLoseView
}

namespace Assets.Scripts.FactoryPattern
{
    public class AbstractFactoryIcons : MonoBehaviour
    {
        [SerializeField] private FactoryIconsCoinSO factorySO;
        private Dictionary<ECoinIconStyle, Sprite> _coinDicrionary = new Dictionary<ECoinIconStyle, Sprite>();
        private bool _dictionaryStatus => _coinDicrionary.Count == factorySO.CoinSOCount;
        private UIMediator _uiMediator;
        private WaitUntil _waitUntil;
        private Coroutine _coroutineInitWait;

        public Sprite GetIconStyle(ECoinIconStyle eCoinValue) => _coinDicrionary[eCoinValue];

        public void Initialize(UIMediator uiMediator)
        {
            _uiMediator = uiMediator;
            factorySO.InitializeFactorySO(_coinDicrionary);
            _waitUntil = new WaitUntil(() => _dictionaryStatus);

            if (_coroutineInitWait != null)
                StopCoroutine(_coroutineInitWait);

            _coroutineInitWait = StartCoroutine(WaitInitialize());
        }

        private void OnDestroy() => _coinDicrionary.Clear();


        private IEnumerator WaitInitialize()
        {
            yield return _waitUntil;
            _uiMediator.SetCoinImages();
        }

        public Sprite SpawnItem(ECoinIconStyle eCoinValue) => GetIconStyle(eCoinValue);
    }
}
