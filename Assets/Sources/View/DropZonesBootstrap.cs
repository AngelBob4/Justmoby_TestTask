using Cubes.Model;
using Cubes.Presenter;
using Reflex.Attributes;
using UnityEngine;

namespace Cubes.View
{
    public class DropZonesBootstrap : MonoBehaviour
    {
        [SerializeField] private Transform _towerContainer;

        [Header("Zone views")]
        [SerializeField] private IdleZoneView _idleZoneView;
        [SerializeField] private TowerZoneView _towerZoneView;
        [SerializeField] private RecycleBinZoneView _recycleBinZoneView;

        [SerializeField] private RectTransform _towerZonePanel;

        private TowerManagerPresenter _towerManagerPresenter;
        private IdleZonePresenter _idleZonePresenter;
        private RecycleBinPresenter _recycleBinPresenter;

        [Inject]
        private void Inject(
            TowerManager towerManager,
            IdleZoneModel idleZoneModel,
            RecycleBinModel recycleBinModel,
            CubesStorage cubesStorage,
            DropZonesManager dropZonesManager,
            ConsoleModel consoleModel,
            GameStorage gameStorage)
        {
            _towerManagerPresenter = new TowerManagerPresenter(_towerZoneView, towerManager);
            _idleZonePresenter = new IdleZonePresenter(_idleZoneView, idleZoneModel);
            _recycleBinPresenter = new RecycleBinPresenter(recycleBinModel, _recycleBinZoneView);

            recycleBinModel.Init(_recycleBinZoneView.EndPosition, consoleModel);
            idleZoneModel.Init(consoleModel);
            towerManager.Init(_towerZoneView, idleZoneModel, _towerContainer, dropZonesManager, _towerZonePanel, consoleModel, gameStorage);
        }

        private void Awake()
        {
            _towerZoneView.Init(_towerManagerPresenter);
            _idleZoneView.Init(_idleZonePresenter);
            _recycleBinZoneView.Init(_recycleBinPresenter);
        }
    }
}