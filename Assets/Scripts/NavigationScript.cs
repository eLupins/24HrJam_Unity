using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavigationScript : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Transform Target;
    [SerializeField] public Transform[] patrolpath;
    public int searchD = 20;
    int pathindex = 0;
    NavMeshAgent navSelf;
    enum NavStates
    {
        PATROL,
        CHASE
        //Add more states here if you want to expand functionality, but you will likely have to rewrite the whole FSA!
    }
    NavStates curState = NavStates.PATROL;
    void Start()
    {
        navSelf = GetComponent<NavMeshAgent>();
        navSelf.destination = patrolpath[0].position;
    }

    // Update is called once per frame
    void Update()
    {
        if(curState == NavStates.PATROL)
        {
            if (Vector3.Distance(transform.position, Target.position) < searchD)
            {
                navSelf.destination = Target.position;
                curState = NavStates.CHASE;
            }
            if(Vector3.Distance(transform.position, patrolpath[pathindex].position) < 1f)
            {
                pathindex++;
                if (pathindex > patrolpath.Length-1) pathindex = 0;
                navSelf.destination = patrolpath[pathindex].position;            
            }
        }
        if(curState == NavStates.CHASE)
        {
            navSelf.destination = Target.position;
            if (Vector3.Distance(transform.position, Target.position) >= searchD)
            {
                navSelf.destination = patrolpath[pathindex].position;
                curState = NavStates.PATROL;
            }
        }

    }
}
