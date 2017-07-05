using Microsoft.AspNetCore.Authorization.Infrastructure;

namespace TicketSaleCore.AuthorizationPolit.ResourceBased
{
    public static class Operations
    {
        public const string ClaimTypeForDbWork = "CRUD";

        public static OperationAuthorizationRequirement Create
            = new OperationAuthorizationRequirement { Name = "Create" };
        public static OperationAuthorizationRequirement Read
            = new OperationAuthorizationRequirement { Name = "Read" };
        public static OperationAuthorizationRequirement Update
            = new OperationAuthorizationRequirement { Name = "Update" };
        public static OperationAuthorizationRequirement Delete
            = new OperationAuthorizationRequirement { Name = "Delete" };
    }
}