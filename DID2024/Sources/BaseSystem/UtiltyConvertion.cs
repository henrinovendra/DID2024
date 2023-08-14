using System;
using System.Collections.Generic;
using System.Text;

namespace DID
{
    
        public class Convertion
    {

        #region Boolean
        public static byte BoolToByte(bool BooleanValue)
        {
            if (BooleanValue) return 1;
            else return 0;
        }
        public static string BoolToStringNumber(bool BooleanValue)
        {
            if (BooleanValue) return "1";
            else return "0";
        }
        public static string ToBoolToStringNumber(string StringValue)
        {
            bool mBoBoole = false;
            string x = string.Empty;
            if (StringValue == null || StringValue.Length == 0) return "0";
            foreach (char c in StringValue.Trim())
            {
                if (Char.IsLetter(c)) x += c;
            }
            if (x.ToUpper() == "1") x = "true";
            else if (x.ToUpper() == "TRUE") x = "true";
            else if (x.ToUpper() == "ON") x = "true";
            else if (x.ToUpper() == "OK") x = "true";
            else if (x.ToUpper() == "0") x = "false";
            else if (x.ToUpper() == "NO") x = "false";
            else if (x.ToUpper() == "FALSE") x = "false";
            else if (x.ToUpper() == "CHECKED") x = "true";
            else if (x.ToUpper().IndexOf("UN") != -1) x = "false";
            else if (x.ToUpper().IndexOf("OF") != -1) x = "false";
            else if (x.ToUpper().IndexOf("FA") != -1) x = "false";
            else if (x.ToUpper().IndexOf("TR") != -1) x = "true";
            else if (x.ToUpper().IndexOf("RU") != -1) x = "true";
            try
            { mBoBoole = bool.Parse(x); }
            catch { mBoBoole = false; }
            if (mBoBoole) return "1";
            else return "0";
        }

        public static bool ToBool(string StringValue)
        {
            bool mBoBoole = false;
            string x = string.Empty;
            if (StringValue == null || StringValue.Length == 0) return false;
            foreach (char c in StringValue.Trim())
            {
                if (Char.IsLetter(c)) x += c;
            }
            if (x.ToUpper() == "1") x = "true";
            else if (x.ToUpper() == "TRUE") x = "true";
            else if (x.ToUpper() == "ON") x = "true";
            else if (x.ToUpper() == "OK") x = "true";
            else if (x.ToUpper() == "0") x = "false";
            else if (x.ToUpper() == "NO") x = "false";
            else if (x.ToUpper() == "FALSE") x = "false";
            else if (x.ToUpper() == "CHECKED") x = "true";
            else if (x.ToUpper().IndexOf("UN") != -1) x = "false";
            else if (x.ToUpper().IndexOf("OF") != -1) x = "false";
            else if (x.ToUpper().IndexOf("FA") != -1) x = "false";
            else if (x.ToUpper().IndexOf("TR") != -1) x = "true";
            else if (x.ToUpper().IndexOf("RU") != -1) x = "true";
            try
            { mBoBoole = bool.Parse(x); }
            catch { mBoBoole = false; }
            return mBoBoole;
        }
        public static bool ToBoolean(string StringValue)
        {
            return ToBool(StringValue);
        }
        public static bool ToBool(byte ByteValue)
        {
            if (ByteValue == 0)
                return false;
            else
                return true;
        }
        public static bool ToBoolean(byte ByteValue)
        {
            return ToBool(ByteValue);
        }
        #endregion

        #region Numeric
        public static byte ToByte(string StringValue, bool IsWithSymbol)
        {
            if (StringValue == null) return 0;
            byte mByBytes = 0;
            if (StringValue == null || StringValue.Length == 0) return 0;
            try { mByBytes = byte.Parse(ToValidateStringNumeric(StringValue, false, IsWithSymbol)); }
            catch { mByBytes = byte.MaxValue; }
            return mByBytes;
        }
        public static byte ToByte(string StringValue)
        {
            return ToByte(StringValue, false);
        }
        public static short ToShort(string StringValue, bool IsWithSymbol)
        {
            if (StringValue == null) return 0;
            short mShShort = 0;
            if (StringValue == null || StringValue.Length == 0) return 0;
            try { mShShort = short.Parse(ToValidateStringNumeric(StringValue, false, IsWithSymbol)); }
            catch { mShShort = short.MaxValue; }
            return mShShort;
        }
        public static short ToShort(string StringValue)
        {
            return ToShort(StringValue, false);
        }
        public static short ToShort(object StringObject)
        {
            if (StringObject == null)
                return ToShort("0", false);
            else
                return ToShort(StringObject.ToString(), false);
        }

