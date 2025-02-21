using Cubes.Model;
using Cubes.View;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class GameStorage
{
    private string filePath => Path.Combine(Application.persistentDataPath, "cubes.json");
    private CubesGenerator _cubesGenerator;
    private TowerManager _towerManager;

    public void Init(CubesGenerator cubesGenerator, TowerManager towerManager)
    {
        _towerManager = towerManager;
        _cubesGenerator = cubesGenerator;
        LoadCubes();
    }

    public void SaveCubes(List<CubeView> cubes, Transform parentTransform)
    {
        CubeDataList cubeDataList = new CubeDataList();

        foreach (CubeView cube in cubes)
        {
            CubeData cubeData = new CubeData();
            cubeData.LocalPosition = cube.transform.localPosition;
            cubeData.ParentTransform = parentTransform;
            cubeData.Rotation = Quaternion.ToEulerAngles(cube.transform.rotation);

            if (cube.TryGetComponent(out Image image))
                cubeData.Color = image.color;

            cubeDataList.cubes.Add(cubeData);
        }

        string json = JsonUtility.ToJson(cubeDataList, true);

        File.WriteAllText(filePath, json);
        Debug.Log("Cubes saved to " + filePath);
    }

    public void LoadCubes()
    {
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            CubeDataList loadedData = JsonUtility.FromJson<CubeDataList>(json);

            for (int i = 0; i < loadedData.cubes.Count; i++)
            {
                if (i < loadedData.cubes.Count)
                {
                    CubeView cube = _cubesGenerator.ReloadCube(loadedData.cubes[i]);

                    if (cube != null)
                        _towerManager.ReloadCube(cube);
                }
            }
        }
    }
}

[System.Serializable]
public class CubeData
{
    public Vector3 LocalPosition;
    public Transform ParentTransform;
    public Color Color;
    public Vector3 Rotation;
}

[System.Serializable]
public class CubeDataList
{
    public List<CubeData> cubes = new List<CubeData>();
}