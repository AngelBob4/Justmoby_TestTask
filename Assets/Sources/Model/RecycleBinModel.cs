using Cubes.View;
using UnityEngine;
using DG.Tweening;

public class RecycleBinModel : MonoBehaviour
{
    private readonly float _duration = 2f;
    private readonly Vector3 rotating = new Vector3(0, 0, 180);

    private Transform _endPosition;

    public void Init(Transform endPosition)
    {
        _endPosition = endPosition;
    }

    public void DestroyCube(CubeView cubeView)
    {
        cubeView.TurnOffRaycasts();
        cubeView.transform.DORotate(rotating, _duration);
        cubeView.transform.DOScale(Vector3.zero, _duration).OnComplete(() => Object.Destroy(cubeView.gameObject));
    }
}