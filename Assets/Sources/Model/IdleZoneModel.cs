using UnityEngine;
using DG.Tweening;
using Cubes.View;
using UnityEngine.UI;

namespace Cubes.Model
{
    public class IdleZoneModel
    {
        private readonly float _duration = 0.5f;
        private readonly Vector3 rotating = new Vector3(0, 0, 180);

        private ConsoleModel _consoleModel;

        public void Init(ConsoleModel consoleModel)
        {
            _consoleModel = consoleModel;
        }

        public void DestroyCube(CubeView cubeView)
        {
            _consoleModel.WriteToConsole(TypeOfText.CubeDisappearing);
            Vector3 endScale = Vector3.one * 2;

            if (cubeView.TryGetComponent(out Image image))
            {
                Color imageColor = image.color;
                Color endColor = new Color(imageColor.r, imageColor.g, imageColor.b, 0);
                image.DOColor(endColor, _duration);
            }

            cubeView.TurnOffRaycasts();
            cubeView.transform.DORotate(rotating, _duration);
            cubeView.transform.DOScale(endScale, _duration).OnComplete(() => Object.Destroy(cubeView.gameObject));
        }
    }
}