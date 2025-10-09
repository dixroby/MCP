namespace DynamicQueries.Repositories.Dtos
{
    public class PizzaSpecialDto(int id, string name, double basePrice,
        string description, string imageUrl)
    {
        public int Id => id;
        public string Name => name;
        public double BasePrice => basePrice;
        public string Description => description;
        public string ImageUrl => imageUrl;
    }
}
