using AutoMapper;
using Broker.Accounts.Domain.Entities.Write;
using Broker.Accounts.Infrastructure.SQL.Schemas;

namespace Broker.Accounts.Infrastructure.SQL;

public class MapperSQLSchemasProfile : Profile
{
    public MapperSQLSchemasProfile()
    {
        CreateMap<WriteAccount, AccountSchema>()
            .ForMember(schema => schema.Balance, entity => entity.MapFrom(vo => vo.Cash.Value));
    }
}
