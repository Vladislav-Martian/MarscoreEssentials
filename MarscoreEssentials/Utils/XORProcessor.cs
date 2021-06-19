namespace MarscoreEssentials.Utils
{
    public static class XORProcessor
    {
        /// <summary>
        /// Encrypt or decrypt by any yute[] key on place.
        /// </summary>
        /// <param name="source">Original byte array.</param>
        /// <param name="key">Key byte array.</param>
        /// <returns></returns>
        public static byte[] ProcessInPlace(in byte[] source, in byte[] key)
        {
            int keyl = key.Length;
            for (int i = 0; i < source.Length; i++)
            {
                source[i] = (byte)(source[i] ^ key[i % keyl]);
            }
            return source;
        }
        /// <summary>
        /// Encrypt or decrypt by any byte[] key. Creates new array of bytes.
        /// </summary>
        /// <param name="source">Original byte array.</param>
        /// <param name="key">Key byte array.</param>
        /// <returns></returns>
        public static byte[] ProcessOutPlace(in byte[] source, in byte[] key)
        {
            int keyl = key.Length;
            byte[] result = new byte[source.Length];
            for (int i = 0; i < source.Length; i++)
            {
                result[i] = (byte)(source[i] ^ key[i % keyl]);
            }
            return result;
        }

        /// <summary>
        /// Universal method to process bytes, selecting variant by "bool onPlace" argument. Default - InPlace (true).
        /// </summary>
        /// <param name="source">Original byte array.</param>
        /// <param name="key">Key byte array.</param>
        /// <param name="onPlace">true - InPlace, false - OutPlace.</param>
        /// <returns></returns>
        public static byte[] Process(in byte[] source, in byte[] key, in bool onPlace = true)
        {
            return onPlace ? ProcessInPlace(source, key) : ProcessOutPlace(source, key);
        }
    }
}