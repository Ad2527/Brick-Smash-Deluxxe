using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BallScript : MonoBehaviour
{

    public Rigidbody2D rb;

    public bool inPlay;
    //store a reference to the paddle
    public Transform paddle;
    //default speed variable for the ball
    public float speed;
    // Start is called before the first frame update
    public Transform explosion;
    //create a reference to our gamemanager
    public GameManager gm;
    public Transform powerupLife;

    public Transform powerupLength;
    public Transform[] powerUps;
    new AudioSource audio;
    void Start()
    {
        //stores the component the script is attached to as rb upon start
        rb = GetComponent<Rigidbody2D>();
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gm.gameOver) {
            return;
        }
        //check if the ball is in play
        if (!inPlay) {
            transform.position = paddle.position;
        }
        //if the spacebar has been pressed and the ball is not currently in play
        if (Input.GetButtonDown("Jump") && !inPlay) {
            inPlay = true;
            rb.AddForce(Vector2.up * speed);
        }
        if (gm.levelChange) {
            rb.velocity = Vector2.zero;
            inPlay = false;
            gm.levelChange = false;
        }

    }
    //run when this ball hits a trigger collider
    void OnTriggerEnter2D(Collider2D other) {
        //if the ball hits the bottom trigger
        if (other.CompareTag("bottom")) {
            Debug.Log("Ball hit the bottom of the screen");
            //Remove the Ball's momentum
            rb.velocity = Vector2.zero;
            inPlay = false;

            //update the players lives, by losing one
            if (gm.gameOver == false)
            {
                gm.UpdateLives(-1);
            }
        }
    }

    void OnCollisionEnter2D(Collision2D other) {
        //if the Object that the ball has hit is a brick
        if (other.transform.CompareTag("brick")) {

            BrickScript brickScript = other.gameObject.GetComponent<BrickScript>();

            if (brickScript.hitsToBreak > 1)
            {
                brickScript.BreakBrick();
            } else {
                int randomChance = Random.Range(1, 101);
                if (randomChance < 16)
                {
                    //Instantiate(powerupLife, other.transform.position, other.transform.rotation);
                    var RndB = new System.Random();
                    int index = RndB.Next(powerUps.Length);
                    Instantiate(powerUps[index], other.transform.position, other.transform.rotation);
                }

                //create an explosion animation in place of the brick that was hit
                Transform newExplosion = Instantiate(explosion, other.transform.position, other.transform.rotation);
                //clean the temporary created explosion out of the heirachy
                Destroy(newExplosion.gameObject, 2.5f);

                //access the point value for the brick that was just broken from its data script
                gm.updateScore(brickScript.points);
                gm.UpdateNumerOfBricks();
                Destroy(other.gameObject);
            }

            audio.Play();
        }
    }
    public void switchInPlay(){
        inPlay = !inPlay;
    }
    
}
