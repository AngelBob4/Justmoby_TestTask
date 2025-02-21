using Cubes.Model;
using Reflex.Core;
using UnityEngine;

public class ProjectInstaller : MonoBehaviour, IInstaller
{
    public void InstallBindings(ContainerBuilder containerBuilder)
    {
        containerBuilder.AddSingleton(new IdleZoneModel());
        containerBuilder.AddSingleton(new TowerManager());
        containerBuilder.AddSingleton(new CubesGenerator());
        containerBuilder.AddSingleton(new RecycleBinModel());
        containerBuilder.AddSingleton(new DropZonesManager());
        containerBuilder.AddSingleton(new CubesStorage());
        containerBuilder.AddSingleton(new ConsoleModel());
        containerBuilder.AddSingleton(new GameStorage());
    }
}