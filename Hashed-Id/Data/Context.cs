using Hashed_Id.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Hashed_Id.Data
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options)
        {
            
        }


        public DbSet<Person> Persons { get; set; }
    }
}
