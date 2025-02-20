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
        private readonly int _widthTowerZone = 100;
        private readonly int _heightTowerZone = 100;

        private RectTransform _towerZoneViewRectTransform;
        private Transform _towerContainer;
        private IdleZoneModel _idleZoneModel;
        private Transform _startPosition;
        private List<CubeView> _cubeViews = new List<CubeView>();

        public void Init(TowerZoneView towerZoneView, IdleZoneModel idleZoneModel, Transform towerContainer)
        {
            _towerContainer = towerContainer;
            _idleZoneModel = idleZoneModel;
            _startPosition = towerZoneView.StartPosition;

            if(towerZoneView.TryGetComponent(out RectTransform rectTransform))
                _towerZoneViewRectTransform = rectTransform;
        }

        public void TryAddCube(CubeView cubeView)
        {
            if (_cubeViews.Count < _maxCubesAmount)
            {
                _cubeViews.Add(cubeView);
                MoveCubeToTower(cubeView);
                ResetTowerZone();
            }
            else
            {
                _idleZoneModel.DestroyCube(cubeView);
            }
        }

        private void ResetTowerZone()
        {
            Vector2 newColliderSizes = new Vector2(
                _widthTowerZone, 
                _heightTowerZone + _cubeViews.Count * Constants.CubeWidth);

            _towerZoneViewRectTransform.sizeDelta = newColliderSizes;
            Vector3 offset = new Vector3(0, _cubeViews.Count * Constants.CubeWidth / 2, 0);
            _towerZoneViewRectTransform.transform.localPosition = _startPosition.localPosition + offset;
        }

        private void MoveCubeToTower(CubeView cubeView)
        {
            cubeView.transform.SetParent(_towerContainer);

            Vector3 endValue = new Vector3(
                _startPosition.localPosition.x,
                _startPosition.localPosition.y + (_cubeViews.Count - 1) * Constants.CubeWidth,
                _startPosition.localPosition.z);

            cubeView.transform.DOLocalMove(endValue, _moveDuration);
        }
    }
}