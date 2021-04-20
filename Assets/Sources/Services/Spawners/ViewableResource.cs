using Core.Game;

namespace Services.Spawners
{
    public interface IViewGroupMapper
    {
        int GetID(string viewName);
    }

    public abstract class ViewableResource : ConvertableResource
    {
        private IViewGroupMapper viewGroupMapper;
        private string viewGroup;

        public IViewGroupMapper ViewGroupMapper
        {
            set
            {
                viewGroupMapper = value;
            }
        }

        public void SetViewGroup(string viewGroup)
        {
            this.viewGroup = viewGroup;
        }

        public int GetViewGroupID()
        {
            return viewGroupMapper.GetID(viewGroup);
        }

        public ViewableResource() { }
        public ViewableResource(ViewableResource original):base(original)
        {
            viewGroupMapper = original.viewGroupMapper;
            viewGroup = original.viewGroup;
        }
    }
}
