using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using StateExamMvc.Services;
using StateExamMvcWithoutIdentity.Repositories;
using SteteExamMvcWithoutIdentity.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StateExamMvcWithoutIdentity.UnitTests
{
    [TestClass]
    public class KnowledgeBranchServiceTests
    {
        private Mock<IKnowledgeBranchRepository> mockRepository;
        private IQueryable<KnowledgeBranch> knowledgeField;
        private KnowledgeBranchService service;
        private TestContext testContextInstance;

        private bool OrCondition(KnowledgeBranch knowledgeBranch)
        {
            return knowledgeBranch.IsActual &&
                (knowledgeBranch.Id == 1 || knowledgeBranch.Name == "Інформаційні технології");
        }

        private bool AndCondition(KnowledgeBranch knowledgeBranch)
        {
            return (knowledgeBranch.Id == 1 && knowledgeBranch.Name == "Інформаційні технології") &&
                knowledgeBranch.IsActual;
        }

        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }

            set
            {
                testContextInstance = value;
            }
        }

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
                    IsActual=false
                },
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
            mockRepository = new Mock<IKnowledgeBranchRepository>();
            mockRepository.Setup(x => x.KnowledgeField).Returns(knowledgeField.Where(x=>x.IsActual));
            mockRepository.Setup(x => x.FindById(1)).Returns(knowledgeField.ElementAt(1));
            mockRepository.Setup(x => x.FindById(It.Is<int>(id => id != 1))).Returns((KnowledgeBranch)null);
            mockRepository.Setup(x => x.FindByCode("07")).Returns(knowledgeField.ElementAt(1));
            mockRepository.Setup(x => x.FindByCode(It.Is<string>(code => code != "07"))).Returns((KnowledgeBranch)null);
            mockRepository.Setup(x => x.FindByName("Управління та адміністрування")).Returns(knowledgeField.ElementAt(1));
            mockRepository.Setup(x => x.FindByName(It.Is<string>(name => name != "Управління та адміністрування"))).Returns(
                (KnowledgeBranch)null);
            mockRepository.Setup(x => x.FindBySpecifiedCondition(OrCondition)).Returns(
                knowledgeField.Where(OrCondition).AsQueryable());
            mockRepository.Setup(x => x.FindBySpecifiedCondition(AndCondition)).Returns(
                knowledgeField.Where(AndCondition).AsQueryable());
            mockRepository.Setup(x => x.AddKnowledgeBranch(It.Is<KnowledgeBranch>(kb=>kb.Id==1))).Returns(true);
            mockRepository.Setup(x => x.AddKnowledgeBranch(It.Is<KnowledgeBranch>(
                knowledgeBranch => knowledgeBranch.Id!=1))).Returns(false);
            mockRepository.Setup(x => x.EditKnowledgeBranch(1, It.Is<KnowledgeBranch>(kb => kb.Id == 1))).Returns(true);
            mockRepository.Setup(x => x.EditKnowledgeBranch(1,It.Is<KnowledgeBranch>(
                knowledgeBranch => knowledgeBranch.Id != 1))).Returns(false);
            mockRepository.Setup(x => x.DeleteKnowledgeBranch(1)).Returns(true);
            mockRepository.Setup(x => x.DeleteKnowledgeBranch(It.Is<int>(id=>id!=1))).Returns(false);
            service = new KnowledgeBranchService(mockRepository.Object);
        }
        [TestMethod]
        public void CanServiceGetKnowledgeField()
        {
            IQueryable<KnowledgeBranch> factKnowledgeField = service.GetKnowledgeField();

            Assert.AreEqual(knowledgeField.Where(x=>x.IsActual).Count(), factKnowledgeField.Count());
            Assert.AreEqual(knowledgeField.Where(x => x.IsActual).Count(), knowledgeField.Where(x => x.IsActual).Count(s => factKnowledgeField.Contains(s)));
        }

        [TestMethod]
        public void CanServiceFindKnowledgeBranchById()
        {
            KnowledgeBranch knowledgeBranch = new KnowledgeBranch
            {
                Id = 1,
                Code = "07",
                Name = "Управління та адміністрування",
                IsActual = true
            };
            KnowledgeBranch factKnowledgeBranch = service.GetById(1);
            KnowledgeBranch nullKnowledgeBranch = service.GetById(4);

            Assert.AreEqual(knowledgeBranch, factKnowledgeBranch);
            Assert.IsNull(nullKnowledgeBranch);
        }

        [TestMethod]
        public void CanServiceFindKnowledgeBranchByName()
        {
            KnowledgeBranch knowledgeBranch=new KnowledgeBranch
            {
                Id = 1,
                Code = "07",
                Name = "Управління та адміністрування",
                IsActual = true
            };
            KnowledgeBranch factKnowledgeBranch = service.GetByName("Управління та адміністрування");
            KnowledgeBranch nullKnowledgeBranch = service.GetByName("Автоматизація та приладобудування");

            Assert.AreEqual(knowledgeBranch, factKnowledgeBranch);
            Assert.IsNull(nullKnowledgeBranch);
        }

        [TestMethod]
        public void CanServiceFindKnowledgeBranchByCode()
        {
            KnowledgeBranch knowledgeBranch = new KnowledgeBranch
            {
                Id = 1,
                Code = "07",
                Name = "Управління та адміністрування",
                IsActual = true
            };
            KnowledgeBranch factKnowledgeBranch = service.GetByCode("07");
            KnowledgeBranch nullKnowledgeBranch = service.GetByCode("15");

            Assert.AreEqual(knowledgeBranch, factKnowledgeBranch);
            Assert.IsNull(nullKnowledgeBranch);
        }

        [TestMethod]
        public void CanServiceFindKnowledgeFieldBySpecifiedCondition()
        {
            string currentDirectory = Environment.CurrentDirectory;
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

            IQueryable<KnowledgeBranch> factKnoledgeField = service.GetBySpecifiedCondition(OrCondition);
            IEnumerable<KnowledgeBranch> emptySpecialities = service.GetBySpecifiedCondition(AndCondition);

            Assert.AreEqual(knowledgeField.Count(), factKnoledgeField.Count());
            Assert.AreEqual(knowledgeField.Count(), knowledgeField.Count(x => factKnoledgeField.Contains(x)));
            Assert.AreEqual(0, emptySpecialities.Count());
        }

        [TestMethod]
        [DeploymentItem("XMLData/KnowledgeBranch/KnowledgeBranchData.xml")]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.XML", "KnowledgeBranchData.xml","KnowledgeBranch",
            DataAccessMethod.Sequential)]
        public void CanServiceAddKnowledgeBranch()
        {
            int id = int.Parse(TestContext.DataRow["id"].ToString());
            string code = TestContext.DataRow["code"].ToString();
            string name = TestContext.DataRow["name"].ToString();
            bool isActual = bool.Parse(TestContext.DataRow["actual"].ToString());
            KnowledgeBranchError error = (KnowledgeBranchError)Enum.Parse(typeof(KnowledgeBranchError), 
                TestContext.DataRow["error"].ToString());

            KnowledgeBranch knowledgeBranch = new KnowledgeBranch
            {
                Id = id,
                Code=code,
                Name = name,
                IsActual =isActual
            };
            
            KnowledgeBranchError result = service.Add(knowledgeBranch);

            Assert.AreEqual(error, result);
        }

        [TestMethod]
        [DeploymentItem("XMLData/KnowledgeBranch/KnowledgeBranchData.xml")]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.XML", "KnowledgeBranchData.xml", "KnowledgeBranch",
            DataAccessMethod.Sequential)]
        public void CanServiceUpdateKnowledgeBranch()
        {
            int id = int.Parse(TestContext.DataRow["id"].ToString());
            string code = TestContext.DataRow["code"].ToString();
            string name = TestContext.DataRow["name"].ToString();
            bool isActual = bool.Parse(TestContext.DataRow["actual"].ToString());
            KnowledgeBranchError error = (KnowledgeBranchError)Enum.Parse(typeof(KnowledgeBranchError),
                TestContext.DataRow["error"].ToString());

            KnowledgeBranch knowledgeBranch = new KnowledgeBranch
            {
                Id = id,
                Code = code,
                Name = name,
                IsActual = isActual
            };

            KnowledgeBranchError result = service.Edit(1,knowledgeBranch);

            Assert.AreEqual(error, result);
        }

        [TestMethod]
        public void CanServiceDeleteKnowledgeBranch()
        {
            KnowledgeBranchError result=service.Delete(1);
            KnowledgeBranchError errorResult = service.Delete(4);

            Assert.AreEqual(KnowledgeBranchError.None, result);
            Assert.AreEqual(KnowledgeBranchError.DbError, errorResult);
        }
    }
}
