using Cubes.View;
using System;
using UnityEngine;

public class DropZonesManager : MonoBehaviour
{
    public event Action<CubeView> CubeIsTaken;
    public event Action<CubeView> CubeIsDroped;

    private bool _towerIsBuilding = false;
    private bool _isCubeTaken = false;

    public void TakeCube(CubeView cubeView)
    {
        _isCubeTaken = true;
        CubeIsTaken?.Invoke(cubeView);
    }

    public void DropCube(CubeView cubeView)
    {
        _isCubeTaken = false;

        if (_towerIsBuilding)
            return;

        CubeIsDroped?.Invoke(cubeView);
    }

    public void StartBuildingCube()
    {
        _towerIsBuilding = true;
    }

    public void EndBuildingCube()
    {
        _towerIsBuilding = false;

        if(_isCubeTaken == false)
            CubeIsDroped?.Invoke(null);
    }

    public void GetNewCube(CubeView cubeView)
    {
        cubeView.OnBeginDraging += TakeCube;
        cubeView.OnEndDraging += DropCube;
        cubeView.OnCubeDestroy += OnCubeDestroy;
    }

    public void OnCubeDestroy(CubeView cubeView)
    {
        cubeView.OnBeginDraging -= TakeCube;
        cubeView.OnEndDraging -= DropCube;
        cubeView.OnCubeDestroy -= OnCubeDestroy;
    }
}