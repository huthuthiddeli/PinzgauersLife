using System.Xml.Linq;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class NPCController : MonoBehaviour
{
    public NPCData npcData;
    public NPCSchedule schedule;
    public DialogueLine[] dialogue;
    public Stac stateMachine;

    void Start()
    {
        stateMachine = new StateMachine();
        stateMachine.ChangeState(new IdleState(this)); // Initial state
    }

    void Update()
    {
        stateMachine.Update();
    }

    public void Interact()
    {
        stateMachine.ChangeState(new TalkState(this));
    }
}
