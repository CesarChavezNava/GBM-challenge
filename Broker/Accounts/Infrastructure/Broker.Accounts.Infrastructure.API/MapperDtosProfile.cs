using AutoMapper;
using Broker.Accounts.Domain.Entities.Read;
using Broker.Accounts.Infrastructure.API.Dtos;

namespace Broker.Accounts.Infrastructure.API;

public class MapperDtosProfile : Profile
{
    public MapperDtosProfile()
    {
        CreateMap<Issuer, IssuerDto>()
            .ForMember(dto => dto.IssuerName, entity => entity.MapFrom(vo => vo.IssuerName.Value))
            .ForMember(dto => dto.TotalShares, entity => entity.MapFrom(vo => vo.TotalShares.Value))
            .ForMember(dto => dto.SharePrice, entity => entity.MapFrom(vo => vo.SharePrice.Value));

        CreateMap<Account, AccountDto>()
            .ForMember(dto => dto.Id, entity => entity.MapFrom(vo => vo.UserId.Value))
            .ForMember(dto => dto.Cash, entity => entity.MapFrom(vo => vo.Cash.Value))
            .ForMember(dto => dto.Issuers, entity => entity.MapFrom(src => src.Issuers.ToArray()));

        CreateMap<Account, BalanceDto>()
            .ForMember(dto => dto.Cash, entity => entity.MapFrom(vo => vo.Cash.Value))
            .ForMember(dto => dto.Issuers, entity => entity.MapFrom(src => src.Issuers.ToArray()));

        CreateMap<Account, OrderDto>()
            .ForMember(dto => dto.CurrentBalance, entity => entity.MapFrom(src => src))
            .ForMember(dto => dto.BusinessErrors, entity => entity.MapFrom(vo => vo.BusinessErrors.ToArray()));

        CreateMap<Order, OperationDto>()
            .ForMember(dto => dto.Timestamp, entity => entity.MapFrom(vo => vo.Timestamp.Value))
            .ForMember(dto => dto.Operation, entity => entity.MapFrom(vo => vo.Operation.Value.ToString()))
            .ForMember(dto => dto.IssuerName, entity => entity.MapFrom(vo => vo.IssuerName.Value))
            .ForMember(dto => dto.TotalShares, entity => entity.MapFrom(vo => vo.TotalShares.Value))
            .ForMember(dto => dto.SharePrice, entity => entity.MapFrom(vo => vo.SharePrice.Value));
    }
}
