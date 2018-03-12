using UnityEngine;
using System.Collections;
public class Enemy_AI : MonoBehaviour
{
    [SerializeField] float stunTime = 10; //In seconds
    [SerializeField] float rotSpeed = 3.0f;
    [SerializeField] float maxMoveSpeed = 3.0f;
    [SerializeField] float stopRadius = 2;
    [SerializeField] float speedRechargeRate = 0.5f; // speedUp per second
    [SerializeField] GameObject drop;

    private Transform tr_Player; //player = kid
    private float speedAsHealth;
    private bool isStunned;
    private bool isDead;
    private float timeOfDeath;
    private Timer timer; //might need to change this. But it works.
    private float currentMoveSpeed;

    public int maxDrops;
    // Use this for initialization 
    void Start()
    {
        isStunned = false;
        isDead = false;
        speedAsHealth = currentMoveSpeed / maxMoveSpeed;
        tr_Player = GameObject.FindGameObjectWithTag("Player").transform;
        currentMoveSpeed = maxMoveSpeed;
    }
    // Update is called once per frame 
    void Update()
    {
        ///CODE TO COPY PASTE
        speedAsHealth = currentMoveSpeed / maxMoveSpeed;
        if (isDead)
        {
            if (timeOfDeath <= Time.time)
            {
                Object.Destroy(transform.gameObject);
            }
        }
        else
        {
            if (isStunned)
            {
                //Prepare for reactivation
                currentMoveSpeed = maxMoveSpeed;
                if (timer.timer <= 0)
                {
                    print("Enemy is unStunned!!!!");
                    isStunned = false;
                }
                else
                {
                    print("Counting DOWN!");
                    timer.countDown(Time.deltaTime);
                    //print(Mathf.Floor(timer.timer));
                }
            }
            else
            {

                if (currentMoveSpeed <= maxMoveSpeed)
                    currentMoveSpeed += speedRechargeRate * Time.deltaTime;
                //////////////////////////////


                ///BASIC AI FOR ENEMY
                if (Vector3.Distance(transform.position, tr_Player.position) >= stopRadius)
                {    /* Look at Player*/
                    transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(tr_Player.position - transform.position), rotSpeed * Time.deltaTime);
                    /* Move at Player*/
                    transform.position += transform.forward * currentMoveSpeed * Time.deltaTime;
                }
            }
        }
    }

    /// <summary>
    /// CODE TO COPY PASE
    /// It reduces speed by the amount specified in param
    /// </summary>
    /// <param name="slowDownSpeed"></param>
    public void ModifySpeed(float slowDownSpeed)
    {
        if (!isDead) //otherwise do nothing
        {
            if (!isStunned)
            {
                if (currentMoveSpeed > 0)
                {
                    currentMoveSpeed -= slowDownSpeed;
                }
                else
                {
                    //In case of speed being negative
                    currentMoveSpeed = 0;

                    //sets the AI to a stunned State
                    isStunned = true;
                    print("Enemy is stunned");

                    //Might need to use an IEnum here, still keeping timer for now as it works.
                    timer = new Timer(stunTime);
                }
            }
            else
            {
                currentMoveSpeed = 0; //removes negatives if it is negative
                timer.reset();
            }
        }
    }

    /// <summary>
    /// Plays death animation and sets deathFlag to true
    /// Note that the destroy of the enemy is in update
    /// Coincidently it also stops countDown of stun
    /// </summary>

    public void kill()
    {
        if (isStunned)
        {
            print("killing enemy");
            if (!isDead) dropLights();
            ///playDeathAnimation HERE
            /// Get Animation time;
            /// Animation.play(deathAnimation)
            isDead = true;
            timeOfDeath = Time.time + 3;//+ AnimationTime (3 is a placeHolder);
            ///moved destroy to update
            ///then call drops

        }

        else print("cannot kill enemy");
    }

    private void dropLights()
    {
        float amountToDrop = Mathf.Floor(Random.Range(1, maxDrops+1));
        for(int i = 0; i < amountToDrop; i++)
        {
            Instantiate(drop, transform.position, Random.rotation);
        }
    }

    public float getHealth()
    {
        return speedAsHealth;
    }
}