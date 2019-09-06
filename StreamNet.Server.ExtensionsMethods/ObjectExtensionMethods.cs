using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Linq;

namespace StreamNet.ExtensionsMethods
{
    public static class ObjectExtensionMethods
    {
        public static T Update<T>(this T obj, T mergeobj)
        {
            var type = obj.GetType();
            foreach(var p in type.GetProperties().Where(prop => prop.CanWrite && prop.CanRead))
            {
                var prop = p.GetValue(mergeobj, null);
                if (prop == null)
                    continue;
                p.SetValue(obj, prop, null);
            }
            return obj;
        }
    }
}
