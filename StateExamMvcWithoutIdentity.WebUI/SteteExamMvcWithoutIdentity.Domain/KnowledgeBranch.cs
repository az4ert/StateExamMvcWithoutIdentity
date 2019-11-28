using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace SteteExamMvcWithoutIdentity.Domain
{
    [Table("Field of knowledge")]
    public class KnowledgeBranch
    {
        [HiddenInput]
        public int Id
        {
            get;
            set;
        }
        [Required]
        [MinLength(2)]
        [MaxLength(2)]
        [RegularExpression(@"^\d+$")]
        public string Code { get; set; }

        [Required]
        [MinLength(1)]
        public string Name { get; set; }

        public bool IsActual { get; set; }

        public override bool Equals(object obj)
        {
            return GetHashCode() == obj.GetHashCode();
        }
        public override string ToString()
        {
            return $"{Code}. {Name}";
        }
        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }
    }
}
