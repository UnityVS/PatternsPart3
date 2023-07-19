using System.Collections.Generic;

namespace Assets.Scripts.DecoratorPattern
{
    public interface IStatProvider
    {
        void MultiplyStats(EStatType field, float value);
        void AddStats(EStatType field, float value);
        void SubtractStats(EStatType field, float value);
        void ShowStats();
    }
}
