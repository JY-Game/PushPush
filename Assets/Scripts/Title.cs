using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("TitleSkip",11f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void TitleSkip()
    {
        SceneManager.LoadScene("GameScene");
    }
}
