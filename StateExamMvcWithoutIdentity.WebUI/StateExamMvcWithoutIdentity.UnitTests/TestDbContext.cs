using SteteExamMvcWithoutIdentity.Domain;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace StateExamMvcWithoutIdentity.UnitTests
{
    public class TestsDbContext : IDbContext
    {
        public TestsDbContext()
        {
            KnowledgeField = new TestDbSet<KnowledgeBranch>();
        }
        public DbSet<KnowledgeBranch> KnowledgeField { get; set; }
        public int SaveChangesCount { get; private set; }
        public int SaveChanges()
        {
            SaveChangesCount++;
            return 1;
        }
        public DbEntityEntry<T> Entry<T>(T entity) where T : class
        {
            return Entry(entity);
        }
        public void Dispose()
        {
        }
    }
}
