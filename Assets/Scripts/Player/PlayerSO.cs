using System;
using UnityEngine;


[Serializable]
[CreateAssetMenu(fileName = "PlayerSO", menuName = "SO/PlayerSO")]
public class PlayerSO : ScriptableObject
{
    [field: SerializeField, Range(1, 50)] public int Health { get; private set; }
}
