namespace DynamicQueries.Repositories.Dtos
{
    public class ToppingDto(int id, string name, double price)
    {
        public int Id => id;
        public string Name => name;
        public double Price => price;
    }
}
