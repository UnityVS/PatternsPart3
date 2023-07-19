using Assets.Scripts.DecoratorPattern;
using Assets.Scripts.FactoryPattern;
using System;
using UnityEngine;

namespace Assets.Scripts.VisiterPattern
{
    public class Enemy : MonoBehaviour, IEnemyFactory
    {
        private IStatProvider _statProvider;
        [field: SerializeField] public MeshRenderer MeshRendererComponent { get; private set; }
        public Action<Transform> DoTransformRelease { get; set; }
        public Action<int> DoWeightRelease { get; set; }
        public Action DoDie { get; set; }

        public void Initialize(IStatProvider statProvider) => _statProvider = statProvider;

        [ContextMenu("Die")] //in editor
        public void Die() => DoDie?.Invoke();
    }
}
