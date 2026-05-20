namespace FitnessClub.DTOs
{
    public class WorkoutTypeDto
    {
        public int Id { get; set; }
        public int? WorkoutTypeLvl { get; set; }
        public int UserId { get; set; }
        public string TrainerName { get; set; } = string.Empty;
        public string TypeName { get; set; } = string.Empty;
        public string? Description { get; set; }
    }

    public class CreateWorkoutTypeDto
    {
        public int? WorkoutTypeLvl { get; set; }
        public int UserId { get; set; }
        public string TypeName { get; set; } = string.Empty;
        public string? Description { get; set; }
    }

    public class UpdateWorkoutTypeDto
    {
        public int? WorkoutTypeLvl { get; set; }
        public int UserId { get; set; }
        public string TypeName { get; set; } = string.Empty;
        public string? Description { get; set; }
    }
}
