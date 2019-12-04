using SteteExamMvcWithoutIdentity.Domain;
using System;
using System.Linq;

namespace StateExamMvc.Services
{
    public enum KnowledgeBranchError { None, DbError, DuplicateCode, DuplicateName}
    public interface IKnowledgeBranchService
    {
        IQueryable<KnowledgeBranch> GetKnowledgeField();
        KnowledgeBranch GetById(int id);
        KnowledgeBranch GetByCode(string code);
        KnowledgeBranch GetByName(string name);
        IQueryable<KnowledgeBranch> GetBySpecifiedCondition(Func<KnowledgeBranch,bool> condition);
        KnowledgeBranchError Add(KnowledgeBranch knowledgeBranch);
        KnowledgeBranchError Edit(int id, KnowledgeBranch knowledgeBranch);
        KnowledgeBranchError Delete(int id);
    }
}
