using Newtonsoft.Json;
using System.Collections;
using PL.Infra.Util.Model;

namespace PL.Infra.Util
{
    public static class ObjectCompare
    {
        public static bool EqualObjects<T>(T oldObj, T newObj) where T : class
        {
            try
            {
                return JsonConvert.SerializeObject(oldObj) == JsonConvert.SerializeObject(newObj);
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        //public static IEnumerable<ObjectDiference> MapDiferences<T>(T oldObject, T newObject)
        //{
        //    var differences = new List<ObjectDiference>();
        //    var properties = typeof(T).GetProperties();
        //    foreach (var prop in properties)
        //    {
        //        var oldValue = prop.GetValue(oldObject);
        //        var newValue = prop.GetValue(newObject);
        //        if (oldValue == null && newValue == null)
        //            continue;
        //        if (oldValue == null || !oldValue.Equals(newValue))
        //            differences.Add(new ObjectDiference
        //            {
        //                Property = prop,
        //                OldValue = oldValue,
        //                NewValue = newValue
        //            });
        //    }
        //    return differences;
        //}

        public static List<ObjectDifference> CompareObjects<T>(T oldObj, T newObj, string path = "", bool showId = true)
        {
            var diff = new List<ObjectDifference>();
            if (oldObj == null || newObj == null)
            {
                if (!EqualityComparer<T>.Default.Equals(oldObj, newObj))
                    diff.Add(new ObjectDifference { Path = path, OldValue = oldObj, NewValue = newObj });
                return diff;
            }
            var type = oldObj.GetType();
            if (string.IsNullOrEmpty(path))
                path = type.Name;
            var properies = type.GetProperties();
            var objectIdProp = properies.Where(x => x.Name.ToLower() == "id").FirstOrDefault();
            var oldId = default(object);
            if (objectIdProp != null)
                oldId = objectIdProp.GetValue(oldObj, null);
            if (type.IsValueType || typeof(string).IsAssignableFrom(type) || typeof(DateTime).IsAssignableFrom(type))
            {
                if (!oldObj.Equals(newObj))
                    diff.Add(new ObjectDifference { ObjectId = oldId, Path = path, OldValue = oldObj, NewValue = newObj });
                return diff;
            }
            if (typeof(IEnumerable).IsAssignableFrom(type))
            {
                var oldEnumerable = ((IEnumerable)oldObj).Cast<object>().ToList();
                var newEnumerable = ((IEnumerable)newObj).Cast<object>().ToList();
                foreach (var oldItem in oldEnumerable)
                {
                    var itemProperties = oldItem.GetType().GetProperties();
                    var idProperty = itemProperties.Where(x => x.Name.ToLower() == "id").FirstOrDefault();
                    if (idProperty != null)
                    {
                        var oldItemId = idProperty.GetValue(oldItem, null);
                        var newItem = newEnumerable.FirstOrDefault(x => idProperty.GetValue(x, null).Equals(oldItemId) == true);

                        if (newItem == null)
                            diff.Add(new ObjectDifference { ObjectId = oldItemId, Path = $"{path}[{oldEnumerable.IndexOf(oldItem)}]", OldValue = oldItem, NewValue = null });
                        else
                            diff.AddRange(CompareObjects(oldItem, newItem, $"{path}[{oldEnumerable.IndexOf(oldItem)}]"));
                    }
                    else
                    {
                        if (newEnumerable.Count() <= oldEnumerable.IndexOf(oldItem))
                            diff.AddRange(CompareObjects(oldItem, null, $"{path}[{oldEnumerable.IndexOf(oldItem)}]"));
                        else
                        {
                            var newItem = newEnumerable[oldEnumerable.IndexOf(oldItem)];
                            diff.AddRange(CompareObjects(oldItem, newItem, $"{path}[{oldEnumerable.IndexOf(oldItem)}]"));
                        }
                    }
                }
                foreach (var newItem in newEnumerable)
                {
                    var itemProperties = newItem.GetType().GetProperties();
                    var idProperty = itemProperties.Where(x => x.Name.ToLower() == "id").FirstOrDefault();
                    if (idProperty != null)
                    {
                        var newId = idProperty.GetValue(newItem, null);
                        var oldItem = oldEnumerable.FirstOrDefault(x => idProperty.GetValue(x, null).Equals(newId) == true);

                        if (oldItem == null)
                            diff.Add(new ObjectDifference { ObjectId = newId, Path = $"{path}[{newEnumerable.IndexOf(newItem)}]", OldValue = null, NewValue = newItem });
                    }
                    else
                    {
                        if (oldEnumerable.Count() <= newEnumerable.IndexOf(newItem))
                            diff.AddRange(CompareObjects(null, newItem, $"{path}[{newEnumerable.IndexOf(newItem)}]"));
                    }
                }
                return diff;
            }

            var properties = type.GetProperties();
            foreach (var property in properties)
            {
                var oldProp = property.GetValue(oldObj);
                var newProp = property.GetValue(newObj);

                if (oldProp == null || newProp == null)
                {
                    if (oldProp != newProp)
                        diff.Add(new ObjectDifference { ObjectId = oldId, Path = $"{path}.{property.Name}", OldValue = oldProp, NewValue = newProp });
                }
                else if (oldProp.GetType().IsValueType || oldProp is string || oldProp is Enum)
                {
                    if (!oldProp.Equals(newProp))
                        diff.Add(new ObjectDifference { ObjectId = oldId, Path = $"{path}.{property.Name}", OldValue = oldProp, NewValue = newProp });
                }
                else
                    diff.AddRange(CompareObjects(oldProp, newProp, $"{path}.{property.Name}"));
            }

            return diff;
        }
    }
}