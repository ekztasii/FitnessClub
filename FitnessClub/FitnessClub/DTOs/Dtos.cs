namespace FitnessClub.DTOs
{
    // ── Role ──────────────────────────────────────────────────────────────────
    public class RoleDto
    {
        public int Id { get; set; }
        public string RoleName { get; set; } = string.Empty;
    }

    public class CreateRoleDto
    {
        public string RoleName { get; set; } = string.Empty;
    }

    // ── User ──────────────────────────────────────────────────────────────────
    public class UserDto
    {
        public int Id { get; set; }
        public int RoleId { get; set; }
        public string RoleName { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
    }

    public class CreateUserDto
    {
        public int RoleId { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public string Password { get; set; } = string.Empty;
    }

    public class UpdateUserDto
    {
        public int RoleId { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
    }

    // ── WorkoutType ───────────────────────────────────────────────────────────
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

    // ── Workout ───────────────────────────────────────────────────────────────
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

    // ── WorkoutRegistration ───────────────────────────────────────────────────
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

    // ── MembershipPlan ────────────────────────────────────────────────────────
    public class MembershipPlanDto
    {
        public int Id { get; set; }
        public string PlanName { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int ValidityDays { get; set; }
        public int AllowedVisits { get; set; }
    }

    public class CreateMembershipPlanDto
    {
        public string PlanName { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int ValidityDays { get; set; }
        public int AllowedVisits { get; set; }
    }

    // ── ClientMembership ──────────────────────────────────────────────────────
    public class ClientMembershipDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string ClientName { get; set; } = string.Empty;
        public int MembershipPlanId { get; set; }
        public string PlanName { get; set; } = string.Empty;
        public DateOnly PurchaseDate { get; set; }
        public DateOnly ExpiryDate { get; set; }
        public int VisitsUsed { get; set; }
        public int AllowedVisits { get; set; }
        public bool IsActive { get; set; }
    }

    public class CreateClientMembershipDto
    {
        public int UserId { get; set; }
        public int MembershipPlanId { get; set; }
        public DateOnly PurchaseDate { get; set; }
    }
}
