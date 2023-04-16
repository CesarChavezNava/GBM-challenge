using AutoMapper;
using Broker.Accounts.Application.Create;
using Broker.Accounts.Application.Find;
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
    private readonly IForCreateOrder createOrderPort;
    private readonly IForFindAccount findAccountPort;
    public AccountsController(IMapper mapper, 
        IForCreateAccount createAccountPort,
        IForCreateOrder createOrderPort,
        IForFindAccount findAccountPort)
    {
        this.mapper = mapper;
        this.createAccountPort = createAccountPort;
        this.createOrderPort = createOrderPort;
        this.findAccountPort = findAccountPort;
    }

    [HttpPost]
    public async Task<IActionResult> Post(CreateAccountDto createAccountDto)
    {
        WriteAccount writeAccount = new WriteAccount(new Cash(createAccountDto.Cash));
        Account account = await createAccountPort.Create(writeAccount);
        AccountDto accountDto = mapper.Map<AccountDto>(account);

        return Created("", accountDto);
    }

    [HttpGet("{userId}")]
    public async Task<IActionResult> Get(int userId)
    {
        Account account = await findAccountPort.Find(new UserId(userId));
        AccountDto accountDto = mapper.Map<AccountDto>(account);

        return Ok(accountDto);
    }

    [HttpPost("{userId}/orders")]
    public async Task<IActionResult> PostOrder(int userId, CreateOrderDto createOrderDto)
    {
        WriteOrder writeOrder = new WriteOrder(
            new UserId(userId),
            new Timestamp(createOrderDto.Timestamp),
            new Operation(createOrderDto.Operation),
            new IssuerName(createOrderDto.IssuerName),
            new TotalShares(createOrderDto.TotalShares),
            new SharePrice(createOrderDto.SharePrice));

        Account account = await createOrderPort.Create(writeOrder);
        BalanceDto currentBalance = mapper.Map<BalanceDto>(account);

        return Created("", currentBalance);
    }
}
