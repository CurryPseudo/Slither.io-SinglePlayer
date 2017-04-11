using UnityEngine;
using System.Collections;

public class Enemy : Head {
    public BehaviorDesigner.Runtime.BehaviorTree aiTree;
    void Start()
    {
        nowheretime = settime;
        //rd = new System.Random();
        //bodys = new GameObject[3000];

    }
    /*void Ai()
    {

        int[] k = new int[6];
        float[] mindistance = { 2f, 2.5f, 10, 10, 10, 10 };
        Collider2D[] objects = Physics2D.OverlapCircleAll(transform.position, 10);
        //print(foods.Length);
        for (int i = 0; i < 6; i++)
        {
            k[i] = -1;
        }
        for (int i = 0; i < objects.Length; i++)
        {
            GameObject one = objects[i].gameObject;
            //print(one
            if ((one.tag == "enemybody" && one.GetComponent<Body>().head != this) || one.tag == "body")
            {
                float d = Vector3.Distance(transform.position, one.transform.position);
                if (d < mindistance[0])
                {

                    k[0] = i;
                    mindistance[0] = d;
                }
            }
            else
           if (one.tag == "bg")
            {
                k[1] = i;
            }
            else if (one.tag == "head")
            {
                if (one != gameObject)
                {
                    float d = Vector3.Distance(transform.position, one.transform.position);
                    float toangenl = one.transform.eulerAngles.z;
                    float vlength = Vector3.Project(transform.position - one.transform.position, new Vector3(Mathf.Cos(toangenl / 360 * Mathf.PI * 2), Mathf.Sin(toangenl / 360 * Mathf.PI * 2), 0)).magnitude;
                    //print(vlength);
                    if (vlength > 3 && vlength < 4 && d < mindistance[2])
                    {
                        k[2] = i;
                        mindistance[2] = d;
                    }
                }
            }
            if (one.tag == "bigfoodself")
            {
                float d = Vector3.Distance(transform.position, one.transform.position);
                if (d < mindistance[3])
                {

                    k[3] = i;
                    mindistance[3] = d;
                }
            }
            else if (one.tag == "foodself")
            {
                float d = Vector3.Distance(transform.position, one.transform.position);
                if (d < mindistance[4])
                {
                    k[4] = i;
                    mindistance[4] = d;
                }
            }
        }

        if (k[0] != -1)
        {
            Vector3 dt = objects[k[0]].gameObject.transform.position - transform.position;
            //print(mindistance);
            noangle = Vector3.Angle(dt, Vector3.right) * (dt.y > 0 ? 1 : -1) + 180;
            //print(k);
            speedDown();
            return;
        }
        if (k[1] != -1)
        {
            if (transform.position.x > 40 - mindistance[1] || transform.position.x < -40 + mindistance[1])
            {
                noangle = transform.position.x > 0 ? 180 : 0;
                return;
            }
            if (transform.position.y > 22.5 - mindistance[1] || transform.position.y < -22.5 + mindistance[1])
            {
                noangle = transform.position.y > 0 ? -90 : 90;
                return;
            }
            speedDown();
        }
        if (k[2] != -1)
        {
            //print(mindistance);
            noangle = objects[k[2]].gameObject.transform.eulerAngles.z;
            float anotherangel = Vector3.Angle((transform.position - objects[k[2]].gameObject.transform.position), Vector3.right) * ((transform.position - objects[k[2]].gameObject.transform.position).y > 0 ? 1 : -1);
            anotherangel -= noangle;
            if (Mathf.Sin(anotherangel / 360 * 2 * Mathf.PI) > 0)
            {
                noangle -= 90;
            }
            else
            {
                noangle += 90;
            }
            speedUp();
            return;
        }
        if (k[3] != -1)
        {
            Vector3 dt = objects[k[3]].gameObject.transform.position - transform.position;
            //print(mindistance);
            noangle = Vector3.Angle(dt, Vector3.right) * (dt.y > 0 ? 1 : -1);
            //print(k);
            speedUp();
            return;
        }
        if (k[4] != -1)
        {
            Vector3 dt = objects[k[4]].gameObject.transform.position - transform.position;
            //print(mindistance);
            noangle = Vector3.Angle(dt, Vector3.right) * (dt.y > 0 ? 1 : -1);
            //print(k);
            speedDown();
            return;
        }
    }
    */
    void Update()
    {
        noangle = (float)aiTree.GetVariable("moveAngle").GetValue();
        if ((bool)aiTree.GetVariable("speedUp").GetValue())
        {
            speedUp();
        }else
        {
            speedDown();
        }
        speedUpBodyDecline();
        headmove((float)aiTree.GetVariable("moveAngle").GetValue());
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "food")
        {
            other.gameObject.GetComponent<Food>().flag = true;
            other.gameObject.GetComponent<Food>().eaterposition = transform;
            Changelong(body_long + 1);
        }

        if (other.tag == "body")
        {

            gamesystem.GetComponent<GameSystem>().playerkills++;
            dead();
        }
        if (other.tag == "enemybody")
        {
            if (other.gameObject.GetComponent<Body>().head != this)
            {
                dead();
            }
        }
        if (other.tag == "bigfood")
        {
            other.gameObject.GetComponent<bigfood>().eaterposition = transform;
            other.gameObject.GetComponent<bigfood>().flag = true;
            Changelong(body_long + 3);
        }
        if (other.tag == "bg")
        {
            dead();
        }
    }
}