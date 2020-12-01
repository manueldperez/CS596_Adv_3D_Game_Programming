using UnityEngine;

public class SetFollow : MonoBehaviour
{
    public void OnClick_SetFollow()
    {
        GameObject.Find("Flock Controller(Clone)").GetComponent<FlockController>().setFollowMode();
    }
}
