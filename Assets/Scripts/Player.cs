using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class Player : Head {
    public int kills=0;
    public GameObject mc;
    public GameObject deadui;
   // public bool dostart=false;
    // Use this for initialization
    void Start () {
        deadui.SetActive(false);
        bodys = new GameObject[maxbodycount];
        body_count = 0;
        body_long = 30;
        for(int i = 0; i != 5; i++)
        {
            bodyincrease();
        }
        r = gameObject.transform.FindChild("Head").GetComponent<SpriteRenderer>().color.r;
        g = gameObject.transform.FindChild("Head").GetComponent<SpriteRenderer>().color.g;
        b = gameObject.transform.FindChild("Head").GetComponent<SpriteRenderer>().color.b;
    }
  
    // Update is called once per frame
    void Update()
    {
        mc.transform.position = new Vector3(transform.position.x, transform.position.y, -5);
        Vector3 mP = Input.mousePosition;
        Vector3 tr = Camera.main.WorldToScreenPoint(transform.position);
        mP.z = tr.z;
        mP = Camera.main.ScreenToWorldPoint(mP);
        if (Input.GetMouseButtonDown(1))
        {
            speedUp();
        }
        if (Input.GetMouseButtonUp(1))
        {
            speedDown();
        }
        speedUpBodyDecline();
        headmove(mP);

    }
    void OnTriggerEnter2D (Collider2D other)
    {
        if (other.tag == "food")
        {
            other.gameObject.GetComponent<Food>().flag = true;
            other.gameObject.GetComponent<Food>().eaterposition = transform;
            Changelong(body_long+1);
        }
        if (other.tag == "bigfood")
        {
            other.gameObject.GetComponent<bigfood>().eaterposition = transform;
            other.gameObject.GetComponent<bigfood>().flag = true;
            Changelong(body_long + 3);
        }
        if (other.tag == "enemybody" || other.tag=="bg")
        {
            //if (other.gameObject.GetComponent<enemybody>().head != this)
            //{
                dead();
            //}
        }
    }
    new void dead()
    {
        while (body_count > 0)
        {
            bodydecline(true);
        }
        deadui.SetActive(true);
        deadui.transform.FindChild("Text").GetComponent<Text>().text = "You are dead.Your length is " + body_long;
        Destroy(this.gameObject);
    }
}
