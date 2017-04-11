using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class click : MonoBehaviour {
    public void onclick()
    {
        SceneManager.LoadScene(1);
    }
}
