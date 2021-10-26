using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private AudioSource move;
    // Start is called before the first frame update
    void Start()
    {
        move = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Move(Vector2 direction)
    {
        //direction.Normalize();
        Debug.Log("dir:"+direction.sqrMagnitude); //대각선일때(diagonally) 2 
        if (direction.sqrMagnitude >= 2) //대각선방지
        {
            direction.y = 0;
        }

        //Vector2 nextPosition = direction + new Vector2(transform.position.x,transform.position.y);

        //Debug.Log("wall:"+ GameManager.isWall(nextPosition));
        if (!GameManager.instance.isWall(direction, new Vector2(transform.position.x, transform.position.y)))
        {
            transform.Translate(direction);
            move.Play();
        }
            
        
        //if(isClear()) Debug.Log("stage clear!");
    }

    public void OnTriggerStay2D(Collider2D other)
    {
        Debug.Log("o:"+other.gameObject.name);
    }
}
