using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace EasyAIO
{
    /// <summary>
    /// 序列化到文件
    /// </summary>
   internal static class BinarySerializer
    {
        /// <summary>
        /// 序列化到文件
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="o">对象</param>
        /// <param name="filePath">文件路径</param>
        public static void Serialize<T>(T o, string filePath)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.Write, FileShare.None);
            try
            {
                formatter.Serialize(stream, o);
            }
            catch (Exception e) { Console.WriteLine(e.Message); }
            finally
            {
                stream.Flush();
                stream.Close();
                stream.Dispose();
            }
        }
        /// <summary>
        /// 反序列化 
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="filePath">文件路径</param>
        /// <returns>对象</returns>
        public static T DeSerialize<T>(string filePath)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            Stream destream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
            try
            {
                T o = (T)formatter.Deserialize(destream);
                return o;
            }
            catch (Exception e) { Console.WriteLine(e.Message); }
            finally
            {
                destream.Flush();
                destream.Close();
                destream.Dispose();
            }
            return default(T);
        }
    }
}
