using UnityEngine;

public class LimitCamera : MonoBehaviour
{
    public GameObject player;

    private void LateUpdate()
    {
        this.transform.position = new Vector3(player.transform.position.x, 40, player.transform.position.z);

        // Dont Rotate Camera when player is rotating
        this.transform.rotation = Quaternion.Euler(90, 0, 0);
    }
}
