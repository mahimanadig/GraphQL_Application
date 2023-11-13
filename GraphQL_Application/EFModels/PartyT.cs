using System.ComponentModel.DataAnnotations;

namespace GraphQL_Application.EFModels
{
    public class PartyT
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}
