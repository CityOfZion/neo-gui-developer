using Neo.Core;

namespace Neo.SmartContract
{
    public class ContractParameter
    {
        public ContractParameterType Type;
        public object Value;

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}
