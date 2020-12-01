using UnityEngine;
using System.Collections;

public class CameraControllers : MonoBehaviour
{
    public GameObject player;

    public Vector3 offset;

    void Start()
    {
    
        
    }

    void LateUpdate()
    {
        transform.position = player.transform.position + offset;
    }
}