using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DynamicFluentBuilder.Net
{
    class HelpersForT<T>
    {
        #region Memoized Properties

        private ConstructorInfo[] _constructorInfo;
        public ConstructorInfo[] Constructors
        {
            get
            {
                if (_constructorInfo != null) return _constructorInfo;

                _constructorInfo = typeof (T).GetConstructors();

                return _constructorInfo;
            }
        }

        #region Field / Property Info

        /// <summary>
        /// Properties that we may want to set
        /// - these would be set after construction if a constructor
        ///   parameter doesn't exist with the same name
        /// </summary>
        private PropertyInfo[] _propertyInfos;
        public PropertyInfo[] PropertyInformation
        {
            get
            {
                if (_propertyInfos != null) return _propertyInfos;

                _propertyInfos = typeof(T).GetProperties();

                return _propertyInfos;
            }
        }

        /// <summary>
        /// Fields that we may want to set
        /// - these would be set after construction if a constructor
        ///   parameter doesn't exist with the same name
        /// </summary>
        protected FieldInfo[] FieldInfos;
        public FieldInfo[] FieldInformation
        {
            get
            {
                if (FieldInfos != null) return FieldInfos;

                FieldInfos = typeof(T).GetFields(
                    BindingFlags.Public |
                    BindingFlags.Instance |
                    BindingFlags.NonPublic);

                return FieldInfos;
            }
        }

        #endregion

        #endregion

        /// <summary>
        /// Because fields / properties / methods must all have a unique
        /// name, we can check each set of fields / properties to
        /// see if there is a match
        /// </summary>
        /// <param name="fromName"></param>
        /// <returns></returns>
        public dynamic GetFieldInfo(string fromName)
        {
            dynamic result = FieldInformation
                .FirstOrDefault(
                    field => field.Name == fromName
                 );

            if (result != null) return result;

            // if the fields don't have what we are looking for,
            // try the properties.
            result = PropertyInformation
                .FirstOrDefault(
                    field => field.Name == fromName
                );

            return result;
        }


        /// <summary>
        /// sets the value on the specified field for the specified object
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <returns>true if the field has been set with the value</returns>
        public bool SetValueOn(T instance, dynamic field, object value)
        {
            // if the field was not found, abort
            if (field == null) return false;

            // set the field to the value
            if (field is FieldInfo)
            {
                var fieldInfo = (FieldInfo)field;
                value = Convert.ChangeType(
                    value,
                    fieldInfo.FieldType);
                fieldInfo.SetValue(instance, value);
            }
            else if (field is PropertyInfo)
            {
                var propertyInfo = (PropertyInfo)field;
                value = Convert.ChangeType(
                    value,
                    propertyInfo.PropertyType);

                // TODO: since this data is pulled from T,
                // we can't set it. this stuff should be stored in a hash
                propertyInfo.SetValue(instance, value, null);
            }
            else
            {
                return false;
            }

            return true;
        }
    }
}
