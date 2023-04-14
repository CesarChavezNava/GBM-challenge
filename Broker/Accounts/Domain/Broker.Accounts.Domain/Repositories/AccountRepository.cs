﻿using Broker.Accounts.Domain.Entities.Read;
using Broker.Accounts.Domain.Entities.Write;

namespace Broker.Accounts.Domain.Repositories;

public interface IAccountRepository
{
    Task<Account> Create(WriteAccount account);
}
