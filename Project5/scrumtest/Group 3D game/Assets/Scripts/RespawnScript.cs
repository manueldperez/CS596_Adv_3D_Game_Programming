using UnityEngine;

public class RespawnScript : MonoBehaviour
{
    public Transform player;
    public Transform respawnPoint;
    public GameObject effect;
    public GameObject boom;


    void OnTriggerEnter(Collider x)
    {
        Instantiate(effect, player.transform.position, Quaternion.identity);
        Instantiate(boom, player.transform.position, Quaternion.identity);
        player.GetComponent<Rigidbody>().velocity = Vector3.zero;
        player.transform.position = respawnPoint.transform.position;
    }
}
