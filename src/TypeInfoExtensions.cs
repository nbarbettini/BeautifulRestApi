using System;
using System.Collections.Generic;
using System.Reflection;

namespace BeautifulRestApi
{
    public static class TypeInfoExtensions
    {
        /// <summary>
        /// Gets a collection of the constructors defined by the current type and any base types.
        /// </summary>
        /// <param name="typeInfo">The type info.</param>
        /// <remarks>Equivalent to returning <see cref="TypeInfo.DeclaredConstructors"/> from this type and its <see cref="TypeInfo.BaseType"/>, recursively up the inheritance graph.</remarks>
        /// <returns>A collection of the constructors defined by the current type and any base types.</returns>
        public static IEnumerable<ConstructorInfo> GetAllConstructors(this TypeInfo typeInfo)
            => typeInfo.GetAll(ti => ti.DeclaredConstructors);

        /// <summary>
        /// Gets a collection of the events defined by the current type and any base types.
        /// </summary>
        /// <param name="typeInfo">The type info.</param>
        /// <remarks>Equivalent to returning <see cref="TypeInfo.DeclaredEvents"/> from this type and its <see cref="TypeInfo.BaseType"/>, recursively up the inheritance graph.</remarks>
        /// <returns>A collection of the events defined by the current type and any base types.</returns>
        public static IEnumerable<EventInfo> GetAllEvents(this TypeInfo typeInfo)
            => typeInfo.GetAll(ti => ti.DeclaredEvents);

        /// <summary>
        /// Gets a collection of the fields defined by the current type and any base types.
        /// </summary>
        /// <param name="typeInfo">The type info.</param>
        /// <remarks>Equivalent to returning <see cref="TypeInfo.DeclaredFields"/> from this type and its <see cref="TypeInfo.BaseType"/>, recursively up the inheritance graph.</remarks>
        /// <returns>A collection of the fields defined by the current type and any base types.</returns>
        public static IEnumerable<FieldInfo> GetAllFields(this TypeInfo typeInfo)
            => typeInfo.GetAll(ti => ti.DeclaredFields);

        /// <summary>
        /// Gets a collection of the members defined by the current type and any base types.
        /// </summary>
        /// <param name="typeInfo">The type info.</param>
        /// <remarks>Equivalent to returning <see cref="TypeInfo.DeclaredMembers"/> from this type and its <see cref="TypeInfo.BaseType"/>, recursively up the inheritance graph.</remarks>
        /// <returns>A collection of the members defined by the current type and any base types.</returns>
        public static IEnumerable<MemberInfo> GetAllMembers(this TypeInfo typeInfo)
            => typeInfo.GetAll(ti => ti.DeclaredMembers);

        /// <summary>
        /// Gets a collection of the methods defined by the current type and any base types.
        /// </summary>
        /// <param name="typeInfo">The type info.</param>
        /// <remarks>Equivalent to returning <see cref="TypeInfo.DeclaredMethods"/> from this type and its <see cref="TypeInfo.BaseType"/>, recursively up the inheritance graph.</remarks>
        /// <returns>A collection of the methods defined by the current type and any base types.</returns>
        public static IEnumerable<MethodInfo> GetAllMethods(this TypeInfo typeInfo)
            => typeInfo.GetAll(ti => ti.DeclaredMethods);

        /// <summary>
        /// Gets a collection of the nested types defined by the current type and any base types.
        /// </summary>
        /// <param name="typeInfo">The type info.</param>
        /// <remarks>Equivalent to returning <see cref="TypeInfo.DeclaredNestedTypes"/> from this type and its <see cref="TypeInfo.BaseType"/>, recursively up the inheritance graph.</remarks>
        /// <returns>A collection of the nested types defined by the current type and any base types.</returns>
        public static IEnumerable<TypeInfo> GetAllNestedTypes(this TypeInfo typeInfo)
            => typeInfo.GetAll(ti => ti.DeclaredNestedTypes);

        /// <summary>
        /// Gets a collection of the properties defined by the current type and any base types.
        /// </summary>
        /// <param name="typeInfo">The type info.</param>
        /// <remarks>Equivalent to returning <see cref="TypeInfo.DeclaredProperties"/> from this type and its <see cref="TypeInfo.BaseType"/>, recursively up the inheritance graph.</remarks>
        /// <returns>A collection of the properties defined by the current type and any base types.</returns>
        public static IEnumerable<PropertyInfo> GetAllProperties(this TypeInfo typeInfo)
            => typeInfo.GetAll(ti => ti.DeclaredProperties);

        private static IEnumerable<T> GetAll<T>(this TypeInfo typeInfo, Func<TypeInfo, IEnumerable<T>> accessor)
        {
            while (typeInfo != null)
            {
                foreach (var t in accessor(typeInfo))
                {
                    yield return t;
                }

                typeInfo = typeInfo.BaseType?.GetTypeInfo();
            }
        }
    }
}
