using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FitnessClub.Models
{
    [Table("Users")]
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int RoleId { get; set; }

        [Required]
        [MaxLength(100)]
        public string FullName { get; set; } = string.Empty;

        [MaxLength(30)]
        public string? PhoneNumber { get; set; }

        [MaxLength(100)]
        public string? Email { get; set; }

        [Required]
        [MaxLength(255)]
        public string PasswordHash { get; set; } = string.Empty;

        // Navigation
        [ForeignKey("RoleId")]
        public Role? Role { get; set; }

        public ICollection<Workout> Workouts { get; set; } = new List<Workout>();
        public ICollection<WorkoutRegistration> WorkoutRegistrations { get; set; } = new List<WorkoutRegistration>();
        public ICollection<ClientMembership> ClientMemberships { get; set; } = new List<ClientMembership>();
        public ICollection<WorkoutType> WorkoutTypes { get; set; } = new List<WorkoutType>();
    }
}
