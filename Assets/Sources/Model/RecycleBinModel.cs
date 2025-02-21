using Cubes.View;
using UnityEngine;
using DG.Tweening;

public class RecycleBinModel
{
    private readonly float _duration = 0.5f;
    private readonly Vector3 _rotating = new Vector3(0f, 0f, 90f);

    private Transform _binPosition;
    private ConsoleModel _consoleModel;

    public void Init(Transform endPosition, ConsoleModel consoleModel)
    {
        _consoleModel = consoleModel;
        _binPosition = endPosition;
    }

    public void DestroyCube(CubeView cubeView)
    {
        _consoleModel.WriteToConsole(TypeOfText.CubeThrowing);
        cubeView.TurnOffRaycasts();
        Vector3 topPoint = _binPosition.position + new Vector3(0, 100, 0);

        Sequence sequence = DOTween.Sequence();
        sequence.Append(cubeView.transform.DORotate(_rotating, _duration, RotateMode.Fast))
            .SetEase(Ease.Linear)
            .Join(cubeView.transform.DOMove(topPoint, _duration))
            .SetEase(Ease.Linear);

        sequence.Append(cubeView.transform.DORotate(_rotating, _duration, RotateMode.Fast))
            .SetEase(Ease.Linear)
            .Join(cubeView.transform.DOMove(_binPosition.position, _duration))
            .SetEase(Ease.Linear)
            .OnComplete(() => StartDeletingCube(cubeView));

        sequence.Play();
    }

    private void StartDeletingCube(CubeView cubeView)
    {
        Vector3 bottomPoint = _binPosition.position - new Vector3(0, 100, 0);

        cubeView.transform.SetParent(_binPosition);
        cubeView.transform.DOMove(bottomPoint, _duration)
            .OnComplete(() => Object.Destroy(cubeView.gameObject));
    }
}