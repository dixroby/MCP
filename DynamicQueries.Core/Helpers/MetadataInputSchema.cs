using DynamicQueries.Core.Resources;

namespace DynamicQueries.Core.Helpers
{
    internal static class MetadataInputSchemaHelper
    {
        public static object GetInputSchema() => new
        {
            type = "object",
            properties = new
            {
                dataSource = new
                {
                    type = "string",
                    description = InputSchemaMessages.DataSourceDescription
                },
                fieldNames = new
                {
                    type = "array",
                    items = new { type = "string" },
                    description = InputSchemaMessages.FieldNamesDescription // Cambié la descripción para mayor claridad
                },
                filters = new
                {
                    type = "array",
                    description = InputSchemaMessages.FiltersDescription,
                    items = new
                    {
                        type = "object",
                        properties = new
                        {
                            fieldName = new
                            {
                                type = "string",
                                description = InputSchemaMessages.FiltersFieldNameDescription
                            },
                            operation = new
                            {
                                type = "string",
                                @enum = new[] { ">", ">=", "<", "<=", "==", "!=", "contains" },
                                description = InputSchemaMessages.FiltersOperationDescription
                            },
                            value = new
                            {
                                type = "string",
                                description = InputSchemaMessages.FiltersValueDescription
                            },
                            joinWithNext = new
                            {
                                type = "string",
                                @enum = new[] { "and", "or" },
                                description = InputSchemaMessages.FiltersJoinWithNextDescription
                            }
                        },
                        required = new[] { "fieldName", "operation", "value" }
                    }
                },
                orders = new
                {
                    type = "array",
                    description = InputSchemaMessages.OrdersDescription,
                    items = new
                    {
                        type = "object",
                        properties = new
                        {
                            fieldName = new
                            {
                                type = "string",
                                description = InputSchemaMessages.OrdersFieldNameDescription
                            },
                            orderType = new
                            {
                                type = "string",
                                @enum = new[] { "asc", "desc" },
                                description = InputSchemaMessages.OrdersOrdersTypeDescription
                            }
                        },
                        required = new[] { "fieldName" }
                    }
                }
            },
            required = new[] { "dataSource" }
        };
    }
}