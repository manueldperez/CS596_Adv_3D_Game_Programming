using UnityEngine;

public class TargetMovement : MonoBehaviour
{
    [SerializeField]
    private Vector3 bounds;

    [SerializeField]
    private float moveSpeed;

    [SerializeField]
    private float turnSpeed;

    [SerializeField]
    private float targetPointTolerance;

    private Vector3 initialPosition;
    private Vector3 nextMovementPoint;
    private Vector3 targetPosition;

    private void Awake()
    {
        initialPosition = transform.position;
        CalculateNextMovementPoint();
    }

    //Indicates the way and how fast the target moves to the next calculated point
    private void Update()
    {
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(nextMovementPoint - transform.position), turnSpeed * Time.deltaTime);

        if (Vector3.Distance(nextMovementPoint, transform.position) <= targetPointTolerance)
        {
            CalculateNextMovementPoint();
        }
    }

    //Calculates where the target object goes
    private void CalculateNextMovementPoint()
    {
        float posX = Random.Range(initialPosition.x - bounds.x, initialPosition.x + bounds.x);
        float posY = Random.Range(initialPosition.y - bounds.y, initialPosition.y + bounds.y);
        float posZ = Random.Range(initialPosition.z - bounds.z, initialPosition.z + bounds.z);
        targetPosition.x = posX;
        targetPosition.y = posY;
        targetPosition.z = posZ;
        nextMovementPoint = initialPosition + targetPosition;
    }
}