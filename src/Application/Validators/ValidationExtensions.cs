using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validators
{
    public static class ValidationExtensions
    {
        public static void ThrowIfEmpty(this long id, string parameterName)
        {
            if (id < 0)
            {
                throw new ArgumentException($"{parameterName} cannot be empty.", parameterName);
            }
        }

        public static void ThrowIfNull<T>(this T obj, string parameterName) where T : class
        {
            if (obj == null)
            {
                throw new ArgumentNullException(parameterName);
            }
        }
    }
}
