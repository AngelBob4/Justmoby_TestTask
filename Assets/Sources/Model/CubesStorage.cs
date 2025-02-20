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
        private TowerManager _towerManager;

        public CubesStorage(
            CubesGenerator cubesGenerator, 
            CubesConfig config, 
            List<Image> storageImages,
            TowerManager towerManager)
        {
            _towerManager = towerManager;
            _cubesGenerator = cubesGenerator;
            _storageImages = storageImages;

            _storage = _cubesGenerator.GenerateListOfCubes(config);

            foreach (CubeView cube in _storage)
            {
                cube.OnBeginDraging += OnCubeBeginDraging;
                cube.OnEndDraging += OnCubeEndedDraging;

                if (cube.TryGetComponent(out Image image))
                    _storageCubesImages.Add(image);
            }
        }

        private void OnCubeBeginDraging(CubeView oldCube)
        {
            CubeView newCube = _cubesGenerator.GenerateCube(oldCube);

            if (newCube == null)
                return;

            oldCube.OnBeginDraging -= OnCubeBeginDraging;
            newCube.OnBeginDraging += OnCubeBeginDraging;
            newCube.OnEndDraging += OnCubeEndedDraging;

            oldCube.TryGetComponent(out Image oldImage);
            newCube.TryGetComponent(out Image newImage);
            _storageCubesImages.Remove(oldImage);
            _storageCubesImages.Add(newImage);

            foreach (Image image in _storageImages)
            {
                image.raycastTarget = false;
            }

            foreach (Image image in _storageCubesImages)
            {
                image.raycastTarget = false;
            }

            _towerManager.TurnOffCubesRaycasts();
        }

        private void OnCubeEndedDraging(CubeView cube)
        {
            cube.OnEndDraging -= OnCubeEndedDraging;

            foreach (Image image in _storageImages)
            {
                image.raycastTarget = true;
            }

            foreach (Image image in _storageCubesImages)
            {
                image.raycastTarget = true;
            }

            _towerManager.TurnOnCubesRaycasts();
        }
    }
}