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
                    Code="07",
                    Name="Управління та адміністрування"
                },
                new KnowledgeBranch
                {
                    Code="07",
                    Name="Управління та адміністрування"
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
                    Code="07",
                    Name="Управління та адміністрування"
                },
                new KnowledgeBranch
                {
                    Code="07",
                    Name="Управління та адміністрування"
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
            string str = "07. Управління та адміністрування";

            Assert.AreEqual(str.GetHashCode(), knowledgeField[0].GetHashCode());
            Assert.AreEqual(knowledgeField[0].GetHashCode(), knowledgeField[1].GetHashCode());
            Assert.AreNotEqual(knowledgeField[0].GetHashCode(), knowledgeField[2].GetHashCode());
            Assert.AreNotEqual(knowledgeField[0].GetHashCode(), knowledgeField[3].GetHashCode());
        }
    }
}
