

namespace Interact.GateInvitations.Common.Constants
{
    public class GateAppConfig
    {
        public const string SqlServerDatabaseName = "sqlserver";
        public const string SqliteDatabaseName = "sqlite";
        public const string LocalDatabaseName = "localSql";
        public const string CustomerPolicy = "Gate_Customer_Policy";
        public const string SecurityKeeperPolicy = "Gate_SecurityKeeper_Policy";
        public const string AdminPolicy = "Gate_Admin_Policy";
        public const string ActivatedCustomerPolicy = "Activated_Customer_Policy";
        public static class UserClaims
        {
            public const string UserNameClaim = "UserName";
            public const string UserID = "UserId";
            public const string IsActive = "IsActive";
            //public const string UserNameClaim = "Username";
        }
    }
}
