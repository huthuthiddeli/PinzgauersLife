public abstract class NPCState
{
    protected NPCController npc;
    public NPCState(NPCController npc) { this.npc = npc; }

    public abstract void Enter();
    public abstract void Update();
    public abstract void Exit();
}

public class StateMachine
{

}

public class IdleState : NPCState
{
    public IdleState(NPCController npc) : base(npc) { }
    public override void Enter() { /* Stand idle */ }
    public override void Update() { /* Check for schedule changes */ }
    public override void Exit() { }
}
