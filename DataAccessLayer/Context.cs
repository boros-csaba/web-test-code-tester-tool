using DataAccessLayer.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class Context : DbContext
    {
        public DbSet<Problem> Problems { get; set; }
        public DbSet<AnswerKey> AnswerKeys { get; set; }
        public DbSet<ProblemSet> ProblemSets { get; set; }
        public DbSet<OrderedProblem> OrderedProblems { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<Context>());
        }

    }
}
