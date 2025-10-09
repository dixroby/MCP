using DynamicQueries.BusinessObjects.Dtos;

namespace DynamicQueries.BusinessObjects.Interfaces
{
    public interface IExecuteQueryRepository
    {
        // el consumidor me va a decir el nombre de la fuente de datos, pizzas, o el nombre de una vista.
        DataSourceDto GetDataSourceByName(string name);
        // EL EXPANDO OBJECT SE EJECUTA EN MEMORIA, SI NOSOTROS ENVIAMOS LA PETICIÓN A UN QUERYABLE CON UNA FUENTE
        // DE DATOS DE UNA LISTA, NO HAY NINGÚN PROBLEMA PORQUE TODO SE EJECUTA AQUÍ EN LA APLICACIÓN

        // PERO SI MANDAMOS LA CONSULTA COMO LINQ, HACIA UNA FUENTE DE DATOS EXTERNAS, COMO ADO.NET, ENTITY FRAMEWORK
        // NO PUEDE CREAR EL EXPANDO
        // SE PUEDE USAR EXPANDO SI SOLO FUERA EN COLECCIONES EN MEMORIA, 
        IEnumerable <DataSourceDto> GetAllDataSources(); // MCP SERVER TENGO ESTAS FUENTES DE DATOS, DE AHÍ SACA LA RESPUESTA.

        // 3 campos, debe devolver una colección de 3 campos. dame el id, nombre y el rpecio, de cada elemento voy a querer ciertos valores
        // quiero el id y nombre, de cada registro, de cada elemento de la colección, id nombre, id nombre, id nombre.// el primer elemento será 1 hawaiina, 2 tocineitor
        // van ir  los valores de los campos solicitados
        Task<IEnumerable<object[]>> GetDataAsync(IQueryable<object[]> queryable); // solo van ir los valores.
        // obtener los valores de la consulta.

    }
}
