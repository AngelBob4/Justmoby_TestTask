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

    public void Init(Transform container, Transform canvas, DropZonesManager dropZonesManager)
    {
        _dropZonesManager = dropZonesManager;
        _container = container;
        _canvas = canvas;
    }

    public List<CubeView> GenerateListOfCubes(CubesConfig config)
    {
        if (_canvas == null && _container == null)
            return null;

        List<CubeView> generatedCubes = new List<CubeView>();
        int cubesAmount = config.Amount;
        CubeView template = config.CubeTemplate;

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
}