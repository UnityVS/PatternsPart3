using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.DecoratorPattern
{
    public class StatProvider : IStatProvider
    {
        Dictionary<EStatType, float> _statsDictionary = new();

        public StatProvider(Dictionary<EStatType, float> dictionary)
        {
            _statsDictionary = dictionary;
            ShowStats();
        }

        public void MultiplyStats(EStatType field, float value)
        {
            float newValue = _statsDictionary[field] * value;
            _statsDictionary[field] = newValue;
            Debug.Log($"Species | {field}: {newValue}");
        }

        public void AddStats(EStatType field, float value)
        {
            float newValue = _statsDictionary[field] + value;
            _statsDictionary[field] = newValue;
            Debug.Log($"Species | {field}: {newValue}");
        }

        public void SubtractStats(EStatType field, float value)
        {
            float newValue = _statsDictionary[field] - value;
            if (newValue < 0)
                newValue = 0;
            _statsDictionary[field] = newValue;
            Debug.Log($"Species | {field}: {newValue}");
        }

        public void ShowStats()
        {
            foreach (var item in _statsDictionary)
                Debug.Log($"Original stat | {item.Key}: {item.Value}");
        }
    }

    [Serializable]
    public class StatDecorator
    {
        [field: SerializeField] public EStatType EStateTypeValue { get; private set; }
        [field: SerializeField] public EDecorationType EDecorationType { get; private set; }
        [field: SerializeField] public float StatValue { get; private set; }
    }
}
