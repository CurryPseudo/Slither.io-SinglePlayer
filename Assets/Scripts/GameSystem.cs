using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
public class GameSystem : MonoBehaviour {
    public struct node
    {
        public node(int x,int y)
        {
            index = x;
            bodylong = y;
            return;
        }
        public int index; // Player -1
        public int bodylong;
    }
    public int playerlength;
    public int playerkills;
    public List<node> longlist;
    public GameObject food_p;
    public GameObject enemy_p;
    public GameObject bg;
    public GameObject[] enemys;
    public GameObject player;
    public GameObject canvas;
    public Color[] enemyscolor;
    public int food_count=0;
    public float creattime;
    private System.Random rd;
    public int enemynums = 100;
    // Use this for initialization
    public void rebuildenemy(int index,int length)
    {
        Destroy(enemys[index]);
        creatoneenemy(index, length);        
    }
    public void creatonefood()
    {
        Vector3 creatposition = new Vector3(((float)rd.NextDouble() - 0.5f) * 79, ((float)rd.NextDouble() - 0.5f) * 44, 0);
        GameObject food = Instantiate(food_p, creatposition, transform.rotation) as GameObject;
        food.GetComponent<SpriteRenderer>().sortingOrder = food_count++;
    }
    public void creatoneenemy(int index,int blong)
    {
        Vector3 creatposition = new Vector3(((float)rd.NextDouble() - 0.5f) * 77, ((float)rd.NextDouble() - 0.5f) * 43, 0);
        enemys[index] = Instantiate(enemy_p, creatposition, transform.rotation) as GameObject;
        float r, g, b;
        r = (float)rd.NextDouble();
        g = (float)rd.NextDouble();
        b = (float)rd.NextDouble();
        enemyscolor[index] = new Color(r, g, b);
        enemys[index].GetComponent<Enemy>().r = r;
        enemys[index].GetComponent<Enemy>().g = g;
        enemys[index].GetComponent<Enemy>().b = b;
        enemys[index].transform.Find("Head").GetComponent<SpriteRenderer>().color = enemyscolor[index];
        enemys[index].GetComponent<Enemy>().who = index;
        enemys[index].transform.eulerAngles = new Vector3(0, 0, rd.Next(1,360));
        enemys[index].GetComponent<Enemy>().rd = new System.Random(index);
        enemys[index].GetComponent<Enemy>().gamesystem = gameObject;
        enemys[index].GetComponent<Enemy>().bodys = new GameObject[3000];
        enemys[index].GetComponent<Enemy>().Changelong(blong);

    }
    void Start()
    {
        longlist = new List<node>();
        rd = new System.Random();
        enemys = new GameObject[400];
        enemyscolor = new Color[400];
        for (int i = 0; i < enemynums; i++)
        {
            creatoneenemy(i, rd.Next(30, 150));
        }
        for (int i = 0; i < 120; i++)//120
        {
            creatonefood();
        }
    }
	
	// Update is called once per frame
	void Update()
    {
        
        longlist.Clear();
        node now;
        for (int i = 0; i < enemynums; i++)
        {
            if (enemys[i])
            {
                now = new node(i, enemys[i].GetComponent<Enemy>().body_long);
                longlist.Add(now);
            }
        }
        if (player) playerlength = player.GetComponent<Player>().body_long;
        now = new node(-1, playerlength);
        longlist.Add(now);
        longlist.Sort((left, right) =>
        {
            if (left.bodylong < right.bodylong)
                return 1;
            else if (left.bodylong == right.bodylong)
                return 0;
            else
                return -1;
        });
        string gradetext = "";
        int gradenums = longlist.Count < 7 ? longlist.Count : 7;
        for(int i=0; i< gradenums; i++)
        {
            if (longlist[i].index == -1)
            {
                gradetext += "<color=#5977B2FF>" + (i + 1).ToString() + ".Player" +  "  " + longlist[i].bodylong.ToString() + "</color>\n";
            }else
            {
                gradetext += "<color=#"+colortostring(enemyscolor[longlist[i].index])+">"+(i + 1).ToString() + ".enemy" + longlist[i].index.ToString() + "  " + longlist[i].bodylong.ToString() + "</color>\n";
            }
        }
        canvas.transform.Find("gradedata").GetComponent<Text>().text = gradetext;
        canvas.transform.Find("playersdata").GetComponent<Text>().text = "Your kill:" + playerkills;
        
    }
    string colortostring(Color cl)
    {
        int rr = (int)(cl.r * 255);
        int gg = (int)(cl.g * 255);
        int bb = (int)(cl.b * 255);
        int aa = (int)(cl.a * 255);
        return rr.ToString("X2") + gg.ToString("X2") + bb.ToString("X2") + aa.ToString("X2");
    }
}
