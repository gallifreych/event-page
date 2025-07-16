using event_page_web.Models;
using Microsoft.EntityFrameworkCore;


namespace event_page_web.Data
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options)
            : base(options) { }

        public DbSet<Event> Etkinlikler { get; set; }
    }
}