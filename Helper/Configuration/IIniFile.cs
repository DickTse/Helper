using System;
using System.Collections.Generic;

namespace Helper.Configuration
{
    public interface IIniFile
    {
        string Path { get; }
        void WriteValue<T>(string section, string key, T value) where T : IConvertible;
        T ReadValue<T>(string section, string key, T defaultValue = default(T)) where T : IConvertible;
        IDictionary<string, T> ReadAllKeysAndValues<T>(string section) where T : IConvertible;
    }
}