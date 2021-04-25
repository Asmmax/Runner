using Zenject;
using System.Collections.Generic;
using UnityEngine;
using Core.Game;
using Interactors;
using Core.Level;
using Services.Spawners;
using Saves;
using Services.Generators;

public class GameInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<IRandomGenerator>().To<UnityRandomGenerator>().AsSingle();
        Container.Bind<ILevelGeneratorFactory>().To<SingleRoadGeneratorFactory>().AsSingle();

        Container.BindInterfacesAndSelfTo<UnityTimeController>().AsSingle();
        Container.BindInterfacesAndSelfTo<UnityInputController>().AsSingle();
        Container.Bind<ILevelGateway>().To<TestLevelGateway>().AsSingle();

        Container.BindInterfacesAndSelfTo<GameController>().AsSingle();
        Container.BindInterfacesAndSelfTo<State>().AsSingle();

        Container.BindInterfacesAndSelfTo<GameModelBinder>().AsSingle().NonLazy();
    }
}
