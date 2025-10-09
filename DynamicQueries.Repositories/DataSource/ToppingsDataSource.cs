using DynamicQueries.BusinessObjects.Dtos;
using DynamicQueries.Repositories.Dtos;

namespace DynamicQueries.Repositories.DataSource
{
    internal class ToppingsDataSource
    {
        private static IEnumerable<ToppingDto> s_toppings = [
            new ToppingDto(1, "Queso extra", 23.50),
            new ToppingDto(2, "Tocino de pavo", 28.80),
            new ToppingDto(3, "Tocino de jabalí", 28.80),
            new ToppingDto(4, "Tocino de ternera", 28.80),
            new ToppingDto(5, "Té y bollos", 47.00),
            new ToppingDto(6, "Bollos recién horneados", 43.50),
            new ToppingDto(7, "Pimiento", 9.00),
            new ToppingDto(8, "Cebolla", 9.00),
            new ToppingDto(9, "Champiñones", 9.00),
            new ToppingDto(10, "Pepperoni", 9.00),
            new ToppingDto(11, "Salchicha de pato", 30.80),
            new ToppingDto(12, "Albóndigas de venado", 24.50),
            new ToppingDto(13, "Cubierta de langosta", 612.50),
            new ToppingDto(14, "Caviar de esturión", 965.25),
            new ToppingDto(15, "Corazones de alcachofa", 32.60),
            new ToppingDto(16, "Tomates frescos", 19.00),
            new ToppingDto(17, "Albahaca", 19.00),
            new ToppingDto(18, "Filete", 80.50),
            new ToppingDto(19, "Pimientos picantes", 39.80),
            new ToppingDto(20, "Pollo búfalo", 48.00),
            new ToppingDto(21, "Queso azul", 24.50)
            ];

        public static DataSourceDto Metadata => new DataSourceDto("Toppings", "Catálogo de ingredientes de pizzas.", s_toppings.AsQueryable(),
            [
                new DataSourcePropertyInfoDto(
                    nameof(ToppingDto.Id), "Identificador del ingrediente."),
                new DataSourcePropertyInfoDto(
                    nameof(ToppingDto.Name), "Nombre del ingrediente."),
                new DataSourcePropertyInfoDto(
                    nameof(ToppingDto.Price), "Precio DEL INGREDIENTE.")
            ]);
    }
}
