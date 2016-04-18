using System.Text;

namespace MSP.Core.Utils.Extensions
{
    public static class StringExtend
    {
        public static string RemoveLastChar(this StringBuilder s)
        {
            Ex.EnsureNotNull("s",s);
            Ex.Ensure(s.Length > 0);
            return s.ToString(0, s.Length - 1);
        }
        public static string RemoveFirstChar(this StringBuilder s)
        {
            Ex.EnsureNotNull("s", s);
            Ex.Ensure(s.Length > 0);
            return s.ToString(1, s.Length);
        }
    }
}
