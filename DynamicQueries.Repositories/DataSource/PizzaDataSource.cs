using DynamicQueries.BusinessObjects.Dtos;
using DynamicQueries.Repositories.Dtos;

namespace DynamicQueries.Repositories.DataSource
{
    internal class PizzaDataSource
    {
        private static IEnumerable<PizzaSpecialDto> s_pizzas =
       [
          new PizzaSpecialDto
          (
               1,
               "Pizza clásica de queso",
               89.99,
               "Es de queso y delicioso. ¿Por qué no querrías una?",
               "cheese.jpg"
           ),
           new PizzaSpecialDto
           (
               2,
               "Tocinator",
               127.99,
               "Tiene TODO tipo de tocino",
               "bacon.jpg"
          ),
          new PizzaSpecialDto
          (
               3,
               "Clásica de pepperoni",
               99.50,
               "Es la pizza con la que creciste, ¡pero ardientemente deliciosa!",
               "pepperoni.jpg"
           ),
           new PizzaSpecialDto
           (
               4,
               "Pollo búfalo",
               128.75,
               "Pollo picante, salsa picante y queso azul, garantizado que entrarás en calor",
               "meaty.jpg"
          ),
          new PizzaSpecialDto
          (
               5,
               "Amantes del champiñón",
               109.00,
               "Tiene champiñones. ¿No es obvio?",
               "mushroom.jpg"
           ),
           new PizzaSpecialDto
           (
               6,
               "Hawaiana",
               90.25,
               "De piña, jamón y queso...",
               "hawaiian.jpg"
          ),
          new PizzaSpecialDto
          (
               7,
               "Delicia vegetariana",
               118.50,
               "Es como una ensalada, pero en una pizza",
               "salad.jpg"
           ),
           new PizzaSpecialDto
           (
               8,
               "Margarita",
               89.99,
               "Pizza italiana tradicional con tomates y albahaca",
               "margherita.jpg"
           )
       ];

        public static DataSourceDto Metadata => new DataSourceDto("Pizzas", "Catálogo de pizzas.", s_pizzas.AsQueryable(),
            [
                new DataSourcePropertyInfoDto(
                    nameof(PizzaSpecialDto.Id), "Identificador de la pizza."),
                new DataSourcePropertyInfoDto(
                    nameof(PizzaSpecialDto.Name), "Nombre de la pizza."),
                new DataSourcePropertyInfoDto(
                    nameof(PizzaSpecialDto.BasePrice), "Precio base de la pizza."),
                new DataSourcePropertyInfoDto(
                    nameof(PizzaSpecialDto.Description), "Descripción de la pizza."),
                new DataSourcePropertyInfoDto(
                    nameof(PizzaSpecialDto.ImageUrl), "Url de la imagen de la pizza.")
            ]);
    }
}
