namespace FitnessClub.DTOs
{
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
