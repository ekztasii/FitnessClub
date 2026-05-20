namespace FitnessClub.DTOs
{
    public class WorkoutRegistrationDto
    {
        public int Id { get; set; }
        public int WorkoutId { get; set; }
        public DateTime WorkoutDate { get; set; }
        public string WorkoutTypeName { get; set; } = string.Empty;
        public int UserId { get; set; }
        public string ClientName { get; set; } = string.Empty;
        public DateTime RegistrationDate { get; set; }
        public bool AttendanceMarked { get; set; }
    }

    public class CreateWorkoutRegistrationDto
    {
        public int WorkoutId { get; set; }
        public int UserId { get; set; }
    }

    public class MarkAttendanceDto
    {
        public bool AttendanceMarked { get; set; }
    }
}
