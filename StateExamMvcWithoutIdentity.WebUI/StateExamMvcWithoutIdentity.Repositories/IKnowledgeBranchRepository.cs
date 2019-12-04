using SteteExamMvcWithoutIdentity.Domain;
using System;
using System.Linq;

namespace StateExamMvcWithoutIdentity.Repositories
{
    public interface IKnowledgeBranchRepository
    {
        IQueryable<KnowledgeBranch> KnowledgeField { get; }
        KnowledgeBranch FindById(int id);
        KnowledgeBranch FindByCode(string code);
        KnowledgeBranch FindByName(string name);
        IQueryable<KnowledgeBranch> FindBySpecifiedCondition(Func<KnowledgeBranch, bool> condition);

        bool AddKnowledgeBranch(KnowledgeBranch knowledgeBranch);
        bool EditKnowledgeBranch(int oldId, KnowledgeBranch knowledgeBranch);
        bool DeleteKnowledgeBranch(int id);
    }
}
