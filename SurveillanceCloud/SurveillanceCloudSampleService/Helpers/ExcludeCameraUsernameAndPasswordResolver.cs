using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace SurveillanceCloudSampleService.Helpers
{
    //Excluding Camera's Username and Password properties from the file serialization
    public class ExcludeCameraUsernameAndPasswordResolver : DefaultContractResolver
    {
        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
            var property = base.CreateProperty(member, memberSerialization);
            property.ShouldSerialize = _ => ShouldSerialize(member);
            return property;
        }

        internal static bool ShouldSerialize(MemberInfo memberInfo)
        {
            var propertyInfo = memberInfo as PropertyInfo;
            if (propertyInfo == null)
            {
                return false;
            }

            if(propertyInfo.DeclaringType.Name == "Camera" && (propertyInfo.Name == "Username" || propertyInfo.Name == "Password"))
            {
                return false;
            }

            if (propertyInfo.SetMethod != null)
            {
                return true;
            }

            var getMethod = propertyInfo.GetMethod;
            return Attribute.GetCustomAttribute(getMethod, typeof(CompilerGeneratedAttribute)) != null;
        }

    }
}