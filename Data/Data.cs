using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace NetWork.Data
{
    [Serializable]
    public static class Packet 
    {
        public static byte[] Pack(Tuple<int, string[]> content) //пакування пакетів
        {
            var bf = new BinaryFormatter();
            var ms = new MemoryStream(); //викор. пам`ять у якості резервного сховища

            for (int i = 0; i < content.Item2.Length; i++)
                content.Item2[i] = Encode(content.Item2[i]);

            bf.Serialize(ms, content); 
            byte[] packet = ms.ToArray();
            ms.Close(); 

            return packet;
        }

        public static Tuple<int, string[]> UnPack(byte[] packet) //розпакування пакетів
        {
            var bf = new BinaryFormatter(); 
            var ms = new MemoryStream(packet);

            var content = (Tuple<int, string[]>)bf.Deserialize(ms);
            ms.Close(); 

            for (int i = 0; i < content.Item2.Length; i++)
                content.Item2[i] = Decode(content.Item2[i]);

            return content;
        }

        //Кодування даних
        public static string Encode(string data)
        {
            if (data != null)
            {
                var encodeBase64 = System.Text.Encoding.UTF8.GetBytes(data);
                return Convert.ToBase64String(encodeBase64);
            }
            return null;
        }

        //Декодування
        public static string Decode(string data)
        {
            if (data != null)
            {
                var decodeBase64 = System.Convert.FromBase64String(data);
                return System.Text.Encoding.UTF8.GetString(decodeBase64);
            }
            return null;
        }

    }
} 
