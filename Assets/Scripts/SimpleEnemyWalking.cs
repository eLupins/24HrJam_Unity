using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//** PLEASE NOTE THIS IS FOR 2D SPACE ONLY **//

public class SimpleEnemyWalking : MonoBehaviour
{

    
    public GameObject enemy;
    public Transform[] WayPoints;
    public int currentWayPoint;
    public float Speed = 1.0f;
    public Collider attackRange;
    private SpriteRenderer renderer;
    private Material mat;

    public Renderer rend;
    // Use this for initialization
    void Start()
    {
        renderer = enemy.GetComponent<SpriteRenderer>();
        mat = enemy.GetComponent<Material>();
    }

    // Update is called once per frame
    void Update()
    {

        //If im at waypoint A, then increase the currentWayPoint so that I move to the next WayPoint
        if (transform.position == WayPoints[currentWayPoint].position)
        {
            currentWayPoint++;
            renderer.flipX = true;
        }

        //If Im at waypoint B, reset so I go back to waypoint A
        if (currentWayPoint >= WayPoints.Length)
        {
            currentWayPoint = 0;
            renderer.flipX = false;
        }
       
        //heres teh actual movement
        transform.position = Vector3.MoveTowards(transform.position, WayPoints[currentWayPoint].position, Speed * Time.deltaTime);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        Debug.Log("Boop.");
    }
}
