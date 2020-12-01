using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class PlayerMovement : MonoBehaviour
{
    public string user;

    public PhotonView PV;

    public GameObject player;
    public Camera myCamera;
    public  Vector3 offset;


    void Start()
    {
        PV = GetComponent<PhotonView>();
        // Start following the target if wanted.
        if (PV.IsMine)
        {
            PV.RPC("RPC_AddUsername", RpcTarget.AllBuffered, DB_Manager.db.username);
        }
        else
        {
            Destroy(myCamera);
           
        }
    }

    [PunRPC]
    void RPC_AddUsername(string username)
	{
        user = username;
	}

    void LateUpdate()
    {
        // The transform target may not destroy on level load,
        // so we need to cover corner cases where the Main Camera is different everytime we load a new scene, and reconnect when that happens
        // only follow is explicitly declared

    }
    public float speed = 20f;
   

    //controller if player decides to move. 
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

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Finish")
		{
            Debug.Log("Winner!");
            Debug.Log(user);
            DB_Manager.db.updateDB(true, user);
            SceneManager.LoadScene(1);
        }
	}
}

