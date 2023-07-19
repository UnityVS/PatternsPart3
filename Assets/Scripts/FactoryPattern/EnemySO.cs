using Assets.Scripts.DecoratorPattern;
using Assets.Scripts.VisiterPattern;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.FactoryPattern
{
    [Serializable]
    [CreateAssetMenu(fileName = "EnemySO", menuName = "SO/EnemySO")]
    public class EnemySO : ScriptableObject
    {
        [SerializeField] private List<EnemyInfo> enemyList = new List<EnemyInfo>();
        public int EnemySOTypesCount => enemyList.Count;

        public void InitializeFactorySO(Dictionary<EEnemyType, EnemyInfo> enemyDictionary)
        {
            for (int i = 0; i < enemyList.Count; i++)
                enemyDictionary.Add(enemyList[i].EnemyType, enemyList[i]);
        }
    }

    [Serializable]
    public class EnemyInfo
    {
        [field: SerializeField] public EEnemyType EnemyType { get; private set; }
        [field: SerializeField] public Enemy EnemyPrefab { get; private set; }
        [field: SerializeField] public int Weight { get; private set; }
        [field: SerializeField] public Color EnemyColor { get; private set; }
        [field: SerializeField] public List<EnemyStats> EnemyStats { get; private set; }
        [field: SerializeField] public List<StatDecorator> EnemyStatsDecorator { get; private set; }
    }

    [Serializable]
    public class EnemyStats
    {
        [field: SerializeField] public EStatType EStateTypeValue { get; private set; }
        [field: SerializeField] public float StatValue { get; private set; }
    }
}
