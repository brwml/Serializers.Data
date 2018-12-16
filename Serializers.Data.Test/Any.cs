using System;
using System.Security.Cryptography;

namespace Serializers.Data.Test
{
    public static class Any
    {
        private static RandomNumberGenerator Rng = RandomNumberGenerator.Create();

        public static char CharValue()
        {
            while (true)
            {
                var c = BitConverter.ToChar(GetBytes(sizeof(char)));

                if (char.IsLetterOrDigit(c) || char.IsPunctuation(c) || char.IsSymbol(c))
                {
                    return c;
                }
            }
        }

        public static byte[] GetBytes(int length)
        {
            var bytes = new byte[length];
            Rng.GetBytes(bytes);
            return bytes;
        }

        public static short Int16Value()
        {
            return BitConverter.ToInt16(GetBytes(sizeof(short)));
        }

        public static short Int16Value(short min, short max)
        {
            return (short)((PositiveInt16Value() % (max - min)) + min);
        }

        public static int Int32Value()
        {
            return BitConverter.ToInt32(GetBytes(sizeof(int)));
        }

        public static int Int32Value(int min, int max)
        {
            return (PositiveInt32Value() % (max - min)) + min;
        }

        public static short PositiveInt16Value()
        {
            return Math.Abs(Int16Value());
        }

        public static int PositiveInt32Value()
        {
            return Math.Abs(Int32Value());
        }

        public static string StringValue(int length)
        {
            var chars = new char[length];

            for (var i = 0; i < length; i++)
            {
                chars[i] = CharValue();
            }

            return new string(chars);
        }
    }
}