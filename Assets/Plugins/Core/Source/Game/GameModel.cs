using Leopotam.Ecs;
using Core.Game.Systems;

namespace Core.Game
{
    public interface IImageView
    {
        void Show(int[] ids, int[] groupIDs);
        void UpdatePosition(int[] ids, float2[] positions);
        void UpdateAnimSpeed(int[] ids, float[] speeds);
        void Hide(int[] ids);
    }
    public interface IScoreView
    {
        void Update(int points);
    }
    public interface IHealthView
    {
        void Update(int points);
    }


    public interface IInputController
    {
        float Vertical { get; }
        void Lock();
        void Unlock();
        void Reset();
    }
    public interface ITimeController
    {
        float DeltaTime { get; }
        void Lock();
        void Unlock();
    }
    public interface IGameController
    {
        void Win();
        void Lose();
    }


    public interface IEntityManger
    {
        EcsEntity CreateEntity();
    }
    public interface IEntityConverter
    {
        void ConvertToEntity(IEntityManger entityManager);
    }

    public class EcsEntityManager : IEntityManger
    {
        private EcsWorld world;

        public EcsEntityManager(EcsWorld world)
        {
            this.world = world;
        }

        public EcsEntity CreateEntity()
        {
            return world.NewEntity();
        }
    }

    public class GameModel : System.IDisposable {

        protected EcsWorld _world;
        protected EcsSystems _systems;
        private bool isValid = false;

        IImageView imageView;
        IScoreView scoreView;
        IHealthView healthView;
        IInputController inputController;
        ITimeController timeController;
        IGameController gameController;

        IEntityConverter converter;

        public IGameController GameController
        {
            set { gameController = value; }
        }

        public IEntityConverter EntityConverter
        {
            set { converter = value; }
        }

        public GameModel(IInputController inputController,
                            ITimeController timeController,
                            IImageView imageView,
                            IHealthView healthView,
                            IScoreView scoreView)
        {
            this.inputController = inputController;
            this.timeController = timeController;
            this.imageView = imageView;
            this.healthView = healthView;
            this.scoreView = scoreView;
        }

        protected virtual void InitializeBeforeInitSystem() { }

        public virtual void Initialize () {
            _world = new EcsWorld();
            _systems = new EcsSystems(_world);

            InitializeBeforeInitSystem();

            _systems
                // gateway
                .Add (new ConversionSystem ())
                //controllers
                .Add (new Systems.VerticalController ())
                //logic
                .Add (new CharacterMovement ())
                .Add (new FollowerMovement ())
                .Add (new ResourceCollection ())
                .Add (new ScoreSystem ())
                .Add (new HealthSystem ())
                .Add (new WinSystem ())
                .Add (new LoseSystem ())
                //views
                .Add (new ViewSystem ())
                .Add (new HideResourceSystem ())
                .Add (new DynamicViewSystem ())
                .Add (new ScoreView ())
                .Add (new HealthView ())
                //utilization
                .Add (new ResourceUtilization ())

                // register one-frame components (order is important), for example:
                // .OneFrame<TestComponent1> ()

                // inject service instances here (order doesn't important), for example:
                // .Inject (new CameraService ())
                .Inject(converter)
                .Inject (imageView)
                .Inject (scoreView)
                .Inject (healthView)
                .Inject (inputController)
                .Inject (timeController)
                .Inject (gameController)
                .Init ();

            isValid = true;
        }

        public void Update() {
            _systems?.Run ();
        }

        public bool IsValid()
        {
            return isValid;
        }

        public void Dispose() {
            if (_systems != null) {
                _systems.Destroy ();
                _systems = null;
                _world.Destroy ();
                _world = null;
            }
            isValid = false;
        }
    }
}