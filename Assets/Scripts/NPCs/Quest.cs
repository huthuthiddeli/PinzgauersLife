using UnityEngine;

[CreateAssetMenu(fileName = "Quest", menuName = "Quests/Quest")]
public class Quest : ScriptableObject
{
    public string title;
    [TextArea] public string description;
    public bool isCompleted;
    public bool isActive;
}
