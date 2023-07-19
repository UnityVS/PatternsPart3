using System;
using UnityEngine;

namespace Assets.Scripts.FactoryPattern
{
    public interface IEnemyFactory
    {
        public Action<Transform> DoTransformRelease { get; set; }
        public Action<int> DoWeightRelease { get; set; }
        public Action DoDie { get; set; }
    }
}
