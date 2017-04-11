using UnityEngine;
using System.Collections;

public class Food : MonoBehaviour {
    public bool flag = false;
    private float delaytime;
    public Transform eaterposition;
    public float v;
    private Vector3 moveto;
    public bool bodyLeftFood = false;
	// Use this for initialization
	void Start () {
        delaytime = 1.45f / v;
	}
	
	// Update is called once per frame
	void Update () {
        if (flag)
        {
            if (delaytime > 0)
            {
                delaytime -= Time.deltaTime;
                if(eaterposition!=null)moveto = (eaterposition.position - transform.position).normalized * v * Time.deltaTime;
                transform.Translate(moveto);
            }else
            {
                if (!bodyLeftFood)
                {
                    GameObject.Find("gamesystem").GetComponent<GameSystem>().creatonefood();
                    GameObject.Find("gamesystem").GetComponent<GameSystem>().food_count--;
                }
                Destroy(gameObject);

            }
        }
	}
}
