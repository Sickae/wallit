using System;
using System.Collections.Generic;
using System.Text;
using WallIT.DataAccess.Entities.Base;
using WallIT.Shared.Enums;

namespace WallIT.DataAccess.Entities
{
    public class AccountEntity : EntityBase
    {
        public virtual string Name { get; set; }
        public virtual double Balance { get; set; }
        public virtual AccountType AccountType { get; set; }
        public virtual Currency Currency { get; set; }
        public virtual UserEntity User { get; set; }
    }
}
