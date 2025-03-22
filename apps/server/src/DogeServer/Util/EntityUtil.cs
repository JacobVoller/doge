using System.Reflection;

namespace DogeServer.Util
{
    public static class EntityUtil
    {
        public static T? Combine<T>(T? target, T? source)
        {
            if (target == null && source == null)
                return default;

            if (target == null)
                return source;

            if (source == null)
                return target;

            var type = target.GetType();
            var properties = type.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            foreach (var property in properties)
            {
                var sourceValue = property.GetValue(source);
                if (sourceValue == null)
                    continue;

                var targetValue = property.GetValue(target);
                if (targetValue == sourceValue)
                    continue;

                var canCombine = property.CanRead
                    && property.CanWrite
                    && (property.GetGetMethod(true)?.IsPublic ?? false)
                    && (property.GetSetMethod(true)?.IsPublic ?? false);

                if (!canCombine)
                    continue;

                var copy = AllowPropertyOverride(targetValue);
                if (!copy)
                    continue;

                property.SetValue(target, sourceValue);
            }

            return target;
        }

        public static void Zip<TA, TB>(TA target, TB source)
        {
            if (target == null || source == null) return;

            var targetType = typeof(TA);
            var sourceType = typeof(TB);

            foreach (var targetProp in targetType.GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                if (!targetProp.CanWrite || !targetProp.CanRead) continue;

                var sourceProp = sourceType.GetProperty(targetProp.Name);
                if (sourceProp == null || !sourceProp.CanRead) continue;

                // Check if the property types match
                if (targetProp.PropertyType != sourceProp.PropertyType) continue;

                var targetValue = targetProp.GetValue(target);
                var sourceValue = sourceProp.GetValue(source);

                if (targetValue == null && sourceValue != null)
                {
                    targetProp.SetValue(target, sourceValue);
                }
            }
        }

        private static bool AllowPropertyOverride(object? value)
        {
            if (value == null)
                return true;

            var type = value.GetType();
            if (type == typeof(string))
                return true;

            if (type == typeof(int))
                return (int)value != 0;

            if (Nullable.GetUnderlyingType(type) == null)
                return true;

            if (!type.IsValueType)
                return true;

            var defaultValue = Activator.CreateInstance(type);
            return !value.Equals(defaultValue);
        }



    }
}
