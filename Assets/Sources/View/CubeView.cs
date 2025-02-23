using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Cubes.View
{
    [RequireComponent(typeof(CanvasGroup))]
    public class CubeView : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        private Transform _canvas;
        private CanvasGroup _canvasGroup;

        public event Action<CubeView> OnBeginDraging;
        public event Action<CubeView> OnEndDraging;
        public event Action<CubeView> OnCubeDestroy;

        public bool IsInTower { get; private set; } = false;

        public void Init(Transform canvas)
        {
            _canvas = canvas;
            _canvasGroup = GetComponent<CanvasGroup>();
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            OnBeginDraging?.Invoke(this);
            IsInTower = false;
            _canvasGroup.blocksRaycasts = false;
            _canvasGroup.alpha = 0.6f;
            transform.SetParent(_canvas);
        }

        public void OnDrag(PointerEventData eventData)
        {
            transform.position = eventData.position;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            OnEndDraging?.Invoke(this);
            _canvasGroup.alpha = 1f;

            if (EventSystem.current.IsPointerOverGameObject() == false)
                Destroy(gameObject);
        }

        private void OnDestroy()
        {
            OnCubeDestroy?.Invoke(this);
        }

        public void TurnOffRaycasts()
        {
            _canvasGroup.blocksRaycasts = false;
        }

        public void TurnOnRaycasts()
        {
            _canvasGroup.blocksRaycasts = true;
        }

        public void SetTower()
        {
            IsInTower = true;
        }
    }
}