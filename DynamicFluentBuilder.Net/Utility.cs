using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DynamicFluentBuilder.Net
{
    class Utility
    {
        public static List<string> GetParameterNames(ParameterInfo[] parameters)
        {
            return parameters
                .Select(parameter => parameter.Name)
                .ToList();
        }

        #region String Manipulation
        /// <summary>
        /// converts FieldName to _fieldName
        /// </summary>
        /// <param name="publicName"></param>
        /// <returns></returns>
        public static string PublicNameToPrivate(string publicName)
        {

            var privateName = "_" + PublicNameToVariableName(publicName);

            return privateName;
        }

        /// <summary>
        /// converts to camelCase from PascalCase
        /// </summary>
        /// <param name="publicName"></param>
        /// <returns></returns>
        public static string PublicNameToVariableName(string publicName)
        {
            var result = char.ToLower(
                publicName[0]) + publicName.Substring(1);

            return result;
        }

        /// <summary>
        /// converts PascalCase to camelCase
        /// </summary>
        /// <param name="variableName"></param>
        /// <returns></returns>
        public static string VariableNameToPublicName(string variableName)
        {
            var result = char.ToUpper(
                variableName[0]) + variableName.Substring(1);

            return result;
        }

        public static string RemoveWith(string methodName)
        {
            return methodName.Replace("With", "");
        }

        #endregion


        /// <summary>
        /// for use when a constructor parameter doesn't have
        /// a user-defined default value
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public static object DefaultValueForType(Type t)
        {
            if (t == typeof(string))
            {
                return null;
            }

            return Activator.CreateInstance(t);
        }
    }
}
