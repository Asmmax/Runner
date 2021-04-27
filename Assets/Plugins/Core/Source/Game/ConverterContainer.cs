using System.Collections.Generic;

namespace Core.Game
{
    public class ConverterContainer : IEntityConverter
    {
        private IList<IEntityConverter> converters = new List<IEntityConverter>();
        public void ConvertToEntity(IEntityManger entityManager)
        {
            foreach(var converter in converters)
            {
                converter.ConvertToEntity(entityManager);
            }
        }

        public void AddConverter(IEntityConverter converter)
        {
            converters.Add(converter);
        }
        public void Clear()
        {
            converters.Clear();
        }
    }
}
