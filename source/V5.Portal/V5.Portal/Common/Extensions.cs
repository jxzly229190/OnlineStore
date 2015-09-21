using System;
using System.Collections.Generic;
using System.Linq;

namespace V5.Portal.Common
{
    public static class Extensions
    {
        //根据条件删除对象
        public static bool Remove<T>(this IList<T> list, Func<T, bool> func)
        {
	        var item = list.FirstOrDefault(func);

	        if (item == null)
	        {
		        return true;
	        }

            return list.Remove(item);
        }
    }
}