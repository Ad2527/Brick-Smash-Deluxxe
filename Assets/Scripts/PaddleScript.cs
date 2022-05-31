using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleScript : MonoBehaviour
{
    //could be used to alter the paddle in later features
    public float speed;
    public float rightScreenEdge;
    public float leftScreenEdge;

    public AudioClip lifeUp;
    public AudioClip sizeUp;
    public AudioClip sizeDown;
    new AudioSource audio;
    public bool levelThree;
    public GameManager gm;
    Command command;
    // Start is called before the first frame update
    void Start()
    {
        command = new Command(transform);
        audio = GetComponent<AudioSource>();
        levelThree = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (gm.gameOver) {
            return;
        }

        //move according to the horizontal axis
        float horizontal = Input.GetAxis("Horizontal");
        command.ExecutePaddleMoveCommand(horizontal,speed);

        //then check if player had met the horizontal boundaries
        if (transform.position.x < leftScreenEdge) {
            command.ExecutePushCommand(leftScreenEdge);
        }
        if (transform.position.x > rightScreenEdge){
            command.ExecutePushCommand(rightScreenEdge);
        }
    }

    private void OnTriggerEnter2D(Collider2D other){

        //Extra Life PowerUp
        if (other.CompareTag("extraLife"))
        {
            audio.PlayOneShot(lifeUp,0.8f);
            gm.UpdateLives(1);
            Destroy(other.gameObject);
        }
        //Length Power up
        if (other.CompareTag("incSize")) {
            audio.PlayOneShot(sizeUp, 0.9f);
            Debug.Log("Length Power up");
            StartCoroutine(ExtendPaddleLength());
            Destroy(other.gameObject);
        }
    }

    IEnumerator ExtendPaddleLength() {
        command.ChangePaddleSizeCommand(1.2f);
        yield return new WaitForSeconds(7);
        command.ChangePaddleSizeCommand(0.8f);
        audio.PlayOneShot(sizeDown, 0.9f);
    }
}
