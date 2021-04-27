using Zenject;
using UnityEngine;
using Interactors;
using Core.Level;
using Saves;
using Services.Generators;

public class GameInstaller : MonoInstaller
{
    [SerializeField]
    LevelSettings levelSettings;
    [SerializeField]
    LocalizationSettings localizationSettings;
    [SerializeField]
    ViewSettings viewSettings;

    public override void InstallBindings()
    {
        Container.Bind<IRandomGenerator>().To<UnityRandomGenerator>().AsSingle();
        Container.Bind<ILevelGeneratorFactory>().To<SingleRoadGeneratorFactory>().AsSingle();

        Container.BindInterfacesAndSelfTo<UnityTimeController>().AsSingle();
        Container.BindInterfacesAndSelfTo<UnityInputController>().AsSingle();
        Container.Bind<ILevelGateway>().To<TestLevelGateway>().AsSingle();
        Container.BindInterfacesTo<LevelContainer>().AsSingle().WithArguments(levelSettings);
        Container.BindInterfacesTo<TextLocalizationService>().AsSingle().WithArguments(localizationSettings);

        Container.BindInterfacesAndSelfTo<ViewGroupMapper>().AsSingle().WithArguments(viewSettings);
        Container.BindInterfacesAndSelfTo<UnityImageView>().AsSingle();
        Container.BindInterfacesAndSelfTo<GameController>().AsSingle();
        Container.BindInterfacesAndSelfTo<State>().AsSingle();

        Container.BindInterfacesAndSelfTo<GameModelBinder>().AsSingle().NonLazy();
    }
}
