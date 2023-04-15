using AutoMapper;
using Broker.Accounts.API.Dtos;
using Broker.Accounts.Domain.Entities.Read;

namespace Broker.Accounts.API;

public class MapperDtosProfile : Profile
{
    public MapperDtosProfile()
    {
        CreateMap<Issuer, IssuerDto>()
            .ForMember(dto => dto.IssuerName, entity => entity.MapFrom(vo => vo.IssuerName.Value))
            .ForMember(dto => dto.TotalShares, entity => entity.MapFrom(vo => vo.TotalShares.Value))
            .ForMember(dto => dto.SharePrice, entity => entity.MapFrom(vo => vo.SharesPrice.Value));

        CreateMap<Account, AccountDto>()
            .ForMember(dto => dto.Id, entity => entity.MapFrom(vo => vo.UserId.Value))
            .ForMember(dto => dto.Cash, entity => entity.MapFrom(vo => vo.Cash.Value))
            .ForMember(dto => dto.Issuers, entity => entity.MapFrom(src => src.Issuers.ToArray()));
    }
}
