using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLIappDT.Helpers;

public static class CheckHelper
{
    public static void CheckNull<T>(T? obj, string? customExeption = null) where T : class
    {
        if (obj == null)
        {
            if (!string.IsNullOrEmpty(customExeption))
            {
                throw new ArgumentNullException(nameof(T), $"{customExeption}");
            }
            throw new ArgumentNullException(nameof(T), $"{typeof(T).Name} cannot be null");
        }
    }
}
