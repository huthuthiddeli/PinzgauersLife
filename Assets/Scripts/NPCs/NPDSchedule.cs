using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ScheduleEntry
{
    public string label; // e.g., "Sleeping"
    public Vector3 location;
    public float startHour;
    public float endHour;
}

[CreateAssetMenu(fileName = "Schedule", menuName = "NPC/Schedule")]
public class NPCSchedule : ScriptableObject
{
    public List<ScheduleEntry> entries;
}
