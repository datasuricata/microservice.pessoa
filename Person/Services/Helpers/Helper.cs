using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;

namespace Person.Services.Helpers {
    public static class Helper {

        #region - enums -

        public static string EnumDisplay(this Enum value) {
            return !(value.GetType().GetField(value.ToString()).GetCustomAttributes(typeof(DisplayAttribute), false)
                .SingleOrDefault() is DisplayAttribute attribute) ? value.ToString() : attribute.Description;
        }

        #endregion

        #region - generics -

        public static T TrimSpaces<T>(this T obj) {
            if (obj == null)
                return obj;

            var properties = obj.GetType()
                .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                .Where(prop => prop.PropertyType == typeof(string))
                .Where(prop => prop.CanWrite && prop.CanRead);

            foreach (var property in properties) {
                var value = (string)property.GetValue(obj, null);
                if (!string.IsNullOrEmpty(value)) {
                    var newValue = (object)value.Trim();
                    property.SetValue(obj, newValue, null);
                }
            }

            var customTypes =
                obj.GetType()
                    .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                    .Where(
                        prop =>
                            !prop.GetType().IsPrimitive && prop.GetType().IsClass &&
                            !prop.PropertyType.FullName.StartsWith("System"));

            foreach (var customType in customTypes) {
                customType.GetValue(obj).TrimSpaces();
            }

            return obj;
        }

        public static T InjectAccount<T>(this T obj, string id, string nameOf) {
            foreach (var x in obj.GetType().GetProperties()) {
                if (x.Name == nameOf)
                    if (x.GetValue(obj) == null)
                        x.SetValue(obj, id);
            }
            return obj;
        }

        #endregion

        #region - cleaner -

        public static string CleanFormat(this string value) {
            if (string.IsNullOrEmpty(value))
                return value;

            return value.Replace(".", string.Empty).Replace("-", string.Empty).Replace("/", string.Empty);
        }

        #endregion

        #region - security -

        public static string EncryptToMD5(this string value) {
            if (string.IsNullOrEmpty(value))
                return "";

            var builder = new StringBuilder();
            foreach (var x in MD5.Create().ComputeHash(Encoding.Default.GetBytes(value)))
                builder.Append(x.ToString("x2"));
            return builder.ToString().ToUpper();
        }

        public static string EncryptToSHA1(this string value) {
            if (string.IsNullOrEmpty(value))
                return "";

            var builder = new StringBuilder();
            foreach (var b in new SHA1Managed().ComputeHash(Encoding.ASCII.GetBytes(value)))
                builder.Append(b.ToString("x2"));
            return builder.ToString().ToUpper();
        }

        #endregion
    }
}
