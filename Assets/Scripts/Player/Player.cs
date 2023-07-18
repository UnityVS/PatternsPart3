using Assets.Scripts.Core;
using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private PlayerSO playerConfig;
    private Health _health;
    private Experience _experience;

    public void Initialize()
    {
        _experience = new Experience();
        _health = new Health(playerConfig.Health);
    }

    public Health PlayerHealth => _health;

    public Experience PlayerExperience => _experience;
}

[Serializable]
[CreateAssetMenu(fileName = "PlayerSO", menuName = "SO/PlayerSO")]
public class PlayerSO : ScriptableObject
{
    [field: SerializeField, Range(1, 50)] public int Health { get; private set; }
}
