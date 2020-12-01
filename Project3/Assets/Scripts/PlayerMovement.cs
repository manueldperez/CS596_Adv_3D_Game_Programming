using Photon.Pun;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public PhotonView PV;

    public float speed = 20f;

    void Start()
    {
        PV = GetComponent<PhotonView>();
    }

    void Update()
    {
        if (PV.IsMine)
        {
            Vector3 pos = transform.position;

            if (Input.GetKey("d") || Input.GetKey(KeyCode.UpArrow))
            {
                pos.z += speed * Time.deltaTime;
            }

            if (Input.GetKey("a") || Input.GetKey(KeyCode.DownArrow))
            {
                pos.z -= speed * Time.deltaTime;
            }

            if (Input.GetKey("s") || Input.GetKey(KeyCode.RightArrow))
            {
                pos.x += speed * Time.deltaTime;
            }

            if (Input.GetKey("w") || Input.GetKey(KeyCode.LeftArrow))
            {
                pos.x -= speed * Time.deltaTime;
            }

            if (Input.GetKey(KeyCode.Space))
            {
                pos.y += speed * Time.deltaTime;
            }

            if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
            {
                pos.y -= speed * Time.deltaTime;
            }

            transform.position = pos;
        }

    }
}