using System;

namespace Util
{
    public static class RandomValues {
        public static T RandomEnumValue<T> ()
        {
            var v = Enum.GetValues(typeof (T));
            return (T)v.GetValue(new System.Random().Next(v.Length));
        }
    }
}
