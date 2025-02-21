using Cubes.Infrastructure;
using UnityEngine;

namespace Cubes.View
{
    public class TowerZoneView : DropZone
    {
        [SerializeField] private Transform _startPosition;

        public Transform StartPosition { get => _startPosition; }
    }
}