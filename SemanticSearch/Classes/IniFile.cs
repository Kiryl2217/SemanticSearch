using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System.Runtime.InteropServices;
using System.Text;

namespace SemanticSearch.Classes
{
    public class IniFile
    {
        public string path;

        [DllImport("kernel32", CharSet = CharSet.Unicode)] static extern long WritePrivateProfileString(string Section, string Key, string Value, string FilePath);

        [DllImport("kernel32", CharSet = CharSet.Unicode)] static extern long GetPrivateProfileString(string Section, string Key, string Default, StringBuilder RetVal, int Size, string FilePath);

        public IniFile (string path)
            => this.path = path;

        public void Write(string section, string key, string value)
        {
            WritePrivateProfileString(section, key, value, path);
        }

        public string Read(string section, string key)
        {
            StringBuilder RetVal = new StringBuilder(255);
            GetPrivateProfileString(section, key, "", RetVal, 255, path);
            return RetVal.ToString();   
        }
    }
}
