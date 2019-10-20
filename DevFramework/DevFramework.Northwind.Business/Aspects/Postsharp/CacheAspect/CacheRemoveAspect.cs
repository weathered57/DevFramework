using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using PostSharp.Aspects;

namespace DevFramework.Northwind.Business.Aspects.Postsharp.CacheAspect
{
    public class CacheRemoveAspect : PostSharp.Aspects.OnMethodBoundaryAspect
    {
        private string _pattern;
        private Type _cacheType;
        private Caching.ICacheManager _cacheManager;

        public CacheRemoveAspect(Type cacheType)
        {
            _cacheType = cacheType;
        }
        public CacheRemoveAspect(string pattern,Type cacheType)
        {
            _pattern = pattern;
            _cacheType = cacheType;
        }

        public override void RuntimeInitialize(MethodBase method)
        {
            if (typeof(Caching.ICacheManager).IsAssignableFrom(_cacheType) == false)
            {
                throw new Exception("Wrong Cache Manager");
            }

            _cacheManager = (Caching.ICacheManager)Activator.CreateInstance(_cacheType);
        }
        public override void OnSuccess(MethodExecutionArgs args)
        {
            _cacheManager.RemoveByPattern(string.IsNullOrEmpty(_pattern) ? string.Format("{0}.{1}.*",
                args.Method.ReflectedType.Namespace, args.Method.ReflectedType.Name) : _pattern);
        }
    }
}
