using Newtonsoft.Json.Serialization;
using NHibernate.Collection;
using NHibernate.Proxy;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Contoso
{
    public class NHibernateProxyJsonValueProvider : IValueProvider
    {
        private readonly IValueProvider _valueProvider;

        public NHibernateProxyJsonValueProvider(IValueProvider valueProvider)
        {
            _valueProvider = valueProvider;
        }

        public void SetValue(object target, object value)
        {
            _valueProvider.SetValue(target, value);
        }

        private static (bool isProxy, bool isInitialized) GetProxy(object proxy)
        {
            // this is pretty much what NHibernateUtil.IsInitialized() does.
            switch (proxy)
            {
                case INHibernateProxy hibernateProxy:
                    return (true, !hibernateProxy.HibernateLazyInitializer.IsUninitialized);
                case ILazyInitializedCollection initializedCollection:
                    return (true, initializedCollection.WasInitialized);
                case IPersistentCollection persistentCollection:
                    return (true, persistentCollection.WasInitialized);
                default:
                    return (false, false);
            }
        }

        public object GetValue(object target)
        {
            object value = _valueProvider.GetValue(target);
            (bool isProxy, bool isInitialized) = GetProxy(value);
            if (isProxy)
            {
                if (isInitialized)
                {
                    return value;
                }

                if (value is IEnumerable)
                {
                    return Enumerable.Empty<object>();
                }

                return null;
            }

            return value;
        }
    }
}
