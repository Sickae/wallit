﻿using System.Collections.Generic;

namespace WallIT.Shared.Transaction
{
    public class TransactionResult
    {
        public int? Id { get; set; }

        public bool Succeeded { get; set; }

        public IList<TransactionErrorMessage> ErrorMessages { get; set; } = new List<TransactionErrorMessage>();
    }
}
