using System;
using System.Reflection;
using System.Text;

namespace Logging.Extensions
{
    public static class ObjectExtensions
    {
        /// <summary>
        /// Pretty prints the value of the current object.
        /// </summary>
        /// <example>123456</example>
        public static string PrintValues(this object o)
        {
            if (o == null) 
                return "Null";

            var fields = o.GetType().GetFields(BindingFlags.Public | BindingFlags.Instance);

            var builder = new StringBuilder();

            builder.Append("[");
            foreach (var field in fields)
            {
                if (field.FieldType.IsArray)
                {
                    PrintValues(field.GetValue(o) as Array);
                }
                else
                {
                    builder.Append(String.Format("{0}, ", field.GetValue(o)));
                }
            }
            builder.Length = builder.Length - 2; // Remove ", "
            builder.Append("]");

            return builder.ToString();
        }

        /// <summary>
        /// Pretty prints the name and value of the current object.
        /// </summary>
        /// <example>devicePointer: 123456</example>
        public static string PrintNamesValues(this object o)
        {
            if (o == null)
                return "Null";

            var fields = o.GetType().GetFields(BindingFlags.Public | BindingFlags.Instance);

            var builder = new StringBuilder();

            builder.Append("[");
            foreach (var field in fields)
            {
                if (field.FieldType.IsArray)
                {
                    PrintNamesValues(field.GetValue(o) as Array);
                }
                else
                {
                    builder.Append(String.Format("{0}: {1}, ", field.Name, field.FieldType == typeof(String) ? "\"" + field.GetValue(o) + "\"" : field.GetValue(o)));
                }
            }
            builder.Length = builder.Length - 2; // Remove ", "
            builder.Append("]");

            return builder.ToString();
        }

        /// <summary>
        /// Pretty prints the type, name, and value of the current object.
        /// </summary>
        /// <example>IntPtr devicePointer: 123456</example>
        public static string PrintTypesNamesValues(this object o)
        {
            if (o == null)
                return "Null";

            var fields = o.GetType().GetFields(BindingFlags.Public | BindingFlags.Instance);

            var builder = new StringBuilder();

            builder.Append("[");
            foreach (var field in fields)
            {
                if (field.FieldType.IsArray)
                {
                    PrintTypesNamesValues(field.GetValue(o) as Array);
                }
                else
                {
                    builder.Append(String.Format("{0} {1}: {2}, ", field.FieldType.Name, field.Name, field.FieldType == typeof(String) ? "\"" + field.GetValue(o) + "\"" : field.GetValue(o)));
                }
            }
            builder.Length = builder.Length - 2; // Remove ", "
            builder.Append("]");

            return builder.ToString();
        }

        public static string PrintValues(this Array array)
        {
            if (array == null)
                return "Null";

            if (array.Length == 0)
                return "(Empty)";

            var builder = new StringBuilder();

            foreach (var element in array)
                builder.Append(PrintValues(element));

            return builder.ToString();
        }

        public static string PrintNamesValues(this Array array)
        {
            if (array == null)
                return "Null";

            if (array.Length == 0)
                return "(Empty)";

            var builder = new StringBuilder();

            foreach (var element in array)
                builder.Append(PrintNamesValues(element));

            return builder.ToString();
        }

        public static string PrintTypesNamesValues(this Array array)
        {
            if (array == null)
                return "Null";

            if (array.Length == 0)
                return "(Empty)";

            var builder = new StringBuilder();

            foreach (var element in array)
                builder.Append(PrintTypesNamesValues(element));

            return builder.ToString();
        }
    }
}
