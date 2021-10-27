using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject Ball;
    public GameObject Player;
    public GameObject Floor;
    public GameObject Wall;
    public GameObject Goal;

    public static GameManager instance;

    public string[] Level;
    public int level;
    public GameObject[][] map;

    [SerializeField]
    public string[][] levels = {
        new string[]
        {
            "#####",
            "#@o*#",
            "#####"
        }
    };

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;

        Level = levels[level];
        SetBoard();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SetBoard()
    {
        Debug.Log("SetBoard:" + Level.Length);
        map = new GameObject[Level.Length][];

        GameObject tile = null;
        for (int y = 0; y < Level.Length; y++)//for (int y =Level.Length-1; y>=0; y--)
        {
            string row = Level[y];
            map[y] = new GameObject[row.Length];

            for (int x = 0; x < row.Length; x++)//for (int x=row.Length-1;x>=0;x--)
            {
                Debug.Log("SetRow:" + row.Length);
                switch (row[x])
                {
                    case '#':
                        tile = Wall;
                        break;
                    case '@':
                        tile = Player;
                        Instantiate(Floor, new Vector2(x - row.Length / 2, Level.Length / 2 - y), Quaternion.identity);
                        break;
                    case '.':
                        tile = Floor;
                        break;
                    case 'o':
                        tile = Ball;
                        Instantiate(Floor, new Vector2(x - row.Length / 2, Level.Length / 2 - y), Quaternion.identity);
                        break;
                    case '*':
                        tile = Goal;
                        break;
                }

                map[y][x] = Instantiate(tile, new Vector2(x - row.Length / 2, Level.Length / 2 - y), Quaternion.identity);
                map[y][x].name = row[x].ToString();
            }
        }

    }

    public bool isWall(Vector2 direction, Vector2 currentPosition)
    {
        Vector2 nextPosition = currentPosition + direction;

        int y = Mathf.FloorToInt(Level.Length / 2f - nextPosition.y);
        int x = Mathf.FloorToInt(Level[y].Length / 2f + nextPosition.x);

        GameObject next = map[y][x];
        Debug.Log("next:" +next.name + "/"+y+x);

        if(next.name == '#'.ToString())
        {
            return true;
        }

        return false;
    }
}
