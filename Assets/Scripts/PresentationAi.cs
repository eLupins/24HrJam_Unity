using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PresentationAi : MonoBehaviour
{
    // Start is called before the first frame update
    NavMeshAgent navSelf;
    [SerializeField] Transform target;
    [SerializeField] Transform home;
    public int searchD = 20;
    float timer = 10;
    enum AiStates
    {
        Wait,
        Chase,
        Alert
    }
    AiStates curState = AiStates.Wait;
    void Start()
    {
        navSelf = this.GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (curState)
        {
            case AiStates.Wait:
                navSelf.destination = home.position;
                if (Vector3.Distance(this.transform.position,target.position) < searchD){
                    curState = AiStates.Chase;
                }
                break;
            case AiStates.Chase:
                navSelf.destination = target.position;
                if (!(Vector3.Distance(this.transform.position, target.position) < searchD))
                {
                    curState = AiStates.Alert;
                }
                break;
            case AiStates.Alert:
                if(timer <= 0)
                {
                    timer = 10;
                    curState = AiStates.Wait;
                  
                    break;
                }
                if((Vector3.Distance(this.transform.position, target.position) < searchD))
                {
                    curState = AiStates.Chase;

                }
                timer -= 1*Time.deltaTime;
                break;

        }   
    }
}
