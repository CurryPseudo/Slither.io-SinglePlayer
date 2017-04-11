using UnityEngine;
using System.Collections;

public class Head : MonoBehaviour {
    protected float speedUpDelayTime=0;
    protected bool ifSpeedUp;
    public float r, g, b;
    public GameObject gamesystem;
    public int who;
    public Sprite bg;
    public GameObject littleFood_p;
    public GameObject food_p;
    public GameObject body_p;
    public GameObject[] bodys;
    public float v;
    public float angelv;
    public Vector3 drct;
    public float drctangel;
    public int body_count;
    public int body_long;
    public System.Random rd;
    public float nowheretime;
    protected const float settime = 1;
    public int maxbodycount;
    Vector3 nowhere;
    public float noangle = 0;
    // public bool dostart=false;
    // Use this for initialization
    protected bool bodyincrease()
    {
        if (body_count >= maxbodycount - 5) return false;
        body_count++;
        GameObject bodylast = body_count > 1 ? bodys[body_count - 2] : this.gameObject;
        float angel = bodylast.transform.eulerAngles.z;
        Vector3 delayVector = new Vector3(Mathf.Cos(Mathf.PI / 180 * angel), Mathf.Sin(Mathf.PI / 180 * angel), 0);
        GameObject bodynow = Instantiate(body_p, bodylast.transform.position-delayVector*0.7f, bodylast.transform.rotation) as GameObject;
        bodynow.GetComponent<SpriteRenderer>().color = transform.Find("Head").GetComponent<SpriteRenderer>().color;
        bodys[body_count - 1] = bodynow;
        Body body_cs = bodynow.GetComponent<Body>();
        body_cs.head = this;
        body_cs.v = this.v;
        body_cs.delaydistance = 0.5f;
        SpriteRenderer body_SR = bodynow.GetComponent<SpriteRenderer>();
        body_SR.sortingOrder = -body_count;
        body_cs.drcts = body_count > 1 ? new Queue(bodylast.GetComponent<Body>().drcts.ToArray()) : new Queue();
        body_cs.last = bodylast;
        body_cs.init = true;
        return true;
        //body_cs.start = true;
    }
    protected void bodydecline(bool dead)
    {
        if (body_count > 0)
        {
            if (dead)
            {
                GameObject leavefood = Instantiate(food_p, bodys[body_count - 1].transform.position, new Quaternion()) as GameObject;
                leavefood.GetComponent<SpriteRenderer>().color = bodys[body_count - 1].GetComponent<SpriteRenderer>().color;
            }
            Destroy(bodys[body_count - 1]);
            body_count--;
        }
    }
    public void Changelong(int b_long)
    {
        body_long = b_long;
        int nextcount = (int)body_long / 6;
        if (nextcount > body_count)
        {
            while (nextcount > body_count)
            {
                if (!bodyincrease()) return;
            }
        }
        else if (nextcount < body_count)
        {
            while (nextcount < body_count)
            {
                bodydecline(false);
            }
        }
    }
    protected void headmove(float angel)
    {
        drctangel = angel;
        /*if (drct.y < 0)
        {
            drctangel = 360 - drctangel;
        }*/
        drctangel = drctangel - transform.eulerAngles.z;
        //if (drctangel > 180) drctangel -= 360; else if (drctangel < -180) drctangel += 360;
        while (drctangel > 180) drctangel -= 360;
        while (drctangel < -180) drctangel += 360;
        float maxangel = 30;
        if ((drctangel > maxangel && drctangel <= 180))
        {
            drctangel = maxangel;
        }
        else if ((drctangel < -maxangel && drctangel >= -180))
        {
            drctangel = -maxangel;
        }
        drctangel *= angelv;
        transform.Rotate(0, 0, drctangel * Time.deltaTime);
        transform.Translate(Vector3.right * v * Time.deltaTime);
    }
    protected void headmove(Vector3 where)
    {

        Vector3 to = where;
        to -= transform.position;
        to = new Vector3(to.x, to.y, 0);
        drct = (to).normalized;

        drctangel = Vector3.Angle(drct, Vector3.right);
        if (drct.y < 0)
        {
            drctangel = 360 - drctangel;
        }
        drctangel = drctangel - transform.eulerAngles.z;
        if (drctangel > 180) drctangel -= 360; else if (drctangel < -180) drctangel += 360;
        float maxangel = 30;
        if ((drctangel > maxangel && drctangel <= 180))
        {
            drctangel = maxangel;
        }
        else if ((drctangel < -maxangel && drctangel >= -180))
        {
            drctangel = -maxangel;
        }
        drctangel *= angelv;
        transform.Rotate(0, 0, drctangel * Time.deltaTime);
        transform.Translate(Vector3.right * v * Time.deltaTime);
    }
    // Update is called once per frame
    public void speedUp()
    {
        ifSpeedUp = true;
        v = 8;
        gameObject.transform.FindChild("Head").GetComponent<SpriteRenderer>().color = new Color(r * 1.5f, g * 1.5f, b * 1.5f);
        for(int i = 0; i < body_count; i++)
        {
            bodys[i].gameObject.GetComponent<SpriteRenderer>().color = new Color(r + 0.2f, g + 0.2f, b + 0.2f);
        }
    }
    public void speedDown() {
        ifSpeedUp = false;
        v = 4;
        gameObject.transform.FindChild("Head").GetComponent<SpriteRenderer>().color = new Color(r , g , b);
        for (int i = 0; i < body_count; i++)
        {
            bodys[i].gameObject. GetComponent<SpriteRenderer>().color = new Color(r, g, b);
        }
    }
    protected void dead()
    {
        while (body_count > 0)
        {
            bodydecline(true);
        }
        gamesystem.GetComponent<GameSystem>().rebuildenemy(who, 30);
    }
    protected void speedUpBodyDecline()
    {
        if (ifSpeedUp)
        {
            if (body_long > 30)
            {
                if (speedUpDelayTime > 0)
                {
                    speedUpDelayTime -= Time.deltaTime;
                }else
                {
                    speedUpDelayTime = 0.5f;
                    GameObject leavefood = Instantiate(littleFood_p, bodys[body_count - 1].transform.position, new Quaternion()) as GameObject;
                    leavefood.GetComponent<SpriteRenderer>().color = bodys[body_count - 1].GetComponent<SpriteRenderer>().color;
                    leavefood.GetComponent<Food>().bodyLeftFood = true;
                    Changelong(body_long -= 1);
                }
            }
            else
            {
                speedDown();
            }
        }
    }
}
