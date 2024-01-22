using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace Application.Common.Behaviours
{
    public static class CustomValidator
    {
        public static IRuleBuilderOptions<T, string> FullName<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder
                .MinimumLength(10)
                .Must(val => val.Split(" ").Length >= 2);
        }
    }
}
