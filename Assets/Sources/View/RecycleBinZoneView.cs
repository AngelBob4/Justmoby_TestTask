using Cubes.Infrastructure;
using UnityEngine;

namespace Cubes.View
{
    public class RecycleBinZoneView : DropZone
    {
        [SerializeField] private Transform _endPosition;

        public Transform EndPosition { get => _endPosition; }
    }
}