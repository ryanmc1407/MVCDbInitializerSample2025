namespace Week1Lab12026.Models
{
    public class DbSeeder
    {
        private readonly UserContext _ctx;
        private readonly IWebHostEnvironment _hosting;
        private bool disposedValue;

        public DbSeeder(UserContext ctx, IWebHostEnvironment hosting)
        {
            _ctx = ctx;
            _hosting = hosting;
        }

        public void Seed()
        {
            _ctx.Database.EnsureCreated();

            if (!_ctx.Users.Any())
            {
                _ctx.Users.AddRange(new List<User>()
                {
                    new User { FirstName = "Mark", MidName = "J", LastName = "Lavin", DOB = new DateTime(1996, 12, 11) },
                    new User { FirstName = "Jogn", MidName = "Frank", LastName = "Doe", DOB = new DateTime(1980, 1, 1) },
                    new User { FirstName = "Jane", MidName = "Frank", LastName = "Doe", DOB = new DateTime(1986, 1, 1) },
                    new User { FirstName = "Mark", MidName = "Frank", LastName = "Smith", DOB = new DateTime(1972, 1, 1) },
                    new User { FirstName = "Mary", MidName = "Frank", LastName = "Smith", DOB = new DateTime(1975, 1, 1) }
                });
                _ctx.SaveChanges();
            }
        }
    }
}
