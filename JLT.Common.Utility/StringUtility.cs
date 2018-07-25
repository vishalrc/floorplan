using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace com.JLT.Common.Utility
{
    public static class StringUtility
    {
        public static List<string> SplitString(string InputString, string Delimiter)
        {
            return Regex.Split(InputString, Delimiter).ToList<string>();
        }

        public static string GenerateJsonResponse(string JsonBody, Enums.JsonMsgType MsgType, string Msg)
        {
            StringBuilder sb = new StringBuilder();
            sb.Clear();
            sb.Append("{");
            sb.Append(" \"MsgType\": \"");
            sb.Append(Enum.GetName(typeof(Enums.JsonMsgType), MsgType));
            sb.Append("\", \"Msg\": \"");
            sb.Append(Msg);
            sb.Append("\", \"JsonBody\": ");
            sb.Append(JsonBody);
            sb.Append("}");
            return sb.ToString();
        }
    }
}
