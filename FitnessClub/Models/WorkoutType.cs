using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FitnessClub.Models
{
    [Table("Workout_Type")]
    public class WorkoutType
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// Уровень сложности (FK на себя или справочник уровней)
        /// </summary>
        public int? WorkoutTypeLvl { get; set; }

        /// <summary>
        /// Тренер, отвечающий за данный тип тренировки
        /// </summary>
        [Required]
        public int UserId { get; set; }

        [Required]
        [MaxLength(100)]
        public string TypeName { get; set; } = string.Empty;

        [MaxLength(500)]
        public string? Description { get; set; }

        // Navigation
        [ForeignKey("UserId")]
        public User? User { get; set; }

        public ICollection<Workout> Workouts { get; set; } = new List<Workout>();
    }
}
