using WallIT.Shared.DTOs.Base;
using WallIT.Shared.Enums;

namespace WallIT.Shared.DTOs
{
    public class AccountDTO : DTOBase
    {
        public string Name { get; set; }

        public double Balance { get; set; }

        public AccountType AccountType { get; set; }

        public Currency Currency { get; set; }

        public int? UserId { get; set; }

        public UserDTO User { get; set; }
    }
}
