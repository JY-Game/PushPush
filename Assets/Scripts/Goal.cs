using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    private Animator goalAnim;
    public bool achieve;
    
    // Start is called before the first frame update
    void Start()
    {
        goalAnim = GetComponent<Animator>();
        achieve = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("Goal in");
        goalAnim.SetBool("Succeed", true);

        GetComponent<SpriteRenderer>().sortingLayerName = "Complete";
        achieve = true;
        GameManager.instance.IsClear();
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        Debug.Log("Goal out");
        goalAnim.SetBool("Succeed", false);
        
        GetComponent<SpriteRenderer>().sortingLayerName = "Default";
        achieve = false;
    }
}
