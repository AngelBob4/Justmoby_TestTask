using Cubes.View;
using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Cubes.Infrastructure
{
    public abstract class DropZone : MonoBehaviour, IDropHandler
    {
        public event Action<CubeView> CubeDroped;

        private IPresenter _presenter;

        public void Init(IPresenter presenter)
        {
            gameObject.SetActive(false);
            _presenter = presenter;
            gameObject.SetActive(true);
        }

        public void OnDrop(PointerEventData eventData)
        {
            GameObject draggedObject = eventData.pointerDrag;

            if (draggedObject.TryGetComponent(out CubeView cubeView) == false)
                return;

            CubeDroped?.Invoke(cubeView);
        }

        private void OnEnable()
        {
            _presenter?.Enable();
        }

        private void OnDisable()
        {
            _presenter?.Disable();
        }
    }
}