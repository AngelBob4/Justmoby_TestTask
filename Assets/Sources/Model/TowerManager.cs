using Cubes.View;
using UnityEngine;
using DG.Tweening;
using Cubes.Infrastructure;
using System.Collections.Generic;

namespace Cubes.Model
{
    public class TowerManager
    {
        private readonly float _startMovingDuration = 0.6f;
        private readonly float _endMovingDuration = 0.4f;
        private readonly int _widthTowerZone = 100;
        private readonly int _heightTowerZone = 100;

        private int _maxCubesAmount = 5;

        private RectTransform _towerZoneViewRectTransform;
        private Transform _towerContainer;
        private IdleZoneModel _idleZoneModel;
        private DropZonesManager _dropZonesManager;
        private Transform _startPosition;
        private List<CubeView> _cubeViews = new List<CubeView>();
        private ConsoleModel _consoleModel;

        public void Init(
            TowerZoneView towerZoneView, 
            IdleZoneModel idleZoneModel, 
            Transform towerContainer,
            DropZonesManager dropZonesManager,
            RectTransform towerZonePanel,
            ConsoleModel consoleModel)
        {
            _consoleModel = consoleModel;
            _maxCubesAmount = (int)(towerZonePanel.sizeDelta.y / Constants.CubeWidth);
            _dropZonesManager = dropZonesManager;
            _towerContainer = towerContainer;
            _idleZoneModel = idleZoneModel;
            _startPosition = towerZoneView.StartPosition;

            if(towerZoneView.TryGetComponent(out RectTransform rectTransform))
                _towerZoneViewRectTransform = rectTransform;

            _dropZonesManager.CubeIsTaken += TurnOffCubesRaycasts;
            _dropZonesManager.CubeIsTaken += DeleteCubeFromTower;
            _dropZonesManager.CubeIsDroped += TurnOnCubesRaycasts;
        }

        ~TowerManager()
        {
            _dropZonesManager.CubeIsTaken -= TurnOffCubesRaycasts;
            _dropZonesManager.CubeIsTaken -= DeleteCubeFromTower;
            _dropZonesManager.CubeIsDroped -= TurnOnCubesRaycasts;
        }

        public void TryAddCube(CubeView cubeView)
        {
            if (_cubeViews.Count < _maxCubesAmount)
            {
                _consoleModel.WriteToConsole(TypeOfText.CubeInstallation);
                cubeView.SetTower();
                _cubeViews.Add(cubeView);
                MoveCubeToTower(cubeView);
                ResetTowerZone();
            }
            else
            {
                _idleZoneModel.DestroyCube(cubeView);
                _consoleModel.WriteToConsole(TypeOfText.HeightLimit);
            }
        }

        public void TurnOffCubesRaycasts(CubeView _)
        {
            foreach (CubeView cube in _cubeViews)
                cube.TurnOffRaycasts();
        }

        public void TurnOnCubesRaycasts(CubeView _)
        {
            foreach (CubeView cube in _cubeViews)
                cube.TurnOnRaycasts();
        }

        private void DeleteCubeFromTower(CubeView cubeView)
        {
            if (cubeView == null || cubeView.IsInTower == false)
                return;

            int cubeIndex = _cubeViews.IndexOf(cubeView);
            _cubeViews.Remove(cubeView);
            ResetCubesInTower(cubeIndex);
        }

        private void ResetCubesInTower(int index)
        {
            ResetTowerZone();

            for (int i = 0; i < _cubeViews.Count; i++)
            {
                if (i >= index)
                {
                    MoveCubeToTower(_cubeViews[i]);
                }
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
            _dropZonesManager.StartBuildingCube();
            cubeView.transform.SetParent(_towerContainer);
            int randomWidth = Random.Range(-Constants.CubeWidth / 4, (Constants.CubeWidth / 4) + 1);

            Vector3 endValue = new Vector3(
                _startPosition.localPosition.x + randomWidth,
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
                .OnComplete(() => _dropZonesManager.EndBuildingCube());

            sequence.Play();
        }
    }
}