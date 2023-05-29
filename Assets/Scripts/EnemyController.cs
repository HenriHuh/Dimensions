using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    public List<Transform> waypointTrans = new List<Transform>();
    public GameObject targetObject;
    public float visionDistance = 20;
    public float moveSpeed = 20;
    public Transform playerTransform;
    public float visionAngle = 20;
    public float visionRange = 10;
    public float rotationSpeed = 10;
    public Animator animator;

    private int currentWaypoint = 0;

    private Vector3[] waypoints;

    private void Awake()
    {
        waypoints = new Vector3[waypointTrans.Count];
        for (int i = 0; i < waypoints.Length; i++)
        {
            waypoints[i] = waypointTrans[i].transform.position;
            Destroy(waypointTrans[i].gameObject);
        }

        waypointTrans.Clear();
    }

    private void Update()
    {
        if (IsPlayerVisible())
        {
            Vector3 movement = Vector3.MoveTowards(transform.position, waypoints[currentWaypoint], moveSpeed * Time.deltaTime * 2);
            animator.SetFloat("WolfSpeed", 2);

            if (movement - transform.position != Vector3.zero)
            {

                Quaternion toRotation = Quaternion.LookRotation(movement, Vector3.up);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
            }


            if(Vector3.Distance(transform.position, playerTransform.position) < 1)
            {
                playerTransform.GetComponent<PlayerController>().GameOver();
            }
        }
        else
        {

            animator.SetFloat("WolfSpeed", 1);

            if (Vector3.Distance(transform.position, waypoints[currentWaypoint]) < 1)
            {
                Debug.Log("ye" + waypoints.Length);


                currentWaypoint = Tools.RepeatInt(currentWaypoint + 1, waypoints.Length);
            }

            Vector3 movement = Vector3.MoveTowards(transform.position, waypoints[currentWaypoint], moveSpeed * Time.deltaTime);

            Debug.Log("move to wp " + movement);
            if (movement - transform.position != Vector3.zero)
            {
                Quaternion toRotation = Quaternion.LookRotation(movement, Vector3.up);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
            }

            transform.position = movement;
        }
    }

    private bool IsPlayerVisible()
    {
        Vector3 directionToPlayer = playerTransform.position - transform.position;
        float angleToPlayer = Vector3.Angle(directionToPlayer, transform.forward);

        if (angleToPlayer <= visionAngle * 0.5f)
        {
            return true;
        }

        return false;
    }

}
