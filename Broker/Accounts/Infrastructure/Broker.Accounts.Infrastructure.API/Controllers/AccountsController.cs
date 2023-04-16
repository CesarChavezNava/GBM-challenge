using AutoMapper;
using Broker.Accounts.Application.Create;
using Broker.Accounts.Domain.Entities.Read;
using Broker.Accounts.Domain.Entities.Write;
using Broker.Accounts.Domain.ValueObjects;
using Broker.Accounts.Infrastructure.API.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Broker.Accounts.Infrastructure.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccountsController : ControllerBase
{
    private readonly IMapper mapper;
    private readonly IForCreateAccount createAccountPort;
    public AccountsController(IMapper mapper, IForCreateAccount createAccountPort)
    {
        this.mapper = mapper;
        this.createAccountPort = createAccountPort;
    }

    [HttpPost]
    public async Task<IActionResult> Post(CreateAccountDto createAccountDto)
    {
        WriteAccount writeAccount = new WriteAccount(new Cash(createAccountDto.Cash));
        Account account = await createAccountPort.Create(writeAccount);
        AccountDto accountDto = mapper.Map<AccountDto>(account);

        return Created("", accountDto);
    }
}
