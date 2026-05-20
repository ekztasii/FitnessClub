namespace FitnessClub.DTOs
{
    public class WorkoutDto
    {
        public int Id { get; set; }
        public int WorkoutTypeId { get; set; }
        public string WorkoutTypeName { get; set; } = string.Empty;
        public int UserId { get; set; }
        public string TrainerName { get; set; } = string.Empty;
        public DateTime WorkoutDate { get; set; }
        public int MaxParticipants { get; set; }
        public int CurrentParticipants { get; set; }
    }

    public class CreateWorkoutDto
    {
        public int WorkoutTypeId { get; set; }
        public int UserId { get; set; }
        public DateTime WorkoutDate { get; set; }
        public int MaxParticipants { get; set; }
    }

    public class UpdateWorkoutDto
    {
        public int WorkoutTypeId { get; set; }
        public int UserId { get; set; }
        public DateTime WorkoutDate { get; set; }
        public int MaxParticipants { get; set; }
    }
}
