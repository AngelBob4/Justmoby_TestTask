using Cubes.Infrastructure;
using Cubes.Model;
using Cubes.View;

namespace Cubes.Presenter
{
    public class IdleZonePresenter : IPresenter
    {
        private IdleZoneModel _idleZoneModel;
        private IdleZoneView _idleZoneView;

        public IdleZonePresenter(IdleZoneView idleZoneView, IdleZoneModel idleZoneModel)
        {
            _idleZoneModel = idleZoneModel;
            _idleZoneView = idleZoneView;
        }

        public void Enable()
        {
            _idleZoneView.CubeDroped += OnCubeDrop;
        }

        public void Disable()
        {
            _idleZoneView.CubeDroped -= OnCubeDrop;
        }

        private void OnCubeDrop(CubeView cubeView)
        {
            _idleZoneModel.DestroyCube(cubeView);
        }
    }
}