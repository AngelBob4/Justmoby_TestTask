using Cubes.View;
using System.Collections.Generic;
using UnityEngine.UI;

namespace Cubes.Model
{
    public class CubesStorage
    {
        private List<CubeView> _storage = new List<CubeView>();
        private List<Image> _storageCubesImages = new List<Image>();
        private List<Image> _storageImages = new List<Image>();
        private CubesGenerator _cubesGenerator;
        private DropZonesManager _dropZonesManager;

        public bool IsReplacingCube { get; private set; } = false;

        public void Init(
            CubesGenerator cubesGenerator, 
            CubesConfig config, 
            List<Image> storageImages,
            DropZonesManager dropZonesManager)
        {
            _dropZonesManager = dropZonesManager;
            _cubesGenerator = cubesGenerator;
            _storageImages = storageImages;

            _storage = _cubesGenerator.GenerateListOfCubes();

            foreach (CubeView cube in _storage)
            {
                cube.OnBeginDraging += OnCubeBeginDraging;
            }

            _dropZonesManager.CubeIsTaken += TurnOffStorage;
            _dropZonesManager.CubeIsDroped += TurnOnStorage;
        }

        ~CubesStorage()
        {
            _dropZonesManager.CubeIsTaken -= TurnOffStorage;
            _dropZonesManager.CubeIsDroped -= TurnOnStorage;
        }

        public void TurnOnStorage(CubeView _)
        {
            foreach (Image image in _storageImages)
                image.raycastTarget = true;

            foreach (CubeView cubeView in _storage)
                cubeView.TurnOnRaycasts();
        }

        public void TurnOffStorage(CubeView _)
        {
            foreach (Image image in _storageImages)
                image.raycastTarget = false;

            foreach (CubeView cubeView in _storage)
                cubeView.TurnOffRaycasts();
        }

        private void OnCubeBeginDraging(CubeView oldCube)
        {
            CubeView newCube = _cubesGenerator.GenerateCube(oldCube);
            _storage.Add(newCube);
            _storage.Remove(oldCube);

            oldCube.OnBeginDraging -= OnCubeBeginDraging;
            newCube.OnBeginDraging += OnCubeBeginDraging;
        }
    }
}