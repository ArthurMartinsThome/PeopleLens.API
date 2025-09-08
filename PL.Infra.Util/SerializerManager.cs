using Newtonsoft.Json;

namespace PL.Infra.Util
{
    public class SerializerManager<T> where T : class
    {
        public T Deserialize(string obj)
        {
            var deserializedObject = JsonConvert.DeserializeObject<T>(obj);

            return deserializedObject;
        }

        public string Serialize(object obj)
        {
            var serializedObject = JsonConvert.SerializeObject(obj);
            return serializedObject;
        }
    }
}