using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace Helper.Configuration
{
    public class IniFile
    {
        [DllImport("kernel32")]
        private static extern bool WritePrivateProfileString(
            string lpAppName, string lpKeyName, string lpString, string lpFilename);

        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(
            string lpAppName, string lpKeyName, string lpDefault, StringBuilder lpszReturnedString, int nSize,
            string lpFilename);

        [DllImport("kernel32")]
        private static extern int GetPrivateProfileSection(
            string lpAppName, byte[] lpszReturnedString, int nSize, string lpFilename);

        /// <summary>
        /// Path of the INI file.
        /// </summary>
        public string Path { get; private set; }

        /// <summary>
        /// Instantiate an INI file object with the path of the INI file.
        /// </summary>
        /// <param name="path">Path of the INI file.</param>
        public IniFile(string path)
        {
            this.Path = path;
        }

        /// <summary>
        /// Set the value of a key.
        /// </summary>
        /// <param name="section">Section name.</param>
        /// <param name="key">Parameter key.</param>
        /// <param name="value">Value to be set.</param>
        public void WriteValue(string section, string key, string value)
        {
            WritePrivateProfileString(section, key, value, Path);
        }

        /// <summary>
        /// Read the value of a key.
        /// </summary>
        /// <param name="section">Section name.</param>
        /// <param name="key">Parameter key.</param>
        /// <param name="defaultValue">Default value to be returned if the key does not exist.</param>
        /// <returns>The value if the key exists; otherwise, the default value.</returns>
        public string ReadValue(string section, string key, string defaultValue = null)
        {
            StringBuilder value = new StringBuilder(255);
            GetPrivateProfileString(section, key, defaultValue, value, 255, Path);
            return value.ToString();
        }

        /// <summary>
        /// Read all parameter keys and values defined under a given section in the INI file.
        /// </summary>
        /// <param name="section">Section name.</param>
        /// <returns>A set of parameter keys and values.</returns>
        public IDictionary<string, string> ReadAllKeysAndValues(string section)
        {
            byte[] buffer = new byte[2048];
            GetPrivateProfileSection(section, buffer, 2048, Path);
            string[] parameters = Encoding.ASCII.GetString(buffer)
                .Trim('\0')
                .Split(new[] { '\0' }, StringSplitOptions.RemoveEmptyEntries);
            var paramToReturn = parameters.Select(p => new { Str = p, EqualSignIndex = p.IndexOf('=') });
            return paramToReturn
                .Where(p => p.EqualSignIndex >= 0)
                .Select(p =>
                    new
                    {
                        Key = p.Str.Substring(0, p.EqualSignIndex),
                        Value = p.Str.Substring(p.EqualSignIndex + 1)
                    })
                .ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
        }
    }
}
