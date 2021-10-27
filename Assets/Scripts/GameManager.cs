using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject ball;
    public GameObject floor;
    public GameObject goal;
    public GameObject player;
    public GameObject wall;

    public static GameManager instance;
    
    public  int level;
    
    [SerializeField]
    public string[][] levels ={
        /*new string[] {
            "#####",
            "#@o*#",
            "#####"
        },
        new string[] {
            "#########",
            "##......#",
            "#...*...#",
            "#...o...#",
            "#.*o@o*.#",
            "####o...#",
            "####*...#",
            "####....#",
            "#########"
        },
        new string[] {
            "#########",
            "#....***#",
            "#.#.#*#*#",
            "#.#..***#",
            "#.ooo.#.#",
            "#.o@o...#",
            "#.ooo##.#",
            "#.......#",
            "#########"}*/
    };
    
//    [SerializeField]
//    public string[] level1  ={
//        "#########",
//        "#....***#",
//        "#.#.#*#*#",
//        "#.#..***#",
//        "#.ooo.#.#",
//        "#.o@o...#",
//        "#.ooo##.#",
//        "#.......#",
//        "#########"
//    };

    public string[] Level;
//    {
//        "#####",
//        "#@o*#",
//        "#####"
//    };

    
    public GameObject[][] map;
    //private static string[][] map = {Level};

    public InputController inputController;

    private void Awake()
    {
       
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
        
    }

    private void Start()
    {
        ResetGame(default, LoadSceneMode.Single);
        SceneManager.sceneLoaded += ResetGame;
    }

    private void ResetGame(Scene arg0, LoadSceneMode loadSceneMode)
    {
        LevelIO();
        level = GameManager.instance.level;
        Level=levels[level];
        SetBoard();
        
        //inputController = GameObject.FindObjectOfType<InputController>().GetComponent<InputController>();
        //inputController.player = GameObject.FindWithTag("Player") as GameObject;
        //SceneManager.sceneLoaded -= ResetGame;
    }


    void SetBoard()
    {
        Debug.Log("SetBoard:"+Level.Length);
        map = new GameObject[Level.Length][];
        
        GameObject tile = null;
        for (int y =0; y<Level.Length; y++)//for (int y =Level.Length-1; y>=0; y--)
        {
            string row = Level[y];
            map[y]=new GameObject[row.Length];
            
            for (int x =0; x<row.Length; x++)//for (int x=row.Length-1;x>=0;x--)
            { Debug.Log("SetRow:"+row.Length);
                switch (row[x])
                {
                    case '#':
                        tile = wall; 
                        break;
                    case '@':
                        tile = player;
                        Instantiate(floor,new Vector2(x-row.Length/2,Level.Length/2-y), Quaternion.identity);
                        break;
                    case '.':
                        tile = floor; 
                        break;
                    case 'o':
                        tile = ball; 
                        Instantiate(floor,new Vector2(x-row.Length/2,Level.Length/2-y), Quaternion.identity);
                        break;
                    case '*':
                        tile = goal;
                        break;
                }

                map[y][x] = Instantiate(tile,new Vector2(x-row.Length/2,Level.Length/2-y), Quaternion.identity);
                map[y][x].name = row[x].ToString();
            }
        }
        
    }

    public bool isWall(Vector2 direction, Vector2 currentPosition)
    {
        Vector2 position = direction + currentPosition;

        int y = Mathf.FloorToInt(Level.Length / 2f - position.y); //Mathf.CeilToInt(f: Level.Length / 2f - position.y) - 1;
        int x = Mathf.FloorToInt(Level[y].Length / 2f + position.x); //Mathf.CeilToInt(Level[y].Length / 2f + position.x) - 1;

        int cy = Mathf.FloorToInt(Level.Length / 2f - currentPosition.y); //Mathf.CeilToInt(f: Level.Length / 2f - currentPosition.y) - 1;
        int cx = Mathf.FloorToInt(Level[y].Length / 2f + currentPosition.x); //Mathf.CeilToInt(Level[y].Length / 2f + currentPosition.x) - 1;
        
        Debug.Log("cy:"+cy+"/cx:"+cx+"/y:"+y+"/x:"+x+"/"+map[y][x]);

        GameObject next = map[y][x];
        Debug.Log("wall: "+next.name);

        if (next.name == '#'.ToString())
        {
            return true;
        }
        if (next.name=='o'.ToString())
        {
            if (!isWall(direction, position))
            {
                next.transform.Translate(direction);
                next = map[y][x];
                GameObject current = map[cy][cx];
                map[y][x] = current;
                map[cy][cx] = next;
                Debug.Log("bomb: "+map[cy][cx].name+" / "+map[y][x].name);
                return false;
            }
            //isGoal
            
            return true;
        }

        Debug.Log("wall remove");

        GameObject p = map[cy][cx];
        map[y][x] = p;
        map[cy][cx] = next;
        Debug.Log("fin: "+map[cy][cx].name+" / "+map[y][x].name);
        
        return false;
    }
    
    public bool IsClear() // 한번 움직일때마다 o랑 *의 위치가 일치하는 갯수 확인.
    {
        GameObject[] goals = GameObject.FindGameObjectsWithTag("Goal");
        foreach (GameObject g in goals)
        {
            if (!g.GetComponent<Goal>().achieve) return false;
        }
        Debug.Log("stage clear!");
        NextLevel();
        return true;
    }

    private void NextLevel()
    {
        level++;
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public string filename;

    private void LevelIO()
    {
        TextAsset textAsset = (TextAsset) Resources.Load(filename);
        if (!textAsset)
        {
            Debug.Log("Levels: " + filename + ".txt does not exist");
            return;
        }
        else
        {
            Debug.Log("Levels imported");
        }

        string completeText = textAsset.text;
        string[] lines = completeText.Split(new string[] {"\n"}, System.StringSplitOptions.None);
        CreateLevel(textAsset);

        for (int i =0, y = 0, x = 0; i < lines.Length; i++)
        {
            string line = lines[i].Replace("\r", "").Replace("\n","");
            if (line.StartsWith(";"))
            {
                y++;
                x = 0;
                continue;
            }

            Debug.Log("line added:"+line.Length+"/"+y+"/"+x);
            levels[y][x] = line;
            x++;
        }
    }

    private void CreateLevel(TextAsset textAsset)
    {
        string completeText = textAsset.text;
        string[] lines = completeText.Split(new string[] {"\n"}, System.StringSplitOptions.None);
        MatchCollection matches = Regex.Matches(completeText,";");
        levels = new string[matches.Count][];
       
        for (int i = 0, s = 0,c = 0; i < lines.Length; i++)
        {
            string line = lines[i].Replace("\r", "").Replace("\n", "");
            if (line.StartsWith(";"))
            {
                levels[s] = new string[c];
                Debug.Log("stageCount:"+c +"/"+s);
                c = 0;
                s++;
                continue;
            }

            c++;
        }

    }
}
