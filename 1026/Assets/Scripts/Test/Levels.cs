using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Level // A single level
{
    public List<string> m_Rows = new List<string>();

    public string[][] levels;
    
    /*public int Height { get { return m_Rows.Count; } }
    public int Width
    { // Width is length of longest row
        get
        {
            int maxLength = 0;
            foreach (var r in m_Rows)
            {
                if (r.Length > maxLength) maxLength = r.Length;
            }
            return maxLength;
        }
    }*/
}

public class Levels : MonoBehaviour
{
    public string filename;
    public List<Level> m_Levels;

    void Awake()
    {
        TextAsset textAsset = (TextAsset)Resources.Load(filename);
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
        string[] lines;
        lines = completeText.Split(new string[] { "\n" }, System.StringSplitOptions.None);
        m_Levels.Add(new Level());
        for (int i = 0; i < lines.Length; i++)
        {
            string line = lines[i];
            if (line.StartsWith(";"))
            {
                Debug.Log("New level added");
                m_Levels.Add(new Level());
                
                continue;
            }
            m_Levels[m_Levels.Count - 1].m_Rows.Add(line); // Always adding level rows to last level in list of levels
        }
    }
}