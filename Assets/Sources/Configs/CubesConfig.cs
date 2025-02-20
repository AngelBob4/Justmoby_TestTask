using Cubes.View;
using UnityEngine;

[CreateAssetMenu(fileName = "CubesConfig", menuName = "ScriptableObjects/CubesConfig")]
public class CubesConfig : ScriptableObject
{
    [field: SerializeField] public int Amount { get; private set; } = 20;
    [field: SerializeField] public CubeView CubeTemplate { get; private set; } = null;
}