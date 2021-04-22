using Zenject;
using System.Collections.Generic;
using UnityEngine;
using Core.Game;
using Interactors;
using Core.Level;
using Services.Spawners;
using Services.Generators;

public class GameInstaller : MonoInstaller
{
    [SerializeField]
    private GeneratorObject[] levels;

    public override void InstallBindings()
    {
        Container.Bind<IRandomGenerator>().To<UnityRandomGenerator>().AsSingle();
        Container.Bind<ILevelGeneratorFactory>().To<SingleRoadGeneratorFactory>().AsSingle();

        Container.BindInterfacesAndSelfTo<UnityTimeController>().AsSingle();
        Container.BindInterfacesAndSelfTo<UnityInputController>().AsSingle();
        Container.Bind<ILevelGateway>().To<TestLevelGateway>().AsSingle();
        Container.Bind<IConverterGateway>().To<UnityGeneratorContainer>().AsSingle().WithArguments(levels);

        Container.BindInterfacesAndSelfTo<PauseInteractor>().AsSingle();
        Container.BindInterfacesAndSelfTo<PlayInteractor>().AsSingle();

        Container.BindInterfacesAndSelfTo<GameModelBinder>().AsSingle().NonLazy();
    }
}
