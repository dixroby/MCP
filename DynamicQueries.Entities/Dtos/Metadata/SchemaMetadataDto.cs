using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicQueries.Entities.Dtos.Metadata
{
    public class SchemaMetadataDto(
        Dictionary<string, OperatorSupportDto> operatorSupport,
        Dictionary<string, string> orderKeywords,
        IEnumerable<DataSourceInfoDto> dataSources,
        object inputSchema)
    {
        // El inputSchema va a ser un objeto, que va a incluir instrucciones acerca del formato que queremos recibir
        // para realizar las consultas, tipo especificar el dataSource, indicar los campos pero que son opcionales, 
        // y si nostrata de dar los campos, o el nombre del campo que debe haber, la colección de filtros, el nombre del campo, la operación del filtro.
        // todo esa forma parte de las specs del input Schema, el consumidor debe saber que nos debe enviar para ejecutar una consulta dinámica.
        public Dictionary<string, OperatorSupportDto> OperatorSupport => operatorSupport;// soporte de los operadores.

        public Dictionary<string, string> OrderKeywords => orderKeywords; // palabras clave para ordenamiento.
        public IEnumerable<DataSourceInfoDto> DataSources => dataSources; // info de los dataSources.
        public object InputSchema => inputSchema; // cómo voy a recibir los datos.
    }
}
