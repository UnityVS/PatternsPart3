using Assets.Scripts.DecoratorPattern;
using Assets.Scripts.VisiterPattern;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public enum EEnemyType
{
    Ork,
    Elf,
    Human,
    Fly,
    Rock,
    Silverman,
    Dogman,
    Hunterman,
    Wizardman
}

namespace Assets.Scripts.FactoryPattern
{
    public class EnemyFactory : MonoBehaviour
    {
        [SerializeField] private List<Transform> spawnPoints = new();
        [SerializeField] private EnemySO enemySO;
        [SerializeField, Min(0)] private int _maxWeight = 50;
        [SerializeField, Min(0)] private float _delayTime = 0.2f;
        List<EEnemyType> _availableEnemyTypes;
        private Dictionary<Transform, bool> _availablePoints = new();
        private Coroutine _coroutine;
        private Coroutine _coroutineInitWait;
        private WaitForSeconds _delay;
        private WaitUntil _waitUntil;
        private int _currentWeight = 0;
        private bool _isStopGeneration = true;

        private Dictionary<EEnemyType, EnemyInfo> _enemyDicrionary = new Dictionary<EEnemyType, EnemyInfo>();
        private bool _dictionaryStatus => _enemyDicrionary.Count == enemySO.EnemySOTypesCount;

        private void OnDestroy() => _enemyDicrionary.Clear();

        public void Initialize()
        {
            enemySO.InitializeFactorySO(_enemyDicrionary);
            _delay = new WaitForSeconds(_delayTime);
            _waitUntil = new WaitUntil(() => _dictionaryStatus);

            foreach (Transform t in spawnPoints)
                _availablePoints.Add(t, true);

            if (_coroutineInitWait != null)
                StopCoroutine(_coroutineInitWait);

            _coroutineInitWait = StartCoroutine(WaitInitialize());
        }

        public EnemyInfo GetEnemy(EEnemyType eEnemyValue) => _enemyDicrionary[eEnemyValue];

        private IEnumerator WaitInitialize()
        {
            yield return _waitUntil;
            if (_coroutine != null)
                StopCoroutine(_coroutine);

            GetAvailableEnemyTypes();
            _coroutine = StartCoroutine(GeneratingEnemyes());
        }

        private void ReleasePoint(Transform transformValue) => _availablePoints.Add(transformValue, true);

        private void AddWeight(int weightValue)
        {
            _currentWeight += weightValue;
            if (_currentWeight > _maxWeight)
                _currentWeight = _maxWeight;
        }

        public void GetAvailableEnemyTypes()
        {
            List<EEnemyType> newList = new List<EEnemyType>();
            foreach (var item in _enemyDicrionary)
                newList.Add(item.Key);

            _availableEnemyTypes = newList;
        }

        private void SubtractWeight(int weightValue)
        {
            _currentWeight -= weightValue;
            if (_currentWeight < 0)
                _currentWeight = 0;
        }

        private Transform GetFreePoint()
        {
            System.Random random = new System.Random();

            IEnumerable<Transform> availableTransforms = _availablePoints
                .Where(pair => pair.Value)
                .Select(pair => pair.Key);

            if (availableTransforms.Any())
            {
                int randomIndex = random.Next(availableTransforms.Count());
                Transform availableTransform = availableTransforms.ElementAt(randomIndex);
                Debug.Log($"Availiable points count: {_availablePoints.Count(item => item.Value)}");
                return availableTransform;
            }

            Debug.Log("No available transforms.");
            return null;
        }

        private IEnumerator GeneratingEnemyes()
        {
            while (true || _isStopGeneration)
            {
                EEnemyType enemyType = _availableEnemyTypes[Random.Range(0, _availableEnemyTypes.Count)];
                Debug.Log($"Current weight: {_currentWeight}");

                if (GetEnemy(enemyType).Weight + _currentWeight >= _maxWeight)
                {
                    yield return _delay;
                    continue;
                }

                Transform newFreePoint = GetFreePoint();

                if (newFreePoint == null)
                {
                    yield return _delay;
                    continue;
                }

                AddWeight(GetEnemy(enemyType).Weight);
                _availablePoints.Remove(newFreePoint);
                Enemy newEnemy = Instantiate(GetEnemy(enemyType).EnemyPrefab, newFreePoint.position, GetEnemy(enemyType).EnemyPrefab.gameObject.transform.rotation, null);
                newEnemy.MeshRendererComponent.material.color = GetEnemy(enemyType).EnemyColor;
                Dictionary<EStatType, Dictionary<EDecorationType, float>> decoratorInfo = new();
                Dictionary<EStatType, float> statProviderDictionary = new();
                foreach (var item in GetEnemy(enemyType).EnemyStats)
                    statProviderDictionary.Add(item.EStateTypeValue, item.StatValue);

                string decorator = "";
                foreach (var item in GetEnemy(enemyType).EnemyStatsDecorator)
                {
                    Dictionary<EDecorationType, float> keyValuePairs = new()
                    {
                        { item.EDecorationType, item.StatValue }
                    };
                    decoratorInfo.Add(item.EStateTypeValue, keyValuePairs);
                    decorator += $"{item.EDecorationType} {item.EStateTypeValue}: {item.StatValue} |";
                }
                newEnemy.Initialize(new StatProviderEnemyType(new StatProvider(statProviderDictionary), decoratorInfo));
                Debug.Log($"Enemy: {enemyType} | {decorator}");
                Debug.Log($"================================");

                newEnemy.DoTransformRelease = ReleasePoint;
                newEnemy.DoWeightRelease = SubtractWeight;
                newEnemy.DoDie += delegate { newEnemy.DoTransformRelease?.Invoke(newFreePoint); };
                newEnemy.DoDie += delegate { newEnemy.DoWeightRelease?.Invoke(GetEnemy(enemyType).Weight); Debug.Log($"+{GetEnemy(enemyType).Weight} Weight / Current weight: {_currentWeight}"); Destroy(newEnemy.gameObject); };
                yield return _delay;
            }
        }
    }
}
