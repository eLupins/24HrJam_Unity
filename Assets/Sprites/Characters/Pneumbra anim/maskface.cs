using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

//** This script is specific to the Pneumbra Anim assets **/
public class maskface : baseEnemy {

    public Transform[] WayPoints;
    public int currentPos;

    public SkeletonAnimation skeletonAnimation;
    public Spine.AnimationState spineAnimationState;
    public Spine.Skeleton skeleton;
    public float waypointDistance;

    // Use this for initialization
    void Start()  {

        skeletonAnimation = GetComponent<SkeletonAnimation>();
        spineAnimationState = skeletonAnimation.AnimationState;
        skeleton = skeletonAnimation.Skeleton;
        
        spineAnimationState.SetAnimation(3, "walk",true);

        animator = GetComponent<Animator>();
        animator.Play("walk");
        base.Start(); //Utilizing Start() from baseEnemy.cs
        damage = 3;
        health = 5;
        speed = 3.0f;
        
	}
	
	// Update is called once per frame
	protected void Update () {
        base.Update(); //Utilizing Update() from baseEnemy.cs
        if(health > 0)
        {
            Pacer();

        }
        if (health <= 0)
        {
            skeletonAnimation.state.SetAnimation(1, "death", false);
            StartCoroutine(Death());
            
        }
        
    }

    
    public void Pacer()
    {
        //if Im at waypoint B...
        if (currentPos >= WayPoints.Length)
        {
            currentPos = 0;  // reset so that I go back to waypoint A
            skeleton.FlipX = false; 


        }
        transform.position = Vector3.MoveTowards(transform.position, WayPoints[currentPos].position, speed * Time.deltaTime);

        if (Vector2.Distance(transform.position, WayPoints[currentPos].position) < waypointDistance) //if Im at waypoint A...
        {
            currentPos++;// increase the current position so that I can movve to the next waypoint 
            skeleton.FlipX = true; 

        }

   
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "attack")
        {
            
            if (health <= 0)
            {

                skeletonAnimation.state.SetAnimation(1, "dmg", false);
            }
            else
            {
                animator.Play("dmg");
            }
        }

        if (col.gameObject.tag == "Player")
        {
            skeletonAnimation.state.SetAnimation(1, "headbutt", false);
            Debug.Log("The players been hit");
        }
    }


    public void OnDrawGizmos()
    {
        foreach (Transform t in WayPoints)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawIcon(t.position, "waypointIcon.png", false);
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(t.position, waypointDistance); 
        }
    }

    public IEnumerator Death()
    {
        skeletonAnimation = GetComponent<SkeletonAnimation>();
        Color alive = skeletonAnimation.skeleton.GetColor();
        alive.a = 1.0f;
        Color dead = skeletonAnimation.skeleton.GetColor();
        dead.a = 0.0f;
        float timer = 0.8f;

        if (hitbox.enabled)
        {
            while (timer > 0)
            {
                skeletonAnimation.skeleton.SetColor(Color.Lerp(alive, dead, (0.8f - timer / 0.8f)));
                yield return new WaitForSeconds(0.03f);
                timer -= 0.05f;

            }
            hitbox.enabled = false;
            skeletonAnimation.skeleton.SetColor(dead);

        }

       
    }
}


