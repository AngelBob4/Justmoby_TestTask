using Cubes.Infrastructure;
using Cubes.View;

namespace Cubes.Presenter
{
    public class RecycleBinPresenter : IPresenter
    {
        private RecycleBinModel _recycleBinModel;
        private RecycleBinZoneView _recycleBinZoneView;

        public RecycleBinPresenter(RecycleBinModel recycleBinModel, RecycleBinZoneView recycleBinZoneView)
        {
            _recycleBinModel = recycleBinModel;
            _recycleBinZoneView = recycleBinZoneView;

            if (_recycleBinModel != null)
                _recycleBinModel.Init(_recycleBinZoneView.EndPosition);
        }

        public void Enable()
        {
            _recycleBinZoneView.CubeDroped += OnCubeDrop;
        }

        public void Disable()
        {
            _recycleBinZoneView.CubeDroped -= OnCubeDrop;
        }

        private void OnCubeDrop(CubeView cubeView)
        {
            _recycleBinModel.DestroyCube(cubeView);
        }
    }
}