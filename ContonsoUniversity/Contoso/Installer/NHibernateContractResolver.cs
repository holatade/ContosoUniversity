using Domain.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Contoso
{
    public class NHibernateContractResolver : CamelCasePropertyNamesContractResolver
    {
        protected override JsonContract CreateContract(Type objectType)
        {
            if (objectType.IsAssignableFrom(typeof(Entity)))
            {
                return base.CreateObjectContract(objectType);
            }

            return base.CreateContract(objectType);
        }

        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
            JsonProperty property = base.CreateProperty(member, memberSerialization);

            property.ValueProvider = new NHibernateProxyJsonValueProvider(property.ValueProvider);

            return property;
        }
    }
}
