using UnityEngine;

public class Leberkassemmal : MonoBehaviour, IFood, IConsumable, IBaseItem
{
    public string Name => "Leberkassemal";
    public string Description => "A delicious Bavarian meatloaf sandwich.";
    public string IconPath => "Icons/Leberkassemmal";
    public uint ItemSlots => 1;
    public bool IsStackable => false;
    public float HungerValue => 50.0f;
    public float ThirstValue => 0f;
    public float AlcoholValue => 0f;

    public void Consume(GameObject obj)
    {
        Player player = obj.GetComponent<Player>();
        if (player != null)
        {
            player.hungriness += HungerValue;
            Debug.Log("Apple eaten via raycast.");
            Destroy(gameObject);
        }
    }
}
