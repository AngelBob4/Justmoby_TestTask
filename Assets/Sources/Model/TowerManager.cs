using Cubes.View;
using UnityEngine;
using DG.Tweening;
using Cubes.Infrastructure;
using System.Collections.Generic;

namespace Cubes.Model
{
    public class TowerManager
    {
        private readonly int _maxCubesAmount = 5;
        private readonly int _moveDuration = 2;

        private IdleZoneModel _idleZoneModel;
        private Transform _startPosition;
        private List<CubeView> _cubeViews = new List<CubeView>();

        public void Init(Transform startPosition, IdleZoneModel idleZoneModel)
        {
            _idleZoneModel = idleZoneModel;
            _startPosition = startPosition;
        }

        public void TryAddCube(CubeView cubeView)
        {
            if (_cubeViews.Count < _maxCubesAmount)
            {
                _cubeViews.Add(cubeView);

                Vector3 endValue = new Vector3(
                    _startPosition.position.x,
                    _startPosition.position.y + _cubeViews.Count * Constants.CubeWidth,
                    _startPosition.position.z);

                cubeView.transform.DOMove(endValue, _moveDuration);
            }
            else
            {
                _idleZoneModel.DestroyCube(cubeView);
            }
        }
    }
}