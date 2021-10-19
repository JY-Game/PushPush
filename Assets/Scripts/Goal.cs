using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    private Animator goalAnim;
    public bool achive;

    // Start is called before the first frame update
    void Start()
    {
        goalAnim = GetComponent<Animator>();
        achive = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Goal In");
        achive = true;
        goalAnim.SetBool("Succeed", true);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        Debug.Log("Goal Out");
        achive = false;
        goalAnim.SetBool("Succeed", false);
    }
}
