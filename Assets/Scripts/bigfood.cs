using UnityEngine;
using System.Collections;

public class bigfood : MonoBehaviour {
    public bool flag = false;
    private float delaytime;
    public Transform eaterposition;
    public float v;
    private Vector3 moveto;
    // Use this for initialization
    void Start()
    {
        delaytime = 1.45f / v;
    }

    // Update is called once per frame
    void Update()
    {
        if (flag)
        {
            if (delaytime > 0)
            {
                delaytime -= Time.deltaTime;
                if (eaterposition != null) moveto = (eaterposition.position - transform.position).normalized * v * Time.deltaTime;
                transform.Translate(moveto);
            }
            else
            {
                //GameObject.Find("gamesystem").GetComponent<gamesystem>().creatonefood();
                Destroy(gameObject);
                //GameObject.Find("gamesystem").GetComponent<gamesystem>().food_count--;
            }
        }
    }
}
