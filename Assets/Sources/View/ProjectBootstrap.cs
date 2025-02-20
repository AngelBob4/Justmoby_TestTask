using Cubes.Model;
using Cubes.Presenter;
using Reflex.Attributes;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProjectBootstrap : MonoBehaviour
{
    [Header("Cubes generation")]
    [SerializeField] private Transform _canvas;
    [SerializeField] private CubesConfig _cubesConfig;
    [SerializeField] private Transform _cubesContainer;
    [SerializeField] private Transform _towerContainer;
    [SerializeField] private List<Image> _storageImages;

    [Header("Zone views")]
    [SerializeField] private IdleZoneView _idleZoneView;
    [SerializeField] private TowerZoneView _towerZoneView;
    [SerializeField] private RecycleBinZoneView _recycleBinZoneView;

    private TowerManagerPresenter _towerManagerPresenter;
    private IdleZonePresenter _idleZonePresenter;
    private RecycleBinPresenter _recycleBinPresenter;

    private CubesStorage _cubesStorage;

    [Inject]
    private void Inject(
        CubesGenerator cubesGenerator,
        TowerManager towerManager,
        IdleZoneModel idleZoneModel,
        RecycleBinModel recycleBinModel)
    {
        cubesGenerator.Init(_cubesContainer, _canvas);
        _cubesStorage = new CubesStorage(cubesGenerator, _cubesConfig, _storageImages, towerManager);
        _towerManagerPresenter = new TowerManagerPresenter(
            _towerZoneView, 
            towerManager, 
            idleZoneModel, 
            _towerContainer,
            _cubesStorage);
        _idleZonePresenter = new IdleZonePresenter(_idleZoneView, idleZoneModel);
        _recycleBinPresenter = new RecycleBinPresenter(recycleBinModel, _recycleBinZoneView);
    }

    private void Awake()
    {
        _towerZoneView.Init(_towerManagerPresenter);
        _idleZoneView.Init(_idleZonePresenter);
        _recycleBinZoneView.Init(_recycleBinPresenter);
    }
}