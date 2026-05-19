using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FitnessClub.Models
{
    [Table("Client_Membership")]
    public class ClientMembership
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public int MembershipPlanId { get; set; }

        [Required]
        public DateOnly PurchaseDate { get; set; }

        [Required]
        public DateOnly ExpiryDate { get; set; }

        [Required]
        public int VisitsUsed { get; set; } = 0;

        // Navigation
        [ForeignKey("UserId")]
        public User? User { get; set; }

        [ForeignKey("MembershipPlanId")]
        public MembershipPlan? MembershipPlan { get; set; }
    }
}
