using Cubes.Infrastructure;
using UnityEngine;

public class TowerZoneView : DropZone
{
    [SerializeField] private Transform _startPosition;

    public Transform StartPosition { get => _startPosition;}
}