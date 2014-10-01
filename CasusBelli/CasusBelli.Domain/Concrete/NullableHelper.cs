using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CasusBelli.Domain.Concrete
{
    public class NullableHelper
    {
        public static Type ConvertToNullable(Type type)
        {
            if (type.IsValueType) return typeof(Nullable<>).MakeGenericType(type);
            return type;
        }

        public override string ToString()
        {
            return base.ToString();
        }

        public static T?[] ConvertArray<T>(T[] array) where T : struct
        {

            T?[] nullableArray = new T?[array.Length];

            for (int i = 0; i < array.Length; i++)

                nullableArray[i] = array[i];

            return nullableArray;

        }
    }
}
