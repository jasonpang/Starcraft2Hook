using System;
using System.Security.Cryptography;
using SharpDX;

namespace Game.Extensions.SharpDX
{
    public static class DataStreamExtensions
    {
        private static MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();

        public static byte[] ToArray(this DataStream stream)
        {
            var array = new byte[stream.Length];
            stream.Read(array, 0, array.Length);
            return array;
        }

        public static byte[] GetMD5HashArray(this DataStream stream)
        {
            return md5.ComputeHash(stream);
        }

        public static String GetMD5HashString(this DataStream stream)
        {
            return Convert.ToBase64String(md5.ComputeHash(stream));
        }
    }
}
