using Zenject;
using System.Collections.Generic;
using UnityEngine;
using Core.Game;
using Interactors;
using Core.Level;
using Services.Spawners;

public class GameInstaller : MonoInstaller
{
    [SerializeField]
    private UnityGameController gameController;

    [SerializeField]
    private LineSettings lineSettings;

    [SerializeField]
    private LevelSettings[] levelSettings;

    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<UnityTimeController>().AsSingle();
        Container.BindInterfacesAndSelfTo<UnityInputController>().AsSingle();

        Container.BindInterfacesAndSelfTo<PauseInteractor>().AsSingle();

        Container.Bind<IRandomGenerator>().To<UnityRandomGenerator>().AsSingle();

        Container.Bind<ILevelGateway>().To<TestLevelGateway>().AsSingle();

        Container.Bind<IConverterGateway>().To<TestProceduralSpawner>().AsSingle().WithArguments(lineSettings, levelSettings/*(uint)2, 2f, 1.0f, 10.0f, 100*/);

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
