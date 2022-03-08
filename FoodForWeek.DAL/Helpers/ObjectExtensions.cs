using System;

namespace FoodForWeek.DAL.Helpers
{
    public static class ObjectExtensions
    {
        public static void CheckNull(this object checkedObject, string argumentName, string message)
        {
            if (checkedObject is null) throw new ArgumentNullException(argumentName, message);
            return;
        }
        public static void CheckNull(this object checkedObject, string message)
        {
            if (checkedObject is null) throw new ArgumentNullException(message);
            return;
        }
    }
}
