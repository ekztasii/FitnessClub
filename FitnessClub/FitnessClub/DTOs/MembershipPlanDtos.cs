namespace FitnessClub.DTOs
{
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

    public class UpdateMembershipPlanDto
    {
        public string PlanName { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int ValidityDays { get; set; }
        public int AllowedVisits { get; set; }
    }
}
