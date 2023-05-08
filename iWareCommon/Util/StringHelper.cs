using System.Text;

namespace iWareCommon.Util
{
    public  class StringHelper
  {
      private static readonly string charStr = "0123456789";
 
      /// <summary> 
      /// 格式化字符串，将物料中的单引号格式化为两个单引号
      /// </summary>
      /// <param name="str"></param>
      /// <returns></returns>
        
      public static string RelpaceQuot(string str)  
      {
          //将单引号格式化为两个单引号
          return string.IsNullOrEmpty(str) ?  "" : str.Replace("'","''");
      }

      /// <summary>
      /// 将HTML的标签代码转为正常字符串
      /// </summary>
      /// <param name="str"></param>
      /// <returns></returns>
      public static string RelpaceHtml(string str)
      {

          //将&QUOT;;格式化为"
          str = string.IsNullOrEmpty(str) ? "" : str.Replace("&QUOT;", "\"");
          

          str = string.IsNullOrEmpty(str) ? "" : str.Replace("\"","'");
          //将&AMP;格式化为&
          str = str.Replace("&AMP;", "&");
          //将&LT;格式化为<
          str = str.Replace("&LT;", "<");
          //将&GT;格式化为>
          str = str.Replace("&GT;", ">");

          //将&AMP;格式化为&
          str = string.IsNullOrEmpty(str) ? "" : str.Replace("&NBSP;", " ");

          //将&AMP;格式化为&
          str = string.IsNullOrEmpty(str) ? "" : str.Replace("&ENSP;", " ");

          //将&AMP;格式化为&
          str = string.IsNullOrEmpty(str) ? "" : str.Replace("&EMSP;", " ");



          return str;
      }




      /// <summary>
      /// 将型如yyyyMMdd的日期类型转为yyyy-MM-dd的日期类型
      /// </summary>
      /// <param name="str"></param>
      /// <returns></returns>
        
      public static string ConvertDateTimeStr(string str)
      {
          if(string.IsNullOrEmpty(str) || str.Length != 8)
          {
              return str;
          }
          return str.Substring(0,4) + "-" + str.Substring(4,2) + "-" + str.Substring(6,2);
      }

      /// <summary>
      /// 获取下一个字符串
      /// </summary>
      /// <param name="str"></param>
      /// <returns></returns>
      public static string NextStr(string str)
      {
          var charArray = charStr.ToCharArray();
          var len = str.Length;
          if (str == "".PadLeft(len, charArray[charArray.Length-1])) { return "".PadLeft(len + 1, charArray[0]); }
          var strArray = str.ToCharArray();
          for (var i = 0; i < strArray.Length; i++)
          {
              var pos = charStr.IndexOf(strArray[i]);
          
              if (pos < charArray.Length - 1) 
              {
                  strArray[i] = charArray[pos + 1];
                  break;
              }

              strArray[i] = charArray[0];
          }    
          return new string(strArray);
      }

      /// <summary>
      /// 获取第一个字符串
      /// </summary>
      /// <returns></returns>
      public static string GetFirstStr()
      {
          var charArray = charStr.ToCharArray();
          return "".PadLeft(6, charArray[0]);
      }

      /// <summary>
      /// 压缩字符串的算法
      /// </summary>
      /// <param name="str"></param>
      /// <returns></returns>
      public static string Compress(string str)
      {
          var res = "";
          var lastChar = "\0";
          var frequency = 0;
          var currentChar = string.Empty;
          for (int i = 0; i < str.Length; i++)
          {
              if (str[i].ToString() != lastChar)
              {
                  if (!string.IsNullOrEmpty(currentChar)) { res += (frequency - 1 > 0 ? (frequency - 1).ToString() : string.Empty) + currentChar; }
                  currentChar = str[i].ToString();
                  lastChar = currentChar;
                  frequency = 1;
              }
              else { frequency += 1; }
          }
          if (!string.IsNullOrEmpty(currentChar)) { res += (frequency - 1 > 0 ? (frequency - 1).ToString() : string.Empty) + currentChar; }
          return res;
      }

      /// <summary>
      /// 解压字符串的算法
      /// </summary>
      /// <param name="str"></param>
      /// <returns></returns>
      public static string DeCompress(string str)
      {
          var nChars = "0123456789";
          var res = "";
          var nbStr = "0";
          for (int i = 0; i < str.Length; i++)
          {
              if (nChars.IndexOf(str[i]) > -1) { nbStr += str[i]; }
              else
              {
                  var nb = Convert.ToInt32(nbStr) + 1;
                  res += "".PadLeft(nb, str[i]);
                  nbStr = "0";
              }
          }
          return res;
      }

        /// <summary>
        /// 分割字符
        /// </summary>
        /// <returns></returns>
        public static string SplitChar()
        {
            return new ASCIIEncoding().GetString(new byte[] { 3 });
        }



    }
}