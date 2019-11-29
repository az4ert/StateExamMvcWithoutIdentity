using System;
using System.Linq;
using SteteExamMvcWithoutIdentity.Domain;
using NLog;

namespace StateExamMvcWithoutIdentity.Repositories
{
    public class KnowledgeBranchDbRepository:IKnowledgeBranchRepository
    {
        private readonly IDbContext context;
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();
        public KnowledgeBranchDbRepository(IDbContext dbContext)
        {
            context = dbContext;
        }

        public IQueryable<KnowledgeBranch> KnowledgeField
        {
            get
            {
                return context.KnowledgeField.Where(x=>x.IsActual);
            }
        }

        public bool AddKnowledgeBranch(KnowledgeBranch knowledgeBranch)
        {
            try
            {
                knowledgeBranch.IsActual = true;
                context.KnowledgeField.Add(knowledgeBranch);
                context.SaveChanges();
                logger.Info("Method AddKnowledgeBranch add data to database");
                return true;
            }
            catch(Exception ex)
            {
                logger.Error(ex, "Method AddKnowledgeBranch throws exception");
                return false;
            }
        }

        public bool DeleteKnowledgeBranch(int id)
        {
            KnowledgeBranch knowledgeBranch = context.KnowledgeField.FirstOrDefault(x => x.Id == id);
            if (knowledgeBranch!=null)
            {
                try
                {
                    knowledgeBranch.IsActual = false;
                    context.SaveChanges();
                    logger.Info("Method DeleteKnowledgeBrach changes IsActual value");
                    return true;
                }
                catch(Exception ex)
                {
                    logger.Error(ex, "Method DeleteKnowledgeBrach throws exception");
                    return false;
                }
            }
            logger.Info("Method DeleteKnowledgeBrach was called with unexists code");
            return true;
        }

        public bool EditKnowledgeBranch(int oldId, KnowledgeBranch knowledgeBranch)
        {
            KnowledgeBranch oldKnowledgeBranch = context.KnowledgeField.FirstOrDefault(x => x.Id == oldId);
            if (knowledgeBranch != null)
            {
                try
                {
                    oldKnowledgeBranch.Name=knowledgeBranch.Name;
                    oldKnowledgeBranch.Code = knowledgeBranch.Code;
                    context.SaveChanges();
                    logger.Info("Method EditKnowledgeBrach changes value");
                    return true;
                }
                catch (Exception ex)
                {
                    logger.Error(ex, "Method EditKnowledgeBrach throws exception");
                    return false;
                }
            }
            logger.Info("Method EditKnowledgeBrach was called with unexists code");
            return true;
        }

        public KnowledgeBranch FindById(int id)
        {
            logger.Info("Method FindById was called");
            return context.KnowledgeField.FirstOrDefault(x => x.Id == id && x.IsActual);
        }

        public KnowledgeBranch FindByName(string name)
        {
            logger.Info("Method FindByName was called");
            return context.KnowledgeField.FirstOrDefault(x => x.Name == name && x.IsActual);
        }

        public IQueryable<KnowledgeBranch> FindBySpecifiedCondition(Func<KnowledgeBranch, bool> condition)
        {
            logger.Info("Method FindBySpecifiedCondition called");
            return context.KnowledgeField.Where(x=>condition(x) && x.IsActual).AsQueryable();
        }
    }
}
