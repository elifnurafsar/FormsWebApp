using FormsWebApp.Models;
using Microsoft.EntityFrameworkCore;

namespace FormsWebApp.Context
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options) { 
        
        }      

        public DbSet<CLogin> Users { get; set; }

        public DbSet<FormsWebApp.Models.Form>? Form { get; set; }

        public DbSet<FormsWebApp.Models.Field>? Field { get; set; }
    }
}
