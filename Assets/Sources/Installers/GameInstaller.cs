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
    private UnityGameController gameController;

    [SerializeField]
    private GeneratorObject[] levels;

    [SerializeField]
    private Life life;

    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<UnityTimeController>().AsSingle();
        Container.BindInterfacesAndSelfTo<UnityInputController>().AsSingle();

        Container.BindInterfacesAndSelfTo<PauseInteractor>().AsSingle();

        Container.Bind<IRandomGenerator>().To<UnityRandomGenerator>().AsSingle();

        Container.Bind<ILevelGateway>().To<TestLevelGateway>().AsSingle();

        Container.Bind<ILevelGeneratorFactory>().To<SingleRoadGeneratorFactory>().AsSingle();
        Container.Bind<IConverterGateway>().To<UnityGeneratorContainer>().AsSingle().WithArguments(levels);

        Container.BindInterfacesAndSelfTo<PlayInteractor>().AsSingle();

        ScoreViewRebinding();

        Container.Bind<GameModel>().To<UnityIntegratedGameModel>().AsSingle();

        Container.Resolve<PlayInteractor>().TargetModel = Container.Resolve<GameModel>();

        Container.Inject(gameController);

    }

    private void ScoreViewRebinding()
    {
        IList<IScoreView> scoreViews = Container.ResolveAll<IScoreView>();
        Container.Unbind<IScoreView>();
        foreach (IScoreView view in scoreViews)
        {
            Container.Bind<IScoreView>().FromInstance(view).WhenInjectedInto<ScoreViewContainer>();
        }
        Container.BindInterfacesAndSelfTo<ScoreViewContainer>().AsSingle().WhenNotInjectedInto<ScoreViewContainer>();
    }
}
