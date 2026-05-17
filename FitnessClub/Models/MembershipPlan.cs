using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FitnessClub.Models
{
    [Table("Membership_Plans")]
    public class MembershipPlan
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string PlanName { get; set; } = string.Empty;

        [Required]
        [Column(TypeName = "decimal(10,2)")]
        public decimal Price { get; set; }

        [Required]
        public int ValidityDays { get; set; }

        [Required]
        public int AllowedVisits { get; set; }

        // Navigation
        public ICollection<ClientMembership> ClientMemberships { get; set; } = new List<ClientMembership>();
    }
}
