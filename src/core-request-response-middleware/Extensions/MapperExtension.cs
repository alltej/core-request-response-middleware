using System.Collections.Generic;
using AutoMapper;
using System.Linq;
using System.Reflection;

namespace core_request_response_middleware.Extensions
{
    public static class MapperExtensions
    {
        public static TDestination MapTo<TDestination>(this object source)
        {
            if (!source.IsGenericList()) return Mapper.Map<TDestination>(source);

            var collection = new List<object>((IEnumerable<object>)source);
            return Mapper.Map<TDestination>(collection);
        }

        private static bool IsGenericList(this object o)
        {
            var isGenericList = false;

            var oType = o.GetType();

            if (oType.GetTypeInfo().IsGenericType && (oType.GetGenericTypeDefinition() == typeof(List<>)))
                isGenericList = true;

            return isGenericList;
        }

    }
}