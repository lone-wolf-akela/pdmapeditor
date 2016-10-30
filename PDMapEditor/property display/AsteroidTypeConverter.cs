﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;

namespace PDMapEditor
{
    class AsteroidTypeConverter : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            if (sourceType == typeof(string))
            {
                return true;
            }
            return base.CanConvertFrom(context, sourceType);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            if (value is string)
            {
                AsteroidType type = AsteroidType.GetTypeFromName((string)value);

                if (type != null)
                    return type;
                else
                    return AsteroidType.AsteroidTypes[0];
            }
            return base.ConvertFrom(context, culture, value);
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == typeof(string))
            {
                AsteroidType type = value as AsteroidType;
                if (type != null)
                    return type.Name;
            }
            return base.ConvertTo(context, culture, value, destinationType);
        }

        public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
        {
            return true;
        }

        public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            List<string> values = new List<string>();
            foreach (AsteroidType type in AsteroidType.AsteroidTypes)
                values.Add(type.Name);

            return new StandardValuesCollection(values);
        }
    }
}