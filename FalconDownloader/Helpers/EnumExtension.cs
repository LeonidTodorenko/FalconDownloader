using FalconDownloader.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;

namespace FalconDownloader
{
    public static class EnumExtension
    {
        public static string GetCustomDescription(object objEnum)
        {
            var fi = objEnum.GetType().GetField(objEnum.ToString());
            var attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);
            return (attributes.Length > 0) ? attributes[0].Description : objEnum.ToString();
        }

        public static Color GetCustomColor(object objEnum)
        {
            var fi = objEnum.GetType().GetField(objEnum.ToString());
            var attributes = (ColorAttribute[])fi.GetCustomAttributes(typeof(ColorAttribute), false);
            return (attributes.Length > 0) ? attributes[0].ForeColor : SystemColors.ControlText;
        }

        public static string Description(this Enum value)
        {
            return GetCustomDescription(value);
        }

        public static Color ForeColor(this Enum value)
        {
            return GetCustomColor(value);
        }
    }
}