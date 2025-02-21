using Cubes.Model;
using Reflex.Attributes;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Cubes.View
{
    public class ProjectBootstrap : MonoBehaviour
    {
        [Header("Cubes generation")]
        [SerializeField] private Transform _canvas;
        [SerializeField] private CubesConfig _cubesConfig;
        [SerializeField] private Transform _cubesContainer;
        [SerializeField] private Transform _towerContainer;
        [SerializeField] private List<Image> _storageImages;

        private CubesGenerator _cubesGenerator;
        private CubesStorage _cubesStorage;
        private DropZonesManager _dropZonesManager;
        private GameStorage _gameStorage;
        private TowerManager _towerManager;

        [Inject]
        private void Inject(
            CubesGenerator cubesGenerator,
            CubesStorage cubesStorage,
            DropZonesManager dropZonesManager,
            GameStorage gameStorage,
            TowerManager towerManager)
        {
            _cubesGenerator = cubesGenerator;
            _cubesStorage = cubesStorage;
            _dropZonesManager = dropZonesManager;
            _gameStorage = gameStorage;
            _towerManager = towerManager;
        }

        private void Awake()
        {
            _cubesGenerator.Init(_cubesContainer, _canvas, _dropZonesManager, _cubesConfig);
            _cubesStorage.Init(_cubesGenerator, _cubesConfig, _storageImages, _dropZonesManager);
            _gameStorage.Init(_cubesGenerator, _towerManager);
        }
    }
}