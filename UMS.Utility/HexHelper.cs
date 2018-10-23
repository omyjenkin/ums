using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UMS.Utility
{
    public static class HexHelper
    {
        /// <summary>
        /// 字符串转16进制字节数组
        /// </summary>
        /// <param name="hexString"></param>
        /// <returns></returns>
        public static byte[] strToHexByte(string hexString)
        {
            hexString = hexString.Replace(" ", "");
            if ((hexString.Length % 2) != 0)
                hexString += " ";
            byte[] returnBytes = new byte[hexString.Length / 2];
            for (int i = 0; i < returnBytes.Length; i++)
                returnBytes[i] = Convert.ToByte(hexString.Substring(i * 2, 2), 16);
            return returnBytes;
        }

        public static int[] hexStr2ByteArray(String hexString)
        {
            //if (String.IsNullOrEmpty(hexString))
            //    throw new ArgumentException("this hexString must not be empty");

            //hexString = hexString.ToLower();
            //byte[] byteArray = new byte[hexString.Length / 2];
            //int k = 0;
            //for (int i = 0; i < byteArray.Length; i++)
            //{
            //    //因为是16进制，最多只会占用4位，转换成字节需要两个16进制的字符，高位在先  
            //    //将hex 转换成byte   "&" 操作为了防止负数的自动扩展  
            //    // hex转换成byte 其实只占用了4位，然后把高位进行右移四位  
            //    // 然后“|”操作  低四位 就能得到 两个 16进制数转换成一个byte.  
            //    //  
            //    byte high = (byte)(Convert.ToByte(hexString.ElementAt(k).ToString(), 16) & 0xff);
            //    byte low = (byte)(Convert.ToByte(hexString.ElementAt(k + 1).ToString(), 16) & 0xff);
            //    byteArray[i] = (byte)(high << 4 | low);
            //    k += 2;
            //}
            //return byteArray;

            hexString = hexString.Replace(" ", "");
            if ((hexString.Length % 2) != 0)
                hexString += " ";
            int[] returnBytes = new int[hexString.Length / 4];
            for (int i = 0; i < returnBytes.Length; i++)
                returnBytes[i] = Convert.ToInt32(getHexUpend(hexString.Substring(i * 4, 4)), 16);
            return returnBytes;
        }

        /// <summary>
        /// 字符串转16进制字节数组
        /// </summary>
        /// <param name="hexString"></param>
        /// <returns></returns>
        public static UInt32[] strToHexInt32(string hexString)
        {
            hexString = hexString.Replace(" ", "");
            if ((hexString.Length % 8) != 0)
                hexString += " ";
            UInt32[] returnBytes = new UInt32[hexString.Length / 8];
            for (int i = 0; i < returnBytes.Length; i++)
                returnBytes[i] = Convert.ToUInt32(getHexUpend(hexString.Substring(i * 8, 8)),16);
            return returnBytes;
        }

        ///<summary>
        /// 字节数组转16进制字符串
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static string uint32ToHexStr(UInt32[] bytes, int size)
        {
            string returnStr = "";
            if (bytes != null)
            {
                if (bytes.Length < size || size <= 0)
                    size = bytes.Length;
                for (int i = 0; i < size; i++)
                {
                    returnStr += bytes[i].ToString("X2");
                }
            }
            return returnStr;
        }


        ///<summary>
        /// 字节数组转16进制字符串
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static string byteToHexStr(byte[] bytes, int size)
        {
            string returnStr = "";
            if (bytes != null)
            {
                if (bytes.Length < size || size <= 0)
                    size = bytes.Length;
                for (int i = 0; i < size; i++)
                {
                    returnStr += bytes[i].ToString("X2");
                }
            }
            return returnStr;
        }



        /*************************************************************************************************************/

        /// <summary>
        /// 从汉字转换到16进制
        /// </summary>
        /// <param name="s"></param>
        /// <param name="charset">编码,如"utf-8","gb2312"</param>
        /// <param name="fenge">是否每字符用逗号分隔</param>, bool fenge
        /// <returns></returns>
        public static string ToHex(string s, string charset)
        {
            if ((s.Length % 2) != 0)
            {
                s += " ";//空格
                //throw new ArgumentException("s is not valid chinese string!");
            }
            System.Text.Encoding chs = System.Text.Encoding.GetEncoding(charset);
            byte[] bytes = chs.GetBytes(s);
            string str = "";
            string strTran = "";
            for (int i = 0; i < bytes.Length; i++)
            {
                strTran = string.Format("{0:X}", bytes[i]);
                if (strTran.Length == 1)
                {
                    strTran = "0" + strTran;
                }
                str += strTran;
                //str += string.Format("{0:X}", bytes[i]);
                //if (fenge && (i != bytes.Length - 1))
                //{
                //    str += string.Format("{0}", ",");
                //}
            }
            return str.ToLower();

        }



        ///
        ///<summary>
        /// 从16进制转换成汉字
        /// </summary>
        /// <param name="hex"></param>
        /// <param name="charset">编码,如"utf-8","gb2312"</param>
        /// <returns></returns>
        public static string UnHex(string hex, string charset)
        {
            if (hex == null)
                throw new ArgumentNullException("hex");
            hex = hex.Replace(",", "");
            hex = hex.Replace("\n", "");
            hex = hex.Replace("\\", "");
            hex = hex.Replace(" ", "");
            if (hex.Length % 2 != 0)
            {
                hex += "20";//空格
            }
            // 需要将 hex 转换成 byte 数组。 
            byte[] bytes = new byte[hex.Length / 2];

            for (int i = 0; i < bytes.Length; i++)
            {
                try
                {
                    // 每两个字符是一个 byte。 
                    bytes[i] = byte.Parse(hex.Substring(i * 2, 2),
                    System.Globalization.NumberStyles.HexNumber);
                }
                catch
                {
                    // Rethrow an exception with custom message. 
                    throw new ArgumentException("hex is not a valid hex number!", "hex");
                }
            }
            System.Text.Encoding chs = System.Text.Encoding.GetEncoding(charset);

            return chs.GetString(bytes);
        }



        /*************************************************************************************************************/


        /// <summary>
        /// 按规约计算新的CRC校验码(累加校验，取32BITS)
        /// </summary>
        /// <param name="strUCRC"></param>
        /// <returns></returns>
        public static string formatCRC(string strUCRC)
        {
            UInt32[] coders = HexHelper.strToHexInt32(strUCRC);
            int nUp = coders.GetUpperBound(0);
            UInt32 n = 0;
            for (int i = 0; i < coders.Length; i++)
            {
                n += coders[i];
            } 
            return getHexUpend(n.ToString("X").PadLeft(8,'0'));
        }


        /// <summary>
        /// 检验是否符合规约CRC校验
        /// </summary>
        /// <param name="strCRC"></param>
        /// <returns></returns>
        //public static bool checkCRC(string strCRC)
        //{
        //    byte[] coders = HexHelper.strToHexByte(strCRC);
        //    byte crc = 0x00;
        //    for (int i = 0; i < coders.Length - 1; i++)
        //    {
        //        crc += coders[i];
        //    }
        //    if (crc != coders[coders.Length - 1])
        //        return false;
        //    else
        //        return true;
        //}



        /*************************************************************************************************************/



        /// <summary>
        /// 将16进制字符串 高低位互转
        /// </summary>
        public static string getHexUpend(string strIn)
        {
            if (strIn.Length == 4)
            {
                strIn = strIn.Substring(2, 2) + strIn.Substring(0, 2);
            }
            else if (strIn.Length == 6)
            {
                strIn = strIn.Substring(4, 2) + strIn.Substring(2, 2) + strIn.Substring(0, 2);
            }
            else if (strIn.Length == 8)
            {
                strIn = strIn.Substring(6, 2) + strIn.Substring(4, 2) + strIn.Substring(2, 2) + strIn.Substring(0, 2);
            }

            return strIn;
        }


        /// <summary>
        /// 将地位在前高位在后的16进制字符串转化为10进制值
        /// </summary>
        /// <param name="nValue">待转换16进制值</param>
        /// <returns></returns>
        public static int formatTerminalToInt(string strIn)
        {
            if (strIn.Length == 4)
            {
                strIn = strIn.Substring(2, 2) + strIn.Substring(0, 2);
            }
            else if (strIn.Length == 6)
            {
                strIn = strIn.Substring(4, 2) + strIn.Substring(2, 2) + strIn.Substring(0, 2);
            }
            else if (strIn.Length == 8)
            {
                strIn = strIn.Substring(6, 2) + strIn.Substring(4, 2) + strIn.Substring(2, 2) + strIn.Substring(0, 2);
            }

            return int.Parse(strIn, System.Globalization.NumberStyles.HexNumber);
        }


        /// <summary>
        /// 将10进制值转化为地位在前高位在后的16进制字符串
        /// </summary>
        /// <param name="nValue">待转换10进制值</param>
        /// <param name="nLength">规定转换后16进制字符串的长度</param>
        /// <returns></returns>
        public static string formatTerminalToHex(int nValue, int nLength)
        {
            string strTerminal = System.Convert.ToString(nValue, 16).PadLeft(nLength, '0');
            if (strTerminal.Length == 4)
            {
                strTerminal = strTerminal.Substring(2, 2) + strTerminal.Substring(0, 2);
            }
            if (strTerminal.Length == 6)
            {
                strTerminal = strTerminal.Substring(4, 2) + strTerminal.Substring(2, 2) + strTerminal.Substring(0, 2);
            }
            if (strTerminal.Length == 8)
            {
                strTerminal = strTerminal.Substring(6, 2) + strTerminal.Substring(4, 2) + strTerminal.Substring(2, 2) + strTerminal.Substring(0, 2);
            }
            return strTerminal;
        }


        /// <summary>
        /// 将时间字符串转换为16进制字符串
        /// </summary>
        public static string formatTimeToHex(string strTime)
        {
            DateTime dt = DateTime.Parse(strTime);
            string strSecond = System.Convert.ToString(dt.Second, 16).PadLeft(2, '0');
            string strMinute = System.Convert.ToString(dt.Minute, 16).PadLeft(2, '0');
            string strHour = System.Convert.ToString(dt.Hour, 16).PadLeft(2, '0');
            string strDay = System.Convert.ToString(dt.Day, 16).PadLeft(2, '0');
            string strMonth = System.Convert.ToString(dt.Month, 16).PadLeft(2, '0');
            string strYear = System.Convert.ToString(dt.Year, 16).PadLeft(4, '0');
            strYear = strYear.Substring(2, 2) + strYear.Substring(0, 2);
            string strFormat = strYear + strMonth + strDay + strHour + strMinute + strSecond;
            return strFormat;
        }


        /// <summary>
        /// 将16进制字符串转换为时间字符串
        /// </summary>
        public static string formatHexToTime(string strTime)
        {
            string strYear = HexHelper.formatTerminalToInt(strTime.Substring(0, 4)).ToString();
            string strMonth = HexHelper.formatTerminalToInt(strTime.Substring(4, 2)).ToString().PadLeft(2, '0');
            string strDay = HexHelper.formatTerminalToInt(strTime.Substring(6, 2)).ToString().PadLeft(2, '0');
            string strHour = HexHelper.formatTerminalToInt(strTime.Substring(8, 2)).ToString().PadLeft(2, '0');
            string strMinute = HexHelper.formatTerminalToInt(strTime.Substring(10, 2)).ToString().PadLeft(2, '0');
            string strSecond = HexHelper.formatTerminalToInt(strTime.Substring(12, 2)).ToString().PadLeft(2, '0');

            string strFormat = strYear + "-" + strMonth + "-" + strDay + " " + strHour + ":" + strMinute + ":" + strSecond;
            return strFormat;
        }

        /// <summary>
        /// 将16进制字符串转换为时间字符串
        /// </summary>
        public static string formatHexToShortYearTime(string strTime)
        {
            string strYear = HexHelper.formatTerminalToInt(strTime.Substring(0, 2)).ToString();
            string strMonth = HexHelper.formatTerminalToInt(strTime.Substring(2, 2)).ToString().PadLeft(2, '0');
            string strDay = HexHelper.formatTerminalToInt(strTime.Substring(4, 2)).ToString().PadLeft(2, '0');
            string strHour = HexHelper.formatTerminalToInt(strTime.Substring(6, 2)).ToString().PadLeft(2, '0');
            string strMinute = HexHelper.formatTerminalToInt(strTime.Substring(8, 2)).ToString().PadLeft(2, '0');
            string strSecond = HexHelper.formatTerminalToInt(strTime.Substring(10, 2)).ToString().PadLeft(2, '0');

            string strFormat = strYear + "-" + strMonth + "-" + strDay + " " + strHour + ":" + strMinute + ":" + strSecond;
            return strFormat;
        }

        /// <summary>
        /// 将IP字符串转换为16进制字符串
        /// </summary>
        public static string formatIpToHex(string strIP)
        {
            string strFormat = "";
            string[] steEme;
            steEme = strIP.Split('.');
            for (int i = 0; i < steEme.Length; i++)
            {
                strFormat += System.Convert.ToString(long.Parse(steEme[i].ToString()), 16).PadLeft(2, '0');
            }
            return strFormat;
        }


        /// <summary>
        /// 将16进制字符串转换为IP字符串
        /// </summary>
        public static string formatHexToIp(string strIP)
        {
            string strFormat = "";

            strFormat = int.Parse(strIP.Substring(0, 2), System.Globalization.NumberStyles.HexNumber).ToString() + ".";
            strFormat += int.Parse(strIP.Substring(2, 2), System.Globalization.NumberStyles.HexNumber).ToString() + ".";
            strFormat += int.Parse(strIP.Substring(4, 2), System.Globalization.NumberStyles.HexNumber).ToString() + ".";
            strFormat += int.Parse(strIP.Substring(6, 2), System.Globalization.NumberStyles.HexNumber).ToString();

            return strFormat;
        }


        /// <summary>
        /// 将2进制字符串转换为16进制字符串
        /// </summary>
        public static string formatBitToHex(string strBit)
        {
            string strFormat = "";
            int nBit = Convert.ToInt32(strBit, 2);
            strFormat = System.Convert.ToString(long.Parse(nBit.ToString()), 16).PadLeft(4, '0');
            strFormat = getHexUpend(strFormat);
            return strFormat;
        }

        /// <summary>
        /// 将16进制字符串转换为2进制字符串
        /// </summary>
        public static string formatHexToBit(string strHex)
        {
            string strFormat = "";

            strHex = getHexUpend(strHex);
            long lgHex = int.Parse(strHex, System.Globalization.NumberStyles.HexNumber);
            strFormat = System.Convert.ToString(lgHex, 2).PadLeft(16, '0');

            return strFormat;
        }

        /// <summary>
        /// 判断是否是日期格式
        /// </summary>
        /// <param name="strDate"></param>
        /// <returns></returns>
        public static bool isDateTime(string strDate)
        {
            //Regex regDateTime = new Regex();
            try
            {
                DateTime dt;
                if (DateTime.TryParse(strDate, out dt) == false)
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
            return true;
        }
        
    }
}
