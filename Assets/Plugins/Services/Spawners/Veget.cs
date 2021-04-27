using Core.Game;
using Leopotam.Ecs;
using Core.Game.Components;
using Core;

namespace Services.Spawners
{
    public class Veget : ViewableResource
    {
        private int damage;

        public int Damage
        {
            set { damage = value; }
        }

        public Veget() { }

        protected Veget(Veget original) : base(original) {
            damage = original.damage;
        }

        protected override void ConvertToEntityImpl(IEntityManger entityManager)
        {
            EcsEntity e = entityManager.CreateEntity();
            ref var pos = ref e.Get<Position>();
            ref var bounds = ref e.Get<Bounds>();
            ref var damage = ref e.Get<Damage>();
            ref var view = ref e.Get<ViewGroup>();

            bounds.height = Size.x;
            bounds.width = Size.y;

            float2 newPos = new float2 { x = Distance, y = TargetField.GetHorizontalPosFor(Line) };
            pos.value.x = newPos.x;
            pos.value.y = newPos.y;

            damage.points = this.damage;
            view.id = GetViewGroupID();
        }

        public override ConvertableResource Clone()
        {
            return new Veget(this);
        }
    }
}
