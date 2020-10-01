using System;
using System.Reflection;
using UnityEngine;

namespace DesignPatterns.MVC.General
{
    public abstract class GeneralPropertyView : View
    {
        [SerializeField] protected string m_propertyName = String.Empty;

        protected PropertyInfo GetProperty(object @object) => @object?.GetType().GetProperty(m_propertyName);

        protected bool IsPropertyType<PropertyType>(object @object)
        {
            if (@object == null) return false;
            PropertyInfo propertyInfo = GetProperty(@object);
            if (propertyInfo == null) return false;
            return propertyInfo.PropertyType == typeof(PropertyType);
        }
        protected PropertyInfo GetProperty() => GetProperty(Object);

        protected ValueType GetValue<ValueType>() => (ValueType) GetProperty().GetValue(Object);
        
        public override bool AcceptObject(object @object) => GetProperty(@object) != null;
    }
}