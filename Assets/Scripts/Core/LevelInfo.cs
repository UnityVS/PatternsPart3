using UnityEngine;

namespace Assets.Scripts.Core
{
    public class LevelInfo
    {
        private const string _levelValue = "LevelValue";

        public LevelInfo() => Level = PlayerPrefs.GetInt(_levelValue);

        public int Level { get; private set; }
    }
}
