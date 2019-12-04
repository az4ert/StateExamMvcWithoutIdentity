using System;
using System.Linq;
using SteteExamMvcWithoutIdentity.Domain;
using StateExamMvcWithoutIdentity.Repositories;
using NLog;

namespace StateExamMvc.Services
{
    public class KnowledgeBranchService : IKnowledgeBranchService
    {
        private IKnowledgeBranchRepository repository;
        private KnowledgeBranchError ValidateKnowledgeBranch(KnowledgeBranch knowledgeBranch)
        {
            if (repository.FindByCode(knowledgeBranch.Code) != null)
                return KnowledgeBranchError.DuplicateCode;
            if (repository.FindByName(knowledgeBranch.Name) != null)
                return KnowledgeBranchError.DuplicateName;
            return KnowledgeBranchError.None;
        }
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        public KnowledgeBranchService(IKnowledgeBranchRepository repository)
        {
            this.repository = repository;
        }
        public KnowledgeBranch GetById(int id)
        {
            logger.Info("KnowledgeBranchService GetById method call");
            return repository.FindById(id);
        }

        public KnowledgeBranch GetByName(string name)
        {
            logger.Info("KnowledgeBranchService GetByName method call");
            return repository.FindByName(name);
        }

        public KnowledgeBranch GetByCode(string code)
        {
            logger.Info("KnowledgeBranchService GetByName method call");
            return repository.FindByCode(code);
        }

        public IQueryable<KnowledgeBranch> GetBySpecifiedCondition(Func<KnowledgeBranch,bool> condition)
        {
            logger.Info("KnowledgeBranchService GetBySpecifiedCondition method call");
            return repository.FindBySpecifiedCondition(condition);
        }

        public IQueryable<KnowledgeBranch> GetKnowledgeField()
        {
            logger.Info("KnowledgeBranchService GetKnowledgeField method call");
            return repository.KnowledgeField;
        }

        public KnowledgeBranchError Add(KnowledgeBranch knowledgeBranch)
        {
            logger.Info("KnowledgeBranchService Add method call");
            KnowledgeBranchError error = ValidateKnowledgeBranch(knowledgeBranch);
            if (error!=KnowledgeBranchError.None)
            {
                logger.Error($"KnowledgeBranchService validate error with message {error.ToString()}");
                return error;
            }
            if (repository.AddKnowledgeBranch(knowledgeBranch))
                return KnowledgeBranchError.None;
            return KnowledgeBranchError.DbError;
        }

        public KnowledgeBranchError Edit(int id, KnowledgeBranch knowledgeBranch)
        {
            logger.Info("KnowledgeBranchService Edit method call");
            KnowledgeBranchError error = ValidateKnowledgeBranch(knowledgeBranch);
            if (error != KnowledgeBranchError.None)
            {
                logger.Error($"KnowledgeBranchService validate error with message {error.ToString()}");
                return error;
            }
            if (repository.EditKnowledgeBranch(id, knowledgeBranch))
                return KnowledgeBranchError.None;
            return KnowledgeBranchError.DbError;
        }

        public KnowledgeBranchError Delete(int id)
        {
            logger.Info("KnowledgeBranchService Delete method call");
            if (repository.DeleteKnowledgeBranch(id))
                return KnowledgeBranchError.None;
            return KnowledgeBranchError.DbError;
        }
    }
}
