using DynamicQueries.BusinessObjects.Dtos;

namespace DynamicQueries.BusinessObjects.DynamicLinqModels
{
    public class DataSourceWrapper
    {
        public DataSourceWrapper(DataSourceDto dataSourceDto)
        {
            DataSourceDto = dataSourceDto;
        }
        public DataSourceDto DataSourceDto { get;}
        public Type ElementType => DataSourceDto.Queryable.ElementType;

        -- que propiedades tiene este tipo de elemento --
    }
}