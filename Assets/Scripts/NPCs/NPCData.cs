using UnityEngine;

[CreateAssetMenu(fileName = "NPC", menuName = "NPC/NPC Data")]
public class NPCData : ScriptableObject
{
    public string npcName;
    public Sprite portrait;
    public string description;
    public Quest[] questsAvailable;
}
