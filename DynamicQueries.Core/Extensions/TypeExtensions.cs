using DynamicQueries.Core.Resources;
using System.Reflection;

namespace DynamicQueries.Core.Extensions
{
    public static class TypeExtensions
    {
        public static PropertyInfo GetPropertyByName(this Type type, string name)
        {


            PropertyInfo Property = type.GetProperties()
                .FirstOrDefault(p => string.Equals(
                    p.Name, name, StringComparison.OrdinalIgnoreCase));
            // si no se encontró la propiedad, lanzaremos una exepción
            return Property ?? 
                throw new ArgumentException(string.Format(Messages.PropertyNotFoundTemplate, name));
        }
    }
}
