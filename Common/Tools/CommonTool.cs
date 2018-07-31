using System;
using System.IO;
using ProtoBuf;

namespace Common.Tools
{
    /// <summary>
    /// 工具类
    /// </summary>
    public class CommonTool
    {
        /// <summary>
        /// protobuf 序列化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static byte[] Serialize<T>(T obj)
        {
            byte[] bytes = null;
            using (var ms = new MemoryStream())
            {
                Serializer.Serialize<T>(ms, obj);
                bytes = new byte[ms.Position];
                var fullBytes = ms.GetBuffer();
                Array.Copy(fullBytes, bytes, bytes.Length);
            }
            return bytes;
        }
        /// <summary>
        /// protobuf 反序列化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static T Deserialize<T>(byte[] bytes)
        {
            using (var stream = new MemoryStream(bytes))
            {
                return ProtoBuf.Serializer.Deserialize<T>(stream);
            }
        }
    }
}