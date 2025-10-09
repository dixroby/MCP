namespace DynamicQueries.BusinessObjects.Dtos
{
    //PARA HACER CONSULTAS, NECESITAMOS EL IQUERYABLE
    public class DataSourceDto(string name, string description,
        IQueryable queryable, DataSourcePropertyInfoDto[] properties)
    {
        // el nombre del dataSource, y la descripción que le servirá a la Inteligencia artificial
        public string Name => name;
        public string Description => description;
        public IQueryable Queryable { get; set; } = queryable; // de que fuente de datos, en memoria de las pixas, o base de datos, con el dbSet.
        // a este queryable.
        // el repositorio 
        public DataSourcePropertyInfoDto[] Properties => properties;

    }
}
