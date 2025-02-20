using UnityEngine;
using DG.Tweening;
using Cubes.View;

namespace Cubes.Model
{
    public class IdleZoneModel
    {
        private readonly float _duration = 2f;
        private readonly Vector3 rotating = new Vector3(0, 0, 180);

        public void DestroyCube(CubeView cubeView)
        {
            cubeView.TurnOffRaycasts();
            cubeView.transform.DORotate(rotating, _duration);
            cubeView.transform.DOScale(Vector3.zero, _duration).OnComplete(() => Object.Destroy(cubeView.gameObject));
        }
    }
}