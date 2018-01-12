using System;

namespace Reafftec.CatchEmAll.WebJobs.Helper
{
    public static class Ensure
    {
        public static void NotNull(object value, string name)
        {
            if (value == null)
            {
                throw new Exception($"'{name}' must not be null!");
            }
        }
    }
}
