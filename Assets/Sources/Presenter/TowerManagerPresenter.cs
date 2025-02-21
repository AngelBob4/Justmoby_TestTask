using Cubes.Infrastructure;
using Cubes.Model;
using Cubes.View;

namespace Cubes.Presenter
{
    public class TowerManagerPresenter : IPresenter
    {
        private TowerManager _towerManager;
        private TowerZoneView _towerZoneView;

        public TowerManagerPresenter(TowerZoneView towerZoneView, TowerManager towerManager)
        {
            _towerZoneView = towerZoneView;
            _towerManager = towerManager;
        }

        public void Enable()
        {
            _towerZoneView.CubeDroped += OnCubeDrop;
        }

        public void Disable()
        {
            _towerZoneView.CubeDroped -= OnCubeDrop;
        }

        private void OnCubeDrop(CubeView cubeView)
        {
            _towerManager.TryAddCube(cubeView);
        }
    }
}