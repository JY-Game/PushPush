using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Numerics;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using Quaternion = UnityEngine.Quaternion;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class LevelEditor : MonoBehaviour
{
    [SerializeField] private InputField numberOfSize;
    [SerializeField] private Canvas canvas;

    [SerializeField]
    private GameObject block;
    
    private int size;

    public GameObject[] blocks;

    public void SetSize()
    {
        size = Convert.ToInt32(numberOfSize.text);
        Debug.Log("size:"+size);
        int length = 4;
        List<GameObject> test = new List<GameObject>();

        for (int i = 0, x=0,y=0; i < size*size; i++,x++) //sizedelta :72 size:4  
        {
            Vector2 sizeDelta = block.GetComponent<RectTransform>().sizeDelta;
           
            //Debug.Log((-size*sizeDelta.x)/2f + sizeDelta.x*x);
            //Debug.Log((size*sizeDelta.y)/2f - sizeDelta.y*y);
            GameObject g = Instantiate(block, new Vector3(), Quaternion.identity,canvas.transform);
            g.name = i.ToString();
            
            if (x==size)
            {
                y++;
                x = 0; 
                //Debug.Log(i);
            }
            g.GetComponent<RectTransform>().sizeDelta = new Vector2(sizeDelta.x*length/size,sizeDelta.y*length/size);
            sizeDelta = g.GetComponent<RectTransform>().sizeDelta;
            g.GetComponent<RectTransform>().anchoredPosition = new Vector3(((1-size)*sizeDelta.x)*0.5f + sizeDelta.x*x, ((size-1)*sizeDelta.y)*0.5f - sizeDelta.y*y);
            g.transform.SetParent(numberOfSize.transform.parent);
            g.GetComponent<RectTransform>().SetSiblingIndex(0);
            test.Add(g);
            //new Vector3((-size*sizeDelta.x)/2f + sizeDelta.x*x + sizeDelta.x*0.5f, (size*sizeDelta.y)/2f - sizeDelta.y*y - sizeDelta.y*0.5f);
        }

        blocks = test.ToArray();
    }

    /*public void GenerateItem(GameObject item)
    {
        
        GameObject i = Instantiate(item,item.transform.position,Quaternion.identity, item.transform.parent);
        i.AddComponent<DragAndDrop>();
        i.GetComponent<Button>().onClick = null;
        //i.GetComponent<DragAndDrop>().canvas = canvas;
        //i.GetComponent<DragAndDrop>().canvasGroup = item.GetComponent<CanvasGroup>();
    }*/
    
    [ContextMenu("레벨저장")]//[MenuItem("Tools/Write file")]
    public void WriteLevel()
    {
        string path = "Assets/file.txt";
        string level = "";
        StreamWriter streamWriter = File.AppendText(path);

        for (int i = 0; i < size * 3; i++)
        {
            level += blocks[i].GetComponent<Block>().item;
            if ((i+1)%size==0)
            {
                streamWriter.WriteLine(level);
                Debug.Log("write");
                level = "";
            }
        }
        streamWriter.WriteLine(";");
        streamWriter.Close();

        //File.AppendAllText(path, System.String.Format("{0} {1} {2}\n", "say", "what", "happen"));
    }


    [ContextMenu("테스트")] //[MenuItem("Tools/Write file")]
    public void test()
    {
        /*long[] nn = new long [1001];
        for (int i = 0; i < nn.Length;i++) {
            if (i < 2) nn[i] = i;
            else nn[i] = nn[i - 1] + nn[i-2];
            Debug.Log(nn[i]);
        }
        Debug.Log("tete:"+nn[nn.Length-1]%1000000);*/
        var s = Console.ReadLine().Split();
        BigInteger n = BigInteger.Parse(s[0]);
        BigInteger  pv = 0, cr = 1, result = 0, d = 1000000;;
        for (int i =0;i<n-1;i++)
        {
            result = pv + cr;
            pv = cr;
            cr = result;
        }
        BigInteger r = BigInteger.Remainder(result,d);
        
        Debug.Log(r.ToString());
    }
}
