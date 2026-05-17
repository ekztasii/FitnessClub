using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FitnessClub.Models
{
    [Table("Workouts")]
    public class Workout
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int WorkoutTypeId { get; set; }

        /// <summary>
        /// Тренер, проводящий тренировку
        /// </summary>
        [Required]
        public int UserId { get; set; }

        [Required]
        public DateTime WorkoutDate { get; set; }

        [Required]
        public int MaxParticipants { get; set; }

        // Navigation
        [ForeignKey("WorkoutTypeId")]
        public WorkoutType? WorkoutType { get; set; }

        [ForeignKey("UserId")]
        public User? User { get; set; }

        public ICollection<WorkoutRegistration> WorkoutRegistrations { get; set; } = new List<WorkoutRegistration>();
    }
}
