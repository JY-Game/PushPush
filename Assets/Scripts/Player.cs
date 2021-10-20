using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    AudioSource moveAudio;
    // Start is called before the first frame update
    void Start()
    {
        moveAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Move(Vector2 direction) 
    {
        Debug.Log("dir:" + direction.magnitude);
        if (direction.sqrMagnitude >= 2)  //대각선 방지를 위해 
        {
            direction.y = 0;
        }
        

        if (!GameManager.instance.isWall(direction, new Vector2(transform.position.x,transform.position.y) ))
        {
            transform.Translate(direction);
            moveAudio.Play();
        }
        
    }
}
