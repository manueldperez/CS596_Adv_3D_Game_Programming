using UnityEngine;

public class SetLazy : MonoBehaviour
{
    public void OnClick_SetLazy()
    {
        GameObject.Find("Flock Controller(Clone)").GetComponent<FlockController>().setLazyMode();
    }
}
