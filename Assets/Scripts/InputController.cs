using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    public bool isReady; 
    public Player player;

    // Start is called before the first frame update
    void Start()
    {
        isReady = true;

        player = GameObject.FindWithTag("Player").GetComponent <Player>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"),Input.GetAxisRaw("Vertical"));
        if (moveInput.magnitude >= 1)
        {
            if (isReady)
            {
                isReady = false;
                player.Move(moveInput);
            }

        }else
        {
            isReady = true;
            //Debug.Log("ready");
        }
        //Debug.Log("input:" + moveInput);
    }
}
