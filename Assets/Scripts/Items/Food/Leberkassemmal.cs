using UnityEngine;

public class Leberkassemmal : MonoBehaviour, IBaseItem, IConsumable
{
    public string Name => "Leberkassemal";
    public string Description => "A delicious Bavarian meatloaf sandwich.";
    public string IconPath => "Icons/Leberkassemmal";
    public uint ItemSlots => 1;
    public bool IsStackable => false;

 
    public void Consume(GameObject obj)
    {
        Player player = obj.GetComponent<Player>();
        if (player != null)
        {
            player.hungriness += 50f;
            Debug.Log("Apple eaten via raycast.");
            Destroy(gameObject);
        }
    }
}
