using UnityEngine;

public class Boid : MonoBehaviour
{
    [SerializeField]
    private FlockController flockController;

    //The modified direction for the boid.
    private Vector3 targetDirection;

    //The Boid's current direction.
    private Vector3 direction;

    public FlockController FlockController
    {
        get { return flockController; }

        set { flockController = value; }
    }

    public Vector3 Direction
    {
        get
        {
            return direction;
        }
    }

    private void Awake()
    {
        direction = transform.forward.normalized;
        if (flockController != null)
        {
            Debug.LogError("You must assign a flock controller!");
        }
    }

    private void Update()
    {
        targetDirection = GameObject.Find("Flock Controller(Clone)").GetComponent<FlockController>().Flock(this, transform.localPosition, direction);

        if (GameObject.Find("Flock Controller(Clone)").GetComponent<FlockController>().getMode() == "Follow")
        {
            targetDirection += GameObject.Find("Flock Controller(Clone)").GetComponent<FlockController>().FollowMode(this);
        }
        if (GameObject.Find("Flock Controller(Clone)").GetComponent<FlockController>().getMode() == "Lazy")
        {
            targetDirection = GameObject.Find("Flock Controller(Clone)").GetComponent<FlockController>().LazyFlight(this);
        }

        if (targetDirection == Vector3.zero)
        {
            return;
        }
        direction = targetDirection.normalized;
        direction *= flockController.SpeedModifier;
        transform.Translate(direction * Time.deltaTime);
    }
}