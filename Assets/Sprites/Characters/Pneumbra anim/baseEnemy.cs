using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

//** This script is specific to the Pneumbra Anim assets **/
public class baseEnemy : MonoBehaviour
{

    public GameObject enemy;
    public float damage;//damage to deal if it hits the player
    public int health; //health
    public float speed;  //walk speed
    public Animator animator; // enemy's animation
    private Transform targePlayer;//transform to attempt toward each turn
    float force; //for knockback 
    
    public AudioClip deathClip; //audio when it dies
    
    public ParticleSystem hitParticles; //emit a particle effect when the player hits it 
    
    /// SPINE BASED VARIABLES ///
    public MeshRenderer meshRend;

    public Rigidbody2D rgbd;
    public Collider2D hitbox; //definitive hitbox
    public float dieTimer = 5.0f;
    protected float dieRemaining;
    public CircleCollider2D attackRange; // the circle collider will serve as a way to detect when the player is nearby

    /*
     * Below this cut are variables to modify the shader
     */
public Material material;

    [Range(0, 1)]
    public float magnitude;
    public Color tint;
    public float DisplaceX;
    public float DisplaceY;
   
    // Use this for initialization
    protected void Start()
    {

        if (attackRange)
        {
            attackRange = enemy.GetComponent<CircleCollider2D>();
        }
        dieRemaining = Time.time;
        //if theres a gameobject enemy assigned...
        if (enemy)
        {
            rgbd = enemy.GetComponent<Rigidbody2D>();
            hitbox = enemy.GetComponent<Collider2D>();
            meshRend = enemy.GetComponent<MeshRenderer>();
        }
       
    }

    // Update is called once per frame
    protected void Update()
    {

        //Below modify the shader properties 
        material.SetFloat("_Magnitude", magnitude/100);
        material.SetColor("_Color", tint);
        material.SetFloat("_DisplaceX", DisplaceX);
        material.SetFloat("_DisplaceY", DisplaceY);
    }

    //a function to handle how the enemy deals with taking damage 
    public void TakeDamage(int amount) //make this modifiable on a per enemy basis
    {
        //figure out which ability the enemy is being hit by!! then calculate what damage to take

        health -= amount;
        
        if (hitParticles)
        {
            hitParticles.Play();
        }
        Debug.Log(enemy + "has been hit");
        
        if (health <= 0)
        {
            Deactivate();
            hitbox.enabled = false;
            Debug.Log(enemy + "has died");
        }

    }

    public IEnumerator Deactivate()
    {
        yield return new WaitForSeconds(5);
        gameObject.SetActive(false);
        
    }
}


  
