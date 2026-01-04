using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Blackboard
{
    private Dictionary<string, object> _data = new Dictionary<string, object>();

    public void SetValue<T>(string key, T value)
    {
        _data[key] = value;
    }

    public T GetValue<T>(string key)
    {
        if (_data.TryGetValue(key, out object value))
            return (T)value;

        return default;
    }

    public bool HasKey(string key) => _data.ContainsKey(key);

    public void Remove(string key)
    {
        if (_data.ContainsKey(key))
            _data.Remove(key);
    }

    public void Clear() => _data.Clear();
}