        public static int ToInteger(string StringValue, bool IsWithSymbol)
        {
            if (StringValue == null) return 0;
            int mInInteg = 0;
            if (StringValue == null || StringValue.Length == 0) return 0;
            try { mInInteg = int.Parse(ToValidateStringNumeric(StringValue, false, IsWithSymbol)); }
            catch { mInInteg = int.MaxValue; }
            return mInInteg;
        }
        public static int ToInteger(string StringValue)
        {
            return ToInteger(StringValue, false);
        }
        public static int ToInteger(object StringObject)
        {
            if (StringObject == null)
                return ToInteger("0", false);
            else
                return ToInteger(StringObject.ToString(), false);
        }
        public static System.DateTime yyyy_MM_dd_HH_mm_ss2Date(string StringTanggal)
        {
            if ((StringTanggal == null) || (StringTanggal.Length < 19)) StringTanggal = DID.CoreProcessing.DefaultDateMinimum.ToString("yyyy-MM-dd HH:mm:ss");
            int vYear = int.Parse(StringTanggal.Substring(0, 4));
            int vMonth = int.Parse(StringTanggal.Substring(5, 2));
            int vDay = int.Parse(StringTanggal.Substring(8, 2));
            int vHour = int.Parse(StringTanggal.Substring(11, 2));
            int vMenit = int.Parse(StringTanggal.Substring(14, 2));
            int vSec = int.Parse(StringTanggal.Substring(17, 2));
            System.DateTime vDate = new System.DateTime(vYear, vMonth, vDay, vHour, vMenit, vSec);
            return vDate;
        }
        public static long ToLong(string StringValue)
        {
            if (StringValue == null) return 0;
            long mLgValue = 0;
            if (StringValue == null || StringValue.Length == 0) return 0;
            try { mLgValue = long.Parse(ToValidateStringNumeric(StringValue, false, false)); }
            catch { mLgValue = long.MaxValue; }
            return mLgValue;
        }

