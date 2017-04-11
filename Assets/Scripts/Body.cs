using UnityEngine;
using System.Collections;

public class Body : MonoBehaviour {
    public bool init = false;
    public Head head;
    public GameObject last;
    public float v;
    public Queue drcts;
    public float delaydistance;
    public bool start = false;
    public bool end = false;
    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        /*if (init) drcts.Enqueue(head.transform.position);
        if (start)
        {

            if (delaydistance > 0)
            {
                delaydistance -= Time.deltaTime * v;
            }
            else
            {
                end = true;
                Vector3 transto = (((Vector3)drcts.Dequeue()) - transform.position).normalized;
                float drctangel = Vector3.Angle(transto, Vector3.right) * (transto.y > 0 ? 1 : -1);
                transform.eulerAngles = new Vector3(0, 0, drctangel);
                transform.Translate(Vector3.right * Time.deltaTime * head.v);
                //print(drcts.Count);

            }
        }
        else if (last != null && last.end == true)
        {
            start = true;
        }*/
        if (init)
        {
            float angel = last.transform.eulerAngles.z;
            Vector3 delayVector = new Vector3(Mathf.Cos(Mathf.PI / 180 * angel), Mathf.Sin(Mathf.PI / 180 * angel), 0);
            Vector3 transTo = (last.gameObject.transform.position - delayVector * delaydistance - transform.position).normalized;
            float drctAngel = Vector3.Angle(transTo, Vector3.right) * (transTo.y > 0 ? 1 : -1);
            transform.eulerAngles = new Vector3(0, 0, drctAngel);
            transform.Translate(Vector3.right * Time.deltaTime * head.v);
        }

    }
}
