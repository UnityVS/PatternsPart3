using System.Collections.Generic;
using System.Linq;

public enum EDecorationType
{
    MultiplyStats,
    AddStats,
    SubtractStats
}

public enum EStatType
{
    Agility,
    Intellect,
    Strength
}

namespace Assets.Scripts.DecoratorPattern
{
    public class StatProviderEnemyType : IStatProvider
    {
        private IStatProvider _statProvider;

        public StatProviderEnemyType(IStatProvider statProvider, Dictionary<EStatType, Dictionary<EDecorationType, float>> statsDictionary)
        {
            _statProvider = statProvider;
            foreach (var dictionary in statsDictionary)
            {
                EStatType eStatType = new();
                EDecorationType decorationType = new();
                float value = new();

                eStatType = dictionary.Key;
                decorationType = dictionary.Value.Keys.FirstOrDefault();
                value = dictionary.Value.Values.FirstOrDefault();

                switch (decorationType)
                {
                    case EDecorationType.MultiplyStats:
                        MultiplyStats(statsDictionary.Keys.FirstOrDefault(x => x == eStatType), value);
                        break;
                    case EDecorationType.AddStats:
                        AddStats(statsDictionary.Keys.FirstOrDefault(x => x == eStatType), value);
                        break;
                    case EDecorationType.SubtractStats:
                        SubtractStats(statsDictionary.Keys.FirstOrDefault(x => x == eStatType), value);
                        break;
                    default:
                        break;
                }
            }
        }
        public void MultiplyStats(EStatType field, float value) => _statProvider.MultiplyStats(field, value);

        public void AddStats(EStatType field, float value) => _statProvider.AddStats(field, value);

        public void SubtractStats(EStatType field, float value) => _statProvider.SubtractStats(field, value);


        public void ShowStats() { }
    }
}
