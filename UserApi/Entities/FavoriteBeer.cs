namespace UserApi.Entities
{
    public class FavoriteBeer
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        public int BeerId { get; set; }

        public User User { get; set; }
    }
}