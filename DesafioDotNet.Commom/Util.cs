using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace DesafioDotNet.Commom
{
    public static class Util
    {
        public static string RecuperaDescricaoEnum(this object value)
        {
            Type objType = value.GetType();
            FieldInfo[] propriedades = objType.GetFields();
            FieldInfo field = propriedades.First(p => p.Name == value.ToString());
            return ((DescriptionAttribute[])field.GetCustomAttributes(typeof(DescriptionAttribute), false))[0].Description;
        }

        public static T ConverteValor<T>(this object value, T defaultValue)
        {
            if (value != null)
                value = value.ToString().Replace(".", ",");
            T result;
            try
            {
                var requestedType = typeof(T);
                var nullableType = Nullable.GetUnderlyingType(requestedType);

                if (nullableType != null)
                {
                    if (value == null)
                    {
                        result = defaultValue;
                    }
                    else
                    {
                        result = (T)Convert.ChangeType(value, nullableType);
                    }
                }
                else
                {
                    result = (T)Convert.ChangeType(value, requestedType);
                }
            }
            catch
            {
                result = defaultValue;
            }
            return result;
        }

        public static bool IsNull(this object objeto)
        {
            return objeto == null;
        }

        public static bool IsNotNull(this object objeto)
        {
            return objeto != null;
        }
    }
}
