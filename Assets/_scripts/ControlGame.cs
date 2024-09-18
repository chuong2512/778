using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlGame : MonoBehaviour
{
    public GameObject[] levels;
    // Start is called before the first frame update
    void Start()
    {
        if (GameManager.instance.getlevel() >= 10)
        {
            levels[Random.Range(0, 10)].SetActive(true);
        }
        else
        {
            levels[GameManager.instance.getlevel()].SetActive(true);
        }
    }

    
}
