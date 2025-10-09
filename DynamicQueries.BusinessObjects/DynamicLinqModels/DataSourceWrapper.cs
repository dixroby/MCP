using DynamicQueries.BusinessObjects.Dtos;
using System.Linq.Expressions;
using System.Reflection;

namespace DynamicQueries.BusinessObjects.DynamicLinqModels
{
    public class DataSourceWrapper
    {
        // diccionaro para guardar el nombre de la propiedad, y cuando yo le pida, tu me vas a dovolver el propertyInfo
        Dictionary<string, PropertyInfo> PropertiesDictionary = null;
        public DataSourceWrapper(DataSourceDto dataSource) 
        { 
            DataSource = dataSource;
            Init();
        }
        public DataSourceDto DataSource { get; }

        // el tipo de elemento.

        public Type ElementType => DataSource.Queryable.ElementType; // que propiedades, donde pongo el código que me de.
    
        // necesitamos armar el parameter expresion, la expresión lambda.
        // *context.OrderBy(p => 
        // unas buenas prácticas para crear varios parameterExpression
        public ParameterExpression ParameterExpression { get; private set; } // esto podría ir en el DTO, pero ahí está el adaptador, wrapper.

        // vamos a querer las propiedades mismas, 
        // puedo saber el nombre de la propiedad, buscar la información acerca de ese tipo, 
        public PropertyInfo GetPropertyInfo(string name) => PropertiesDictionary
            .GetValueOrDefault(name);

        public PropertyInfo[] GetAllPropertiesInfo() =>
            [.. PropertiesDictionary.Values];

        void Init()
        {
            ParameterExpression = Expression.Parameter(ElementType, "p");
            PropertiesDictionary = new Dictionary<string, PropertyInfo>(
                StringComparer.OrdinalIgnoreCase);

            foreach(var Property in DataSource.Properties)
            {
                var PropertyInfo = ElementType.GetProperty(Property.Name,
                    BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);

                if(PropertyInfo != null)
                    PropertiesDictionary.Add(PropertyInfo.Name, PropertyInfo);
            }

        }


    }
}
