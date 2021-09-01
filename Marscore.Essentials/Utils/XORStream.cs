using System;
using System.Collections.Generic;
using System.IO;

namespace Marscore.Essentials.Utils
{
    /// <summary>
    /// Stream container, that allows to encrypt-decrypt it data with xor method. 
    /// Inner stream receives already encoded data. 
    ///  Use using() statement to automatically dispose object, that contains your password.
    /// By default DateTime.Now bytes used as password, if object will be lost, password will be lost too.
    /// </summary>
    public sealed class XorStream: Stream
    {
        #region Props

        private Stream CoreStream { get; }
        private byte[] Password { get; set; }
        #endregion
        
        public XorStream( in Stream origin, byte[] password = null)
        {
            password ??= BitConverter.GetBytes(DateTime.Now.Ticks);
            CoreStream = origin;
            Password = password;
        }
        
        public override void Flush()
        {
            CoreStream.Flush();
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            var result = CoreStream.Read(buffer, offset, count);
            XORProcessor.ProcessInPlace(buffer, Password);
            return result;
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            return CoreStream.Seek(offset, origin);
        }

        public override void SetLength(long value)
        {
            CoreStream.SetLength(value);
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            CoreStream.Write(
                XORProcessor.ProcessInPlace(buffer, Password),
                offset,
                count);
        }

        public new void Dispose()
        {
            Password = BitConverter.GetBytes(0);
            CoreStream.Dispose();
        }

        public override bool CanRead => CoreStream.CanRead;
        public override bool CanSeek => CoreStream.CanSeek;
        public override bool CanWrite => CoreStream.CanWrite;
        public override long Length => CoreStream.Length;
        public override long Position
        {
            get => CoreStream.Position;
            set => CoreStream.Position = value;
        }
    }
}