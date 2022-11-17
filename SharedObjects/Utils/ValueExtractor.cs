using System.Reflection;
using System.Text.RegularExpressions;

namespace SharedObjects.Utils
{
    public static class ValueExtractor
    {
        public static void ExtractValueFromModelToEntity<TEntity, TModel>(TEntity entity, TModel model)
        {
            var modelProps = typeof(TModel).GetProperties();
            foreach (var modelProp in modelProps)
            {
                var entityProp = GetPropInEntityClass<TEntity>(modelProp);
                if (entityProp == null) continue;
                var entityPropType = entityProp.PropertyType;
                var modelPropType = modelProp.PropertyType;
                if (!AreBothPropsSameType(entityPropType, modelPropType)) continue;
                var value = modelProp.GetValue(model);
                if (DoesPropIgnoreDefaultValue(entityPropType, value)) continue;
                if (IsPropNullableInEntityClass(entityPropType))
                {
                    entityProp.SetValue(entity, value);
                    continue;
                };
                if (value == null) continue;
                entityProp.SetValue(entity, value);
            }
        }

        private static bool AreBothPropsSameType(Type entityPropType, Type modelPropType)
        {
            var srt1 = modelPropType.ToString();
            var srt2 = entityPropType.ToString();
            var result = Regex.IsMatch(modelPropType.ToString(), entityPropType.ToString());
            result |= (srt1 == srt2);
            return result;
        }

        private static bool IsPropNullableInEntityClass(Type entityPropType)
        {
            if (entityPropType == null) return false;
            return Regex.IsMatch(entityPropType.ToString(), @"Nullable");
        }

        private static PropertyInfo? GetPropInEntityClass<TEntity>(PropertyInfo modelProp)
        {
            var propName = modelProp.Name;
            var entityProp = typeof(TEntity).GetProperty(propName);
            return entityProp;
        }

        private static bool DoesPropIgnoreDefaultValue(Type propType, object? value)
        {
            if (value == null) return false;
            var isGuidType = Regex.IsMatch(propType.ToString(), @"System.Guid(])?$");
            var isDateTimeType = Regex.IsMatch(propType.ToString(), @"System.DateTime(])?$");
            var str = propType.ToString();
            var isDateTimeOffset = Regex.IsMatch(propType.ToString(), @"System.DateTimeOffset(])?$");

            if (isGuidType)
            {
                if ((Guid)value == default(Guid)) return true;
            }
            if (isDateTimeType)
            {
                if ((DateTime)value == default(DateTime)) return true;
            }
            if (isDateTimeOffset)
            {
                if ((DateTimeOffset)value == default(DateTimeOffset)) return true;
            }
            return false;
        }
    }
}