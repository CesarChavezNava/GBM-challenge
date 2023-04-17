using AutoMapper;
using Broker.Accounts.Application.Create;
using Broker.Accounts.Application.Find;
using Broker.Accounts.Application.Search;
using Broker.Accounts.Domain.Entities.Criteria;
using Broker.Accounts.Domain.Entities.Read;
using Broker.Accounts.Domain.Entities.Write;
using Broker.Accounts.Domain.ValueObjects;
using Broker.Accounts.Infrastructure.API.Dtos;
using Broker.Core.Entities;
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
    private readonly IForSearchOrders searchOrdersPort;
    public AccountsController(IMapper mapper, 
        IForCreateAccount createAccountPort,
        IForCreateOrder createOrderPort,
        IForFindAccount findAccountPort,
        IForSearchOrders searchOrdersPort)
    {
        this.mapper = mapper;
        this.createAccountPort = createAccountPort;
        this.createOrderPort = createOrderPort;
        this.findAccountPort = findAccountPort;
        this.searchOrdersPort = searchOrdersPort;
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
        OrderDto orderDto = mapper.Map<OrderDto>(account);

        return Created("", orderDto);
    }

    [HttpGet("{userId}/orders")]
    public async Task<IActionResult> GetOrders(int userId, [FromQuery] SearchOrdersDto searchOrdersDto)
    {
        Criteria<OrderFilters> criteria = new(
            new OrderFilters(new(userId), searchOrdersDto.IssuerName, searchOrdersDto.Operation),
            new(searchOrdersDto.Order ?? "DESC"),
            searchOrdersDto.Limit != null ? new((int)searchOrdersDto.Limit) : null,
            searchOrdersDto.Offset != null ? new((int)searchOrdersDto.Offset) : null
        );

        Orders orders = await searchOrdersPort.Search(criteria);
        OperationDto[] operationDto = mapper.Map<Order[], OperationDto[]>(orders.ToArray());

        return Ok(operationDto);
    }
}
