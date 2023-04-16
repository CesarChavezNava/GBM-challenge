using Broker.Core.Enums;
using Broker.Core.Exceptions;

namespace Broker.Core.ValueObjects
{
    public class SearchOrder : ValueObjectForEnum<SearchOrderCode>
    {
        public SearchOrder(string value)
        : base(value, true)
        { }

        public SearchOrder(SearchOrderCode value)
            : base(value)
        { }

        protected override void Check(string value)
        {
            if (!Enum.IsDefined(typeof(SearchOrderCode), value))
                throw new InvalidSearchOrderCodeException();
        }
    }
}
