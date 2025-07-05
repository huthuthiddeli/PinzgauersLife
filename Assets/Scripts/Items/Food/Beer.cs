using UnityEngine;

public class Beer : MonoBehaviour, IInteractable, IFood, IBaseItem
{
    public string Name => "Beer";
    public string Description => "A refreshing beer to quench your thirst.";
    public string IconPath => "Icons/Beer";
    public uint ItemSlots => 1;
    public bool IsStackable => false;
    public float HungerValue => 0.0f;
    public float ThirstValue => 30.0f;
    public float AlcoholValue => 15.0f;

    //public void Consume(GameObject obj)
    //{
    //    Player player = obj.GetComponent<Player>();
    //    if (player != null)
    //    {
    //        player.thirstiness += ThirstValue;
    //        player.alcoholLevel += AlcoholValue;
    //        Debug.Log($"{Name} eaten via raycast.");
    //        Destroy(gameObject);
    //    }
    //}

    public void Interact(GameObject obj)
    {
        Player player = obj.GetComponent<Player>();
        if (player != null)
        {
            player.thirstiness += ThirstValue;
            player.alcoholLevel += AlcoholValue;
            Debug.Log($"{Name} eaten via raycast.");
            Destroy(gameObject);
        }
    }
}
