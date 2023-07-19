using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.FactoryPattern
{
    [Serializable]
    [CreateAssetMenu(fileName = "FactorySO", menuName = "SO/FactorySO")]
    public class FactoryIconsCoinSO : ScriptableObject
    {
        [SerializeField] private List<CoinSOType> coinSOList = new List<CoinSOType>();
        public int CoinSOCount => coinSOList.Count;

        public void InitializeFactorySO(Dictionary<ECoinIconStyle, Sprite> coinDicrionary)
        {
            for (int i = 0; i < coinSOList.Count; i++)
                coinDicrionary.Add(coinSOList[i].ECoinStyle, coinSOList[i].CoinSprite);
        }
    }

    [Serializable]
    public class CoinSOType
    {
        [field: SerializeField] public Sprite CoinSprite { get; private set; }
        [field: SerializeField] public ECoinIconStyle ECoinStyle { get; private set; }
    }
}
