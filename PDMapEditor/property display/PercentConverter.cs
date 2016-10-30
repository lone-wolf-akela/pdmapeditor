namespace PDMapEditor
{
    using System;
    using System.ComponentModel;
    using System.Globalization;

    class PercentConverter : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return sourceType == typeof(string);
        }
        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            if (value is string)
            {
                float amount = 0;
                string stringValue = (string)value;
                stringValue = stringValue.Replace("%", "");

                float.TryParse(stringValue, out amount);
                return amount;
            }
            return base.ConvertFrom(context, culture, value);
        }
        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == typeof(string) && value is float)
            {
                return value.ToString() + "%";
            }
            return base.ConvertTo(context, culture, value, destinationType);
        }
    }
}