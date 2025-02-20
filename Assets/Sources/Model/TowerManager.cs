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
        private readonly float _startMovingDuration = 0.6f;
        private readonly float _endMovingDuration = 0.4f;
        private readonly int _widthTowerZone = 100;
        private readonly int _heightTowerZone = 100;

        private RectTransform _towerZoneViewRectTransform;
        private Transform _towerContainer;
        private IdleZoneModel _idleZoneModel;
        private CubesStorage _cubesStorage;
        private Transform _startPosition;
        private List<CubeView> _cubeViews = new List<CubeView>();

        public void Init(
            TowerZoneView towerZoneView, 
            IdleZoneModel idleZoneModel, 
            Transform towerContainer,
            CubesStorage cubesStorage)
        {
            _cubesStorage = cubesStorage;
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
                cubeView.SetTower();
                _cubeViews.Add(cubeView);
                MoveCubeToTower(cubeView);
                ResetTowerZone();
                cubeView.OnEndDraging += DeleteCubeFromTower;
            }
            else
            {
                _idleZoneModel.DestroyCube(cubeView);
            }
        }

        public void TurnOffCubesRaycasts()
        {
            foreach (CubeView cube in _cubeViews)
                cube.TurnOffRaycasts();
        }

        public void TurnOnCubesRaycasts()
        {
            if (_cubesStorage.IsReplacingCube)
                return;

            foreach (CubeView cube in _cubeViews)
                cube.TurnOnRaycasts();
        }

        private void DeleteCubeFromTower(CubeView cubeView)
        {
            if (cubeView.IsInTower)
                return;

            int cubeIndex = _cubeViews.IndexOf(cubeView);
            _cubeViews.Remove(cubeView);
            cubeView.OnEndDraging -= DeleteCubeFromTower;
            ResetCubesInTower(cubeIndex);
        }

        private void ResetCubesInTower(int index)
        {
            TurnOffCubesRaycasts();

            for (int i = 0; i < _cubeViews.Count; i++)
            {
                if (i >= index)
                {
                    MoveCubeToTower(_cubeViews[i]);
                }
            }

            TurnOnCubesRaycasts();
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
            cubeView.TurnOffRaycasts();

            Vector3 endValue = new Vector3(
                _startPosition.localPosition.x,
                //(_cubeViews.Count - 1) * Constants.CubeWidth,
                (_cubeViews.IndexOf(cubeView)) * Constants.CubeWidth,
                _startPosition.localPosition.z);
            Vector3 topPoint = new Vector3(0, 100f, 0);
            Vector3 rotation = new Vector3(0, 0, 90f);

            cubeView.transform.DORotate(rotation, _startMovingDuration + _endMovingDuration);

            Sequence sequence = DOTween.Sequence();

            sequence.Append(cubeView.transform.DOLocalMove(topPoint + endValue, _startMovingDuration))
                .SetEase(Ease.OutSine);
            sequence.Append(cubeView.transform.DOLocalMove(endValue, _endMovingDuration))
                .SetEase(Ease.InSine)
                .OnComplete(() => TurnOnCubesRaycasts());

            sequence.Play();
        }
    }
}