using UnityEngine;
using System.Collections.Generic;

public class FlockController : MonoBehaviour
{
    // The number of boids in the flock
    [SerializeField]
    private int flockSize;

    // Speed modifer for the boid movement
    [SerializeField]
    private float speedModifier;

    // Weight modifier for alignment value's contribution to the flocking direction.
    [SerializeField]
    private float alignmentWeight;

    // Weight modifier for cohesion value's contribution to the flocking direction.
    [SerializeField]
    private float cohesionWeight;

    // Weight modifier for separation value's contribution to the flocking direction.
    [SerializeField]
    private float separationWeight;

    // Weight modifier for the target's contribution to the flocking direction.
    [SerializeField]
    private float followWeight;

    [Header("Boid Data")]

    // Boid object 
    [SerializeField]
    private Boid prefab;

    [SerializeField]
    private float spawnRadius;

    //Where the boids will spawn
    private Vector3 spawnLocation;


    [Header("Target Data")]

    [SerializeField]
    public Transform target;

    //Used to calculate the average center of the entire flock. Used in calculating cohesion.
    private Vector3 flockCenter;

    //Used to calculate the entire flock's direction. Used in calculating alignment.
    private Vector3 flockDirection;

    //The direction to the flocking target.
    private Vector3 targetDirection;

    //Separation value
    private Vector3 separation;

    public List<Boid> flockList = new List<Boid>();

    public float SpeedModifier
    {
        get
        {
            return speedModifier;
        }
    }

    public string targetMode;

    public string getMode()
    {
        return targetMode;
    }

    private Vector3 randPos;

    private void Awake()
    {
        flockList = new List<Boid>(flockSize);
        for (int i = 0; i < flockSize; i++)
        {
            //This is to calculate where the boids will spawn, and practically assures
            //that they don't spawn in the same position
            spawnLocation = Random.insideUnitSphere * spawnRadius + transform.position;
            Boid boid = Instantiate(prefab, spawnLocation, transform.rotation) as Boid;

            boid.transform.parent = transform;
            boid.FlockController = this;
            flockList.Add(boid);
        }
    }

    public Vector3 Flock(Boid boid, Vector3 boidPosition, Vector3 boidDirection)
    {
        target = GameObject.Find("Player(Clone)").transform;

        flockDirection = Vector3.zero;
        flockCenter = Vector3.zero;
        targetDirection = Vector3.zero;
        separation = Vector3.zero;

        for (int i = 0; i < flockList.Count; ++i)
        {
            Boid neighbor = flockList[i];
            //Check only against neighbors.
            if (neighbor != boid)
            {
                //Aggregate the direction of all the boids.
                flockDirection += neighbor.Direction;

                //Aggregate the position of all the boids.
                flockCenter += neighbor.transform.localPosition;

                //Aggregate the delta to all the boids.
                separation += neighbor.transform.localPosition - boidPosition;
                separation *= -1;
            }
        }

        //Alignment. The avereage direction of all boids.
        flockDirection /= flockSize;
        flockDirection = flockDirection.normalized * alignmentWeight;

        //Cohesion. The centroid of the flock.
        flockCenter /= flockSize;
        flockCenter = flockCenter.normalized * cohesionWeight;

        //Separation.
        separation /= flockSize;
        separation = separation.normalized * separationWeight;

        //Direction vector to the target of the flock.
        targetDirection = target.localPosition - boidPosition;
        targetDirection = targetDirection * followWeight;

        return flockDirection + flockCenter + separation + targetDirection;
    }

    public Vector3 LazyFlight(Boid boid)
    {
        if ((boid.transform.position - randPos).magnitude <= 5)
        {
            randPos.x = Random.Range(-20, 20);
            randPos.y = Random.Range(5, 10);
            randPos.z = Random.Range(-20, 20);
        }

        targetDirection = randPos - boid.transform.position;
        targetDirection *= followWeight;

        return targetDirection;
    }

    public Vector3 FollowMode(Boid boid)
    {
        if (target.gameObject.activeSelf == false)
        {
            target.gameObject.SetActive(true);
        }

        targetDirection = target.localPosition - boid.transform.position;
        targetDirection *= followWeight;

        return targetDirection;
    }


    public void setLazyMode()
    {
        targetMode = "Lazy";
    }

    public void setFollowMode()
    {
        targetMode = "Follow";
    }
}