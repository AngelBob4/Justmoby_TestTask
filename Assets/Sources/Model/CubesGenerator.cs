using Cubes.Infrastructure;
using Cubes.View;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CubesGenerator
{
    private Transform _container;
    private Transform _canvas;
    private DropZonesManager _dropZonesManager;
    private CubesConfig _config;

    public void Init(Transform container, Transform canvas, DropZonesManager dropZonesManager, CubesConfig config)
    {
        _config = config;
        _dropZonesManager = dropZonesManager;
        _container = container;
        _canvas = canvas;
    }

    public List<CubeView> GenerateListOfCubes()
    {
        if (_canvas == null && _container == null)
            return null;

        List<CubeView> generatedCubes = new List<CubeView>();
        int cubesAmount = _config.Amount;
        CubeView template = _config.CubeTemplate;

        for (int i = 0; i < cubesAmount; i++)
        {
            CubeView newCube = Object.Instantiate(template);
            newCube.transform.SetParent(_container);
            newCube.Init(_canvas);

            if (newCube.TryGetComponent(out RectTransform rectTransform))
            {
                rectTransform.sizeDelta = new Vector2(Constants.CubeWidth, Constants.CubeWidth);
            }

            if (newCube.TryGetComponent(out Image cubeImage))
            {
                Color randomColor = new Color(Random.value, Random.value, Random.value);
                cubeImage.color = randomColor;
            }

            generatedCubes.Add(newCube);
            _dropZonesManager.GetNewCube(newCube);
        }

        return generatedCubes;
    }

    public CubeView GenerateCube(CubeView originalCube)
    {
        if (_canvas == null && _container == null)
            return null;

        CubeView newCube = Object.Instantiate(originalCube);
        newCube.transform.SetParent(_container);
        newCube.Init(_canvas);

        int index = originalCube.transform.GetSiblingIndex();

        if (newCube.TryGetComponent(out RectTransform rectTransform))
        {
            rectTransform.sizeDelta = new Vector2(Constants.CubeWidth, Constants.CubeWidth);
            rectTransform.localScale = Vector3.one;
        }

        newCube.transform.SetSiblingIndex(index);
        _dropZonesManager.GetNewCube(newCube);

        return newCube;
    }

    public CubeView ReloadCube(CubeData cubeData)
    {
        Vector3 position = cubeData.LocalPosition;
        Color color = cubeData.Color;
        Transform parentTransform = cubeData.ParentTransform;
        Vector3 rotation = cubeData.Rotation;

        if (_canvas == null && _container == null)
            return null;

        CubeView newCube = Object.Instantiate(_config.CubeTemplate);

        newCube.transform.SetParent(parentTransform);
        newCube.transform.localPosition = position;
        newCube.transform.rotation = Quaternion.EulerAngles(rotation);

        if (newCube.TryGetComponent(out Image image))
            image.color = color;

        newCube.Init(_canvas);
        _dropZonesManager.GetNewCube(newCube);

        return newCube;
    }
}