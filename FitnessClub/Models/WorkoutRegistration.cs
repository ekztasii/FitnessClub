using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FitnessClub.Models
{
    [Table("Workout_Registrations")]
    public class WorkoutRegistration
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int WorkoutId { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public DateTime RegistrationDate { get; set; } = DateTime.UtcNow;

        public bool AttendanceMarked { get; set; } = false;

        // Navigation
        [ForeignKey("WorkoutId")]
        public Workout? Workout { get; set; }

        [ForeignKey("UserId")]
        public User? User { get; set; }
    }
}