        public static string ToValidateStringNumeric(string StringNumeric, bool IsWithDecimal, bool IsWithSymbol)
        {
            string x = string.Empty;
            string vStringValue = StringNumeric.Trim().ToUpper().Replace(",", "").Replace("'", "").Replace(@"""", "");
            bool vNumericExist = false;
            if (vStringValue.Trim() == string.Empty) x = "0";
            else
            {
                if (vStringValue.IndexOf('(') >= 0)
                {
                    vStringValue = vStringValue.Replace("(", "").Replace(")", "");
                    vStringValue = "-" + vStringValue;
                }
                else if (vStringValue.IndexOf("CR") >= 0)
                {
                    vStringValue = vStringValue.Replace("CR", "");
                    vStringValue = "-" + vStringValue;
                }
                foreach (char c in vStringValue)
                {
                    if (Char.IsNumber(c))
                    {
                        x += c;
                        vNumericExist = true;
                    }
                    else if (c == '-') x += c;
                    else
                    {
                        if (IsWithDecimal)
                        {
                            if (c == '.') x += c;
                        }
                        else
                        {
                            if (c == '.') break;
                        }
                        if (IsWithSymbol)
                        {
                            if (c == 'A' || c == 'B' || c == 'C' || c == 'D' || c == 'E' || c == 'F' || c == 'X') x += c;
                        }
                    }
                }
                if (x.Trim() == string.Empty) x = "0";
                else if (!vNumericExist) x = "0";
            }
            return x;
        }

        public static decimal ToSmallMoney(string StringValue, bool IsWithSymbol)
        {
            if (StringValue == null) return 0;
            decimal mDcMoney = 0;
            if (StringValue == null || StringValue.Length == 0) return 0;
            try { mDcMoney = decimal.Parse(ToValidateStringNumeric(StringValue, true, IsWithSymbol)); }
            catch { mDcMoney = 0; }
            if (mDcMoney > 214748) mDcMoney = 214748;
            if (mDcMoney < -214748) mDcMoney = -214748;
            return mDcMoney;
        }
        public static decimal ToSmallMoney(string StringValue)
        {
            if (StringValue == null)
                return ToSmallMoney("0", false);
            else
                return ToSmallMoney(StringValue, false);
        }
        public static decimal ToSmallMoney(object StringObject)
        {
            if (StringObject == null)
                return ToSmallMoney("0", false);
            else
                return ToSmallMoney(StringObject.ToString(), false);
        }

        public static decimal ToDecimal(string StringValue)
        {
            if (StringValue == null) return 0;
            decimal mDcMoney = 0;
            if (StringValue == null || StringValue.Length == 0) return 0;
            try { mDcMoney = decimal.Parse(ToValidateStringNumeric(StringValue, true, true)); }
            catch { mDcMoney = 0; }
            return mDcMoney;
        }

        #endregion

        #region String And Date
        public static string StringBool(bool iValue)
        {
            if (iValue) return "T";
            return "F";
        }
        public static string StringArrayToString(string[] StringArray, string StringJoin = "|")
        {
            return string.Join(StringJoin, StringArray);
        }

        public static string StringSQL2JS(string StringValue)
        {
            if (string.IsNullOrWhiteSpace(StringValue)) StringValue = string.Empty;
            return System.Text.RegularExpressions.Regex.Replace(StringValue.Replace("\"", "'").Replace("\\", "/"), @"\r\n?|\n", "\\n");
        }

        /*public static System.DateTime yyyy_MM_dd_HH_mm_ss2Date(string StringTanggal)
        {
            if ((StringTanggal == null) || (StringTanggal.Length < 19)) StringTanggal = DVMS.CoreProcessing.DefaultDateMinimum.ToString("yyyy-MM-dd HH:mm:ss");
            int vYear = int.Parse(StringTanggal.Substring(0, 4));
            int vMonth = int.Parse(StringTanggal.Substring(5, 2));
            int vDay = int.Parse(StringTanggal.Substring(8, 2));
            int vHour = int.Parse(StringTanggal.Substring(11, 2));
            int vMenit = int.Parse(StringTanggal.Substring(14, 2));
            int vSec = int.Parse(StringTanggal.Substring(17, 2));
            System.DateTime vDate = new System.DateTime(vYear, vMonth, vDay, vHour, vMenit, vSec);
            return vDate;
        }*/
        public static string yyyy_MM_dd_HH_mm_ss(DateTime Tanggal)
        {
            return Tanggal.ToString("yyyy-MM-dd HH:mm:ss");
        }
       /* public static System.DateTime yyyy_MM_dd2Date(string StringTanggal)
        {
            if ((StringTanggal == null) || (StringTanggal.Length < 10)) StringTanggal = DVMS.CoreProcessing.DefaultDateMinimum.ToString("yyyy-MM-dd");
            int vYear = int.Parse(StringTanggal.Substring(0, 4));
            int vMonth = int.Parse(StringTanggal.Substring(5, 2));
            int vDay = int.Parse(StringTanggal.Substring(8, 2));
            System.DateTime vDate = new System.DateTime(vYear, vMonth, vDay);
            return vDate;
        }*/
        public static string yyyy_MM_dd(DateTime Tanggal)
        {
            return Tanggal.ToString("yyyy-MM-dd");
        }

        public static string ShowTimeMinuteSecond(int Totalseconds)
        {
            return string.Format("{0}:{1:d2}", Totalseconds / 60, Totalseconds % 60);
        }
        public static string ShowTimeHourMinuteSecond(int Totalseconds)
        {
            TimeSpan time = TimeSpan.FromSeconds(Totalseconds);
            return time.ToString(@"hh\:mm\:ss"); ;
        }

        
        
        public static string[] StringArrayLoad(string StringData)
        {
            return StringData.Split('#');
        }
        public static string StringArraySave(string[] StringData)
        {
            string vTmp = string.Empty;
            for (int i = 0; i < StringData.Length; i++)
            {
                vTmp += StringData[i].ToLower();
                if (i < (StringData.Length - 1)) vTmp += "#";
            }
            return vTmp;
        }
        public static string StringFromList(List<string> iListString, string iSpace = " ")
        {
            return string.Join(iSpace, iListString.ToArray());
        }
        public static string StringWithSlashRNtoNewLine(string iString)
        {
            return iString.Trim().Replace("\r\n", System.Environment.NewLine).Replace("\n\r", System.Environment.NewLine).Replace("\n", System.Environment.NewLine).Replace("\r", System.Environment.NewLine);
        }
        #endregion

        #region Hashes
        public static string MD5(String iString)
        {
            byte[] vStream = Encoding.UTF8.GetBytes(iString);
            try
            {
                System.Security.Cryptography.MD5 md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
                byte[] hash = md5.ComputeHash(vStream);
                StringBuilder sb = new StringBuilder();
                foreach (byte a in hash)
                {
                    if (a < 16)
                        sb.Append("0" + a.ToString("x"));
                    else
                        sb.Append(a.ToString("x"));
                }
                return sb.ToString();
            }
            catch {
                return iString;
            }
            //catch (Exception ex)
            //{
            //    System.Diagnostics.Debug.WriteLine(ex.Message, "Hashes");
            //    throw;
            //}
        }
        #endregion

    }

}