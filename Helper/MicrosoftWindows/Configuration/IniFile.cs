// TODO: Move all classes under Helper.MicrosoftWindows from Helper.dll to a new assembly, in order to make Helper.dll cross-platform.
using Helper.AOP;
using Helper.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace Helper.MicrosoftWindows.Configuration
{
    public class IniFile : IIniFile
    {
        private const int SingleValueSize = 255;
        private const int SectionBufferSize = 2048;

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
            ParameterGuard.NullOrEmptyStringCheck(path, nameof(path), "Path of ini file must not be null or empty.");

            if (!File.Exists(path))
                throw new FileNotFoundException("Ini file does not exist.", path);
                
            Path = path;
        }

        /// <summary>
        /// Set the value of a key.
        /// </summary>
        /// <param name="section">Section name.</param>
        /// <param name="key">Parameter key.</param>
        /// <param name="value">Value to be set.</param>
        public void WriteValue<T>(string section, string key, T value) where T : IConvertible
        {
            ParameterGuard.NullOrEmptyStringCheck(section, nameof(section));
            ParameterGuard.NullOrEmptyStringCheck(key, nameof(key));

            WritePrivateProfileString(section, key, Convert.ToString(value), Path);
        }

        /// <summary>
        /// Read the value of a key.
        /// </summary>
        /// <param name="section">Section name.</param>
        /// <param name="key">Parameter key.</param>
        /// <param name="defaultValue">Default value to be returned if the key does not exist.</param>
        /// <returns>The value if the key exists; otherwise, the default value.</returns>
        public T ReadValue<T>(string section, string key, T defaultValue = default(T)) where T : IConvertible
        {
            ParameterGuard.NullOrEmptyStringCheck(section, nameof(section));
            ParameterGuard.NullOrEmptyStringCheck(key, nameof(key));

            StringBuilder value = new StringBuilder(SingleValueSize);
            GetPrivateProfileString(section, key, Convert.ToString(defaultValue), value, 255, Path);
            return (T)Convert.ChangeType(value, typeof(T));
        }

        /// <summary>
        /// Read all parameter keys and values defined under a given section in the INI file.
        /// </summary>
        /// <param name="section">Section name.</param>
        /// <returns>A set of parameter keys and values.</returns>
        public IDictionary<string, T> ReadAllKeysAndValues<T>(string section) where T : IConvertible
        {
            ParameterGuard.NullOrEmptyStringCheck(section, nameof(section));

            byte[] buffer = new byte[SectionBufferSize];
            GetPrivateProfileSection(section, buffer, SectionBufferSize, Path);
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
                        Value = (T)Convert.ChangeType(p.Str.Substring(p.EqualSignIndex + 1), typeof(T))
                    })
                .ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
        }
    }
}
