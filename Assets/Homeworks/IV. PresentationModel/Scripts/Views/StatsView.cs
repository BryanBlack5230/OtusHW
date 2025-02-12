using Lessons.Architecture.PM;
using TMPro;
using UnityEngine;

public class StatsView : MonoBehaviour, IHeroPart
{
    [SerializeField] private TextMeshProUGUI[] _stats;

    public void Initialized(StatsPresenter statsPresenter)
    {
        UpdateStats(statsPresenter.Stats);
    }

    public void UpdateStats(CharacterStat[] stats)
    {
        if (_stats == null) return;

        for (int i = 0; i < _stats.Length; i++)
        {
            if (stats.Length <= i)
            {
                UpdateStat(i);
            }
            else
            {
                var stat = stats[i];
                UpdateStat(i, stat.Name, stat.Value.ToString());
            }
        }
    }

    private void UpdateStat(int index, string statName = "", string value = "")
    {
        _stats[index].text = $"{statName}: {value}";
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }
}