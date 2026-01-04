using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class Stat
{
  public float BaseValue;
  private List<float> modifiers = new List<float>();

  public float Value
  {
    get
    {
      float total = BaseValue;
      foreach (var mod in modifiers)
        total += mod;
      return total;
    }
  }

  public void AddModifier(float value) => modifiers.Add(value);
  public void RemoveModifier(float value) => modifiers.Remove(value);
}

[System.Serializable]
public class StatSystem
{
  public Dictionary<string, Stat> stats = new Dictionary<string, Stat>();

  public void AddStat(string name, float baseValue)
  {
    if (!stats.ContainsKey(name))
      stats[name] = new Stat { BaseValue = baseValue };
  }

  public float GetValue(string name)
  {
    if (stats.TryGetValue(name, out var stat))
      return stat.Value;

    Debug.LogWarning($"Stat {name} não encontrado.");
    return 0f;
  }

  public void ModifyStat(string name, float value)
  {
    if (stats.TryGetValue(name, out var stat))
      stat.AddModifier(value);
  }
}
