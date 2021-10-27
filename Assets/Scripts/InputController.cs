using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    public bool isReady;

    public GameObject player;
    
    //private int count;
    // Start is called before the first frame update
    void Start()
    {
        isReady = true;
        GameManager.instance.inputController = this;
        GameManager.instance.inputController.player = GameObject.FindWithTag("Player") as GameObject;
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        if (moveInput.magnitude >= 1){ //getKeyDown으로 안하는이유?

            if (isReady) //once
            {
                isReady = false;
                player.GetComponent<Player>().Move(moveInput);
            }

        }
        else
        {
            isReady = true;
            //Debug.Log("ready");
        }

//            if (count > 2500)
//            {
//                moveInput.Normalize();
//                Debug.Log("press2: "+moveInput.magnitude+"/"+moveInput.sqrMagnitude+"/"+moveInput.normalized);
//            }
//            else
//            {
//                Debug.Log("press: "+moveInput.magnitude+"/"+moveInput.sqrMagnitude+"/"+moveInput.normalized);
//                count++;
//            }

        //isPressed = false;
    }
    
}
