using Microsoft.VisualStudio.TestTools.UnitTesting;
using SteteExamMvcWithoutIdentity.Domain;
using System.Collections.Generic;

namespace StateExamMvcWithoutIdentity.UnitTests
{
    [TestClass]
    public class KnowledgeBranchModelTests
    {
        [TestMethod]
        public void CanModelEqualsKnowledgeBranch()
        {
            List<KnowledgeBranch> knowledgeField = new List<KnowledgeBranch>
            {
                new KnowledgeBranch
                {
                    Id=1,
                    Code="07",
                    Name="Управління та адміністрування",
                    IsActual=true
                },
                new KnowledgeBranch
                {
                    Id=1,
                    Code="07",
                    Name="Управління та адміністрування",
                    IsActual=true
                },
                new KnowledgeBranch
                {
                    Id=2,
                    Code="07",
                    Name="Управління та адміністрування",
                    IsActual=true
                },
                new KnowledgeBranch
                {
                    Id=1,
                    Code="07",
                    Name="Управління та адміністрування",
                    IsActual=false
                },
                new KnowledgeBranch
                {
                    Code="08",
                    Name="Управління та адміністрування"
                },
                new KnowledgeBranch
                {
                    Code="07",
                    Name="Управління і адміністрування"
                }
            };
            Assert.IsTrue(knowledgeField[0].Equals(knowledgeField[1]));
            Assert.IsFalse(knowledgeField[0].Equals(knowledgeField[2]));
            Assert.IsFalse(knowledgeField[0].Equals(knowledgeField[3]));
            Assert.IsFalse(knowledgeField[0].Equals(knowledgeField[3]));
            Assert.IsFalse(knowledgeField[0].Equals(knowledgeField[3]));
        }

        [TestMethod]
        public void CanModelConvertKnowledgeBranchToString()
        {
            KnowledgeBranch knowledgeBranch = new KnowledgeBranch
            {
                Code = "07",
                Name = "Управління та адміністрування"
            };
            string str = "07. Управління та адміністрування";

            Assert.AreEqual(str, knowledgeBranch.ToString());
        }

        [TestMethod]
        public void CanModelGenerateKnowledgeBranchHashCode()
        {
            List<KnowledgeBranch> knowledgeField = new List<KnowledgeBranch>
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
                    Id=1,
                    Code="07",
                    Name="Управління та адміністрування",
                    IsActual=true

                },
                new KnowledgeBranch
                {
                    Id=1,
                    Code="07",
                    Name="Управління та адміністрування",
                    IsActual=false

                },
                new KnowledgeBranch
                {
                    Id=1,
                    Code="08",
                    Name="Управління та адміністрування"
                },
                new KnowledgeBranch
                {
                    Id=1,
                    Code="07",
                    Name="Управління і адміністрування"
                },
                new KnowledgeBranch
                {
                    Id=2,
                    Code="07",
                    Name="Управління та адміністрування",
                    IsActual=true

                },
            };
            string str = "1 07. Управління та адміністрування True";

            Assert.AreEqual(str.GetHashCode(), knowledgeField[0].GetHashCode());
            Assert.AreEqual(knowledgeField[0].GetHashCode(), knowledgeField[1].GetHashCode());
            Assert.AreNotEqual(knowledgeField[0].GetHashCode(), knowledgeField[2].GetHashCode());
            Assert.AreNotEqual(knowledgeField[0].GetHashCode(), knowledgeField[3].GetHashCode());
            Assert.AreNotEqual(knowledgeField[0].GetHashCode(), knowledgeField[4].GetHashCode());
            Assert.AreNotEqual(knowledgeField[0].GetHashCode(), knowledgeField[5].GetHashCode());
        }
    }
}
