using Microsoft.VisualStudio.TestTools.UnitTesting;
using StateExamMvcWithoutIdentity.Repositories;
using SteteExamMvcWithoutIdentity.Domain;
using System.Collections.Generic;
using System.Linq;

namespace StateExamMvcWithoutIdentity.UnitTests
{
    [TestClass]
    public class KnowledgeBranchDbRepositoryTests
    {
        private TestsDbContext dbContext;
        private IQueryable<KnowledgeBranch> knowledgeField;
        private TestDbSet<KnowledgeBranch> dbSet;
        private KnowledgeBranchDbRepository repository;

        [TestInitialize]
        public void Initialize()
        {
            knowledgeField = new List<KnowledgeBranch>
            {
                new KnowledgeBranch
                {
                    Id=1,
                    Code ="07",
                    Name="Управління та адміністрування",
                    IsActual=true
                },
                new KnowledgeBranch
                {
                    Id=2,
                    Code="12",
                    Name="Інформаційні технології",
                    IsActual=true
                },
                new KnowledgeBranch
                {
                    Id=3,
                    Code="14",
                    Name="Електрична інженерія",
                    IsActual=true
                },
                new KnowledgeBranch
                {
                    Id=4,
                    Code="15",
                    Name="Автоматизація та приладобудування",
                    IsActual=false
                },
                new KnowledgeBranch
                {
                    Id=5,
                    Code="17",
                    Name="Електроніка та телекомунікації",
                    IsActual=false
                },
                new KnowledgeBranch
                {
                    Id = 1,
                    Code = "07",
                    Name = "Інформаційні технології",
                    IsActual=false
                }
            }.AsQueryable();
            dbSet = new TestDbSet<KnowledgeBranch>(knowledgeField);
            dbContext = new TestsDbContext();
            foreach (KnowledgeBranch knowledgeBranch in knowledgeField)
            {
                dbContext.KnowledgeField.Add(knowledgeBranch);
            }
            repository = new KnowledgeBranchDbRepository(dbContext);
        }
        [TestMethod]
        public void CanRepositoryGetKnowledgeField()
        {
            IQueryable<KnowledgeBranch> factKnowledgeField = repository.KnowledgeField;

            Assert.AreEqual(knowledgeField.Where(x=>x.IsActual).Count(), factKnowledgeField.Count());
            Assert.AreEqual(knowledgeField.Where(x => x.IsActual).Count(), knowledgeField.Where(x => x.IsActual).Count(s => factKnowledgeField.Contains(s)));
        }

        [TestMethod]
        public void CanRepositoryFindKnowledgeBranchById()
        {
            KnowledgeBranch knowledgeBranch = new KnowledgeBranch
            {
                Id = 1,
                Code = "07",
                Name = "Управління та адміністрування",
                IsActual = true
            };
            KnowledgeBranch factKnowledgeBranch = repository.FindById(1);
            KnowledgeBranch nullKnowledgeBranch = repository.FindById(4);

            Assert.AreEqual(knowledgeBranch, factKnowledgeBranch);
            Assert.IsNull(nullKnowledgeBranch);
        }

        [TestMethod]
        public void CanRepositoryFindKnowledgeBranchByName()
        {
            KnowledgeBranch knowledgeBranch=new KnowledgeBranch
            {
                Id = 1,
                Code = "07",
                Name = "Управління та адміністрування",
                IsActual = true
            };
            repository = new KnowledgeBranchDbRepository(dbContext);
            KnowledgeBranch factKnowledgeBranch = repository.FindByName("Управління та адміністрування");
            KnowledgeBranch nullSpeciality = repository.FindByName("Автоматизація та приладобудування");

            Assert.AreEqual(knowledgeBranch, factKnowledgeBranch);
            Assert.IsNull(nullSpeciality);
        }

        [TestMethod]
        public void CanRepositoryFindKnowledgeFieldBySpecifiedCondition()
        {
            List<KnowledgeBranch> knowledgeField = new List<KnowledgeBranch>
            {
                new KnowledgeBranch
                {
                    Id = 1,
                    Code = "07",
                    Name = "Управління та адміністрування",
                    IsActual=true
                },
                new KnowledgeBranch
                {
                    Id = 2,
                    Code = "12",
                    Name = "Інформаційні технології",
                    IsActual=true
                }
            };

            IQueryable<KnowledgeBranch> factKnoledgeField = repository.FindBySpecifiedCondition(x => x.Id == 1 ||
            x.Name == "Інформаційні технології");
            IEnumerable<KnowledgeBranch> emptySpecialities = repository.FindBySpecifiedCondition(x => x.Id == 1 &&
             x.Name == "Інформаційні технології");

            Assert.AreEqual(knowledgeField.Count(), factKnoledgeField.Count());
            Assert.AreEqual(knowledgeField.Count(), knowledgeField.Count(x => factKnoledgeField.Contains(x)));
            Assert.AreEqual(0, emptySpecialities.Count());
        }

        [TestMethod]
        public void CanRepositoryAddKnowledgeBranch()
        {
            KnowledgeBranch knowledgeBranch = new KnowledgeBranch
            {
                Id = 6,
                Code="15",
                Name = "Автоматизація та приладобудування",
                IsActual = true
            };
            bool result=repository.AddKnowledgeBranch(knowledgeBranch);

            Assert.IsTrue(result);
            Assert.AreEqual(knowledgeField.Count() + 1, dbContext.KnowledgeField.Count());
            Assert.AreEqual(knowledgeBranch, dbContext.KnowledgeField.FirstOrDefault(x => x.Id == 6));
        }

        [TestMethod]
        public void CanRepositoryUpdateSpeciality()
        {
            KnowledgeBranch knowledgeBranch = new KnowledgeBranch
            {
                Id = 1,
                Name = "Автоматизація і приладобудування",
                IsActual = true
            };
            KnowledgeBranch nonEditableKnowledgeBranch = new KnowledgeBranch
            {
                Id = 4,
                Code = "17",
                Name = "Автоматизація і приладобудування",
                IsActual = true
            };
            repository.EditKnowledgeBranch(1,knowledgeBranch);
            repository.EditKnowledgeBranch(4, nonEditableKnowledgeBranch);
            Assert.AreEqual(knowledgeField.Count(), dbContext.KnowledgeField.Count());
            Assert.AreEqual(knowledgeBranch, dbContext.KnowledgeField.FirstOrDefault(x => x.Id == 1));
            Assert.AreNotEqual(knowledgeBranch, dbContext.KnowledgeField.FirstOrDefault(x => x.Id == 4));
        }

        [TestMethod]
        public void CanRepositoryDeleteSpeciality()
        {
            repository.DeleteKnowledgeBranch(1);

            Assert.AreEqual(knowledgeField.Count(), dbContext.KnowledgeField.Count());
            Assert.IsFalse(dbContext.KnowledgeField.FirstOrDefault(x => x.Id == 1).IsActual);
        }
    }
}
