using UnityEngine;

[CreateAssetMenu(fileName = "DialogueLine", menuName = "Dialogue/Line")]
public class DialogueLine : ScriptableObject
{
    public string speakerName;
    [TextArea] public string text;
}
