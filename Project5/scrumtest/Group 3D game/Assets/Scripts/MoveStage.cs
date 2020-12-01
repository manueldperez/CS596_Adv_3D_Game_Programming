using UnityEngine;
using System.Collections;

public class MoveStage : MonoBehaviour
{
    public Vector3 currentRot;

    void Update()
    {
        currentRot = GetComponent<Transform>().eulerAngles;
        if ((Input.GetAxis("Horizontal") > .2 )&& (currentRot.z <= 10 || currentRot.z >= 350))
        {
            transform.Rotate(0, 0, 1);
        }
        if ((Input.GetAxis("Horizontal") < -.2) && (currentRot.z >= 351 || currentRot.z <= 11))
        {
            transform.Rotate(0, 0, -1);
        }

        if ((Input.GetAxis("Vertical") < .2) && (currentRot.x <= 10 || currentRot.x >= 350))
        {
            transform.Rotate(1, 0, 0);
        }
        if ((Input.GetAxis("Vertical") > -.2) && (currentRot.x >= 351 || currentRot.x <= 11))
        {
            transform.Rotate(-1, 0, 0);
        }
    }
}
