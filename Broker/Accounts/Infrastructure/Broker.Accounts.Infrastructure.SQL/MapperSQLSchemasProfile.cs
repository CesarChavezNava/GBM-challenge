using AutoMapper;
using Broker.Accounts.Domain.Entities.Write;
using Broker.Accounts.Infrastructure.SQL.Schemas;

namespace Broker.Accounts.Infrastructure.SQL;

public class MapperSQLSchemasProfile : Profile
{
    public MapperSQLSchemasProfile()
    {
        CreateMap<WriteAccount, AccountSchema>()
            .ForMember(schema => schema.UserId, entity => entity.MapFrom(vo => vo.UserId != null ? vo.UserId.Value : 0 ))
            .ForMember(schema => schema.Balance, entity => entity.MapFrom(vo => vo.Cash.Value));

        CreateMap<WriteIssuer, AccountIssuerSchema>()
            .ForMember(schema => schema.UserId, entity => entity.MapFrom(vo => vo.UserId.Value))
            .ForMember(schema => schema.IssuerName, entity => entity.MapFrom(vo => vo.IssuerName.Value))
            .ForMember(schema => schema.TotalShares, entity => entity.MapFrom(vo => vo.TotalShares.Value))
            .ForMember(schema => schema.SharePrice, entity => entity.MapFrom(vo => vo.SharePrice.Value));

        CreateMap<WriteOrder, AccountOrderSchema>()
            .ForMember(schema => schema.UserId, entity => entity.MapFrom(vo => vo.UserId.Value))
            .ForMember(schema => schema.Timestamp, entity => entity.MapFrom(vo => vo.Timestamp.Value))
            .ForMember(schema => schema.Operation, entity => entity.MapFrom(vo => vo.Operation.Value))
            .ForMember(schema => schema.IssuerName, entity => entity.MapFrom(vo => vo.IssuerName.Value))
            .ForMember(schema => schema.TotalShares, entity => entity.MapFrom(vo => vo.TotalShares.Value))
            .ForMember(schema => schema.SharePrice, entity => entity.MapFrom(vo => vo.SharePrice.Value));
    }
}
