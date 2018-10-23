using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UMS.Utility
{
    public class SplitBytes
    {
        private byte[] receiveAllByte;
        private int _bufflength;

        public int BuffLength
        {
            get
            {
                return _bufflength;
            }
        }

        public SplitBytes()
        {
            receiveAllByte = null;
            _bufflength = 0;
        }

        public byte[] ReceiveAllByte
        {
            get
            {
                return receiveAllByte;
            }
        }

        public void Dispose()
        {
            receiveAllByte = null;
        }

        public void AddBytes(byte[] recByte, int count)
        {
            byte[] f;

            if (receiveAllByte != null)
            {
                _bufflength = receiveAllByte.Length + count;
                f = new byte[_bufflength];

                for (int i = 0; i < receiveAllByte.Length; i++)
                {
                    f[i] = receiveAllByte[i];
                }

                for (int i = 0; i < count; i++)
                {
                    f[i + receiveAllByte.Length] = recByte[i];
                }
            }
            else
            {
                _bufflength = count;
                f = new byte[_bufflength];
                for (int i = 0; i < count; i++)
                {
                    f[i] = recByte[i];
                }
            }

            receiveAllByte = f;
        }
    }
}
