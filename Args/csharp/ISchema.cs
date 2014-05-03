using System;

namespace Args
{
    public interface ISchema
    {
        bool ContainsFlag(char flag);
        Type GetTypeOfValue(char flag);
    }
}