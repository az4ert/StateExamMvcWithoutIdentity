using System.Data.Entity;

namespace SteteExamMvcWithoutIdentity.Domain
{
    public class StateExamContext : DbContext, IDbContext
    {
        public DbSet<KnowledgeBranch> KnowledgeField { get; set; }
    }
}
