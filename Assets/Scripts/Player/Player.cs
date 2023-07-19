using Assets.Scripts.Core;
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