using Microsoft.EntityFrameworkCore;
using Tutorial_API.Models;

namespace Tutorial_API.Data{

    //Database context
    public class TutorialContext : DbContext
    {
        public TutorialContext(DbContextOptions<TutorialContext> opt) : base(opt){

        }

        //model representation in Db using DbSet
        //Can add all models if you have
        //represent the model object Tutorial as a Database Set and call it Tutorials
        public DbSet<Tutorial> Tutorials { get; set; }
    }
}