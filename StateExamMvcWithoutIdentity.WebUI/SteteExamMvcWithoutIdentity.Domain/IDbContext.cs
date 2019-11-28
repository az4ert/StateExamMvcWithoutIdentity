using System.Data.Entity;

namespace SteteExamMvcWithoutIdentity.Domain
{
    public interface IDbContext
    {
        DbSet<KnowledgeBranch> KnowledgeField { get; set; }
        int SaveChanges();
    }
}
