using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Reflection;
using System.ComponentModel;
using Windows.Storage;
using System;

// https://github.com/joseangelmt/ObservableSettings

namespace XamlBrewer.Mvvm
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public sealed class DefaultSettingValueAttribute : Attribute
    {
        public DefaultSettingValueAttribute()
        {}

        public DefaultSettingValueAttribute(object value)
        {
            Value = value;
        }

        public object Value { get; set; }
    }

    public class ObservableSettings : INotifyPropertyChanged
    {
        private readonly ApplicationDataContainer settings;

        public ObservableSettings(ApplicationDataContainer settings)
        {
            this.settings = settings;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected bool Set<T>(T value, [CallerMemberName] string propertyName = null)
        {
            if (settings.Values.ContainsKey(propertyName))
            {
                var currentValue = (T)settings.Values[propertyName];
                if (EqualityComparer<T>.Default.Equals(currentValue, value))
                    return false;
            }

            settings.Values[propertyName] = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            return true;
        }

        protected T Get<T>([CallerMemberName] string propertyName = null)
        {
            if (settings.Values.ContainsKey(propertyName))
                return (T)settings.Values[propertyName];

            var attributes = GetType().GetTypeInfo().GetDeclaredProperty(propertyName).CustomAttributes.Where(ca => ca.AttributeType == typeof(DefaultSettingValueAttribute)).ToList();
            if (attributes.Count == 1)
                return (T)attributes[0].NamedArguments[0].TypedValue.Value;

            return default;
        }
    }
}