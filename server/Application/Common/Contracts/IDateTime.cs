using System;

namespace Application.Common.Contracts
{
    public interface IDateTime
    {
        DateTime Now { get; }
    }
}