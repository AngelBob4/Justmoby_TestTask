using Cubes.Model;
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

    [Inject]
    private void Inject(
        CubesGenerator cubesGenerator,
        CubesStorage cubesStorage,
        DropZonesManager dropZonesManager)
    {
        cubesGenerator.Init(_cubesContainer, _canvas, dropZonesManager);
        cubesStorage.Init(cubesGenerator, _cubesConfig, _storageImages, dropZonesManager);
    }
}