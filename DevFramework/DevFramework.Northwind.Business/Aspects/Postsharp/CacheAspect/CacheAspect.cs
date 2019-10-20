using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using PostSharp.Aspects;
using System.Linq;

namespace DevFramework.Northwind.Business.Aspects.Postsharp.CacheAspect
{
    [Serializable]
    public class CacheAspect : MethodInterceptionAspect
    {
        private Type _cacheType;
        private int _cacheByMinute;
        private Caching.ICacheManager _cacheManager;
        public CacheAspect(Type cacheType,int cacheByMinute=60)
        {
            _cacheType = cacheType;
            _cacheByMinute = cacheByMinute;
        }

        public override void RuntimeInitialize(MethodBase method)
        {
           if(typeof(Caching.ICacheManager).IsAssignableFrom(_cacheType)==false)
            {
                throw new Exception("Wrong Cache Manager");
            }

            _cacheManager = (Caching.ICacheManager)Activator.CreateInstance(_cacheType);
        }

        //Methoda girmdeden önce
        //args.Method.ReflectedType.Namespace => namespace adı
        //args.Method.ReflectedType.Name =>class adı
        //args.Method.Name => method adı
        public override void OnInvoke(MethodInterceptionArgs args)
        {
            var methodName = string.Format("{0}.{1}.{2}", args.Method.ReflectedType.Namespace, args.Method.ReflectedType.Name, args.Method.Name);
            var arguments = args.Arguments.ToList();

            var key= string.Format("{0}({1})", methodName,string.Join(",",arguments.Select(x=>x != null ? x.ToString() :"<Null>")));

            if(_cacheManager.IsAdd(key))
            {
                args.ReturnValue = _cacheManager.Get<Object>(key);
            }
            base.OnInvoke(args);
            _cacheManager.Add(key, args.ReturnValue, _cacheByMinute);
        }
    }
}
