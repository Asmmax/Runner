using Core.Game;
using Core.Level;
using Leopotam.Ecs;
using Core.Game.Components;
using Core;

namespace Services.Spawners
{
    public class Character : IEntityConverter
    {
        private Core.Level.Field targetField;
        private IViewGroupMapper viewGroupMapper;

        private float distance;
        private float speed;

        public Character(Core.Level.Field field, IViewGroupMapper viewGroupMapper, float speed, float distance)
        {
            targetField = field;
            this.viewGroupMapper = viewGroupMapper;
            this.distance = distance;
            this.speed = speed;
        }

        void IEntityConverter.ConvertToEntity(IEntityManger entityManager)
        {
            EcsEntity character = CreateCharacter(entityManager);
            CreateCamera(entityManager, character);
        }

        EcsEntity CreateCharacter(IEntityManger entityManager)
        {
            EcsEntity c = entityManager.CreateEntity();

            ref var pos = ref c.Get<Position>();
            ref var speed = ref c.Get<Speed>();
            ref var size = ref c.Get<Bounds>();
            ref var field = ref c.Get<Core.Game.Components.Field>();

            ref var vertical = ref c.Get<VerticalTarget>();

            ref var score = ref c.Get<Score>();
            ref var health = ref c.Get<Health>();
            c.Get<PlayerTag>();

            ref var view = ref c.Get<ViewGroup>();
            c.Get<DynamicTag>();

            pos.value.x = distance;
            speed.value = this.speed;
            vertical.value = 0;
            size.height = 1;
            size.width = 0.5f;
            health.points = 4;
            view.id = viewGroupMapper.GetID("character");

            Rect rawField = targetField.BoundingBox;
            field.pivot = new float2 { x = (rawField.left + rawField.right) / 2, y = (rawField.bottom + rawField.top) / 2 };
            field.extent = new float2 { x = (rawField.right - rawField.left) / 2, y = (rawField.top - rawField.bottom) / 2 };

            return c;
        }

        EcsEntity CreateCamera(IEntityManger entityManager, EcsEntity character)
        {
            EcsEntity cam = entityManager.CreateEntity();
            ref var pos = ref cam.Get<Position>();
            ref var target = ref cam.Get<Target>();

            ref var view = ref cam.Get<ViewGroup>();
            cam.Get<DynamicTag>();

            target.value = character;
            view.id = viewGroupMapper.GetID("camera");

            return cam;
        }
    }
}
