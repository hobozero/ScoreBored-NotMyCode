using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text.RegularExpressions;

/// <summary>
/// Extensions on the string DAtaType
/// </summary>
public static class StringExtensions
{
    /// <summary>
    /// Not a-z or space
    /// </summary>
    static Regex regexURLChars = new Regex(@"[^a-záéíúó?!\s]", RegexOptions.Compiled | RegexOptions.CultureInvariant | RegexOptions.IgnoreCase);
    /// <summary>
    /// Creates a storable Name from the provided string
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static string MakeName(this String str)
    {
        //remove weird characters and double spaces
        //also remove ¿ since it is used as a ? placeholder
        return regexURLChars.Replace(str, " ").Replace("  ", " ").Replace("¿", string.Empty);
    }


    /// <summary>
    /// Encodes a string as an "_" enabled string
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static string URLEncodeName(this String str)
    {
        return System.Web.HttpUtility.UrlEncode(str.Replace(' ', '_').Replace('?', '¿'));
    }

    public static string URLDecodeToName(this String str)
    {
        return System.Web.HttpUtility.UrlDecode(str.Replace('_', ' ').Replace('¿', '?'));
    }
}

