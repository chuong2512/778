using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrushObject : MonoBehaviour
{
    public GameObject lipstick , mascara;
    

    // Start is called before the first frame update
    void Start()
    {
        mascara = transform.GetChild(1).gameObject;
        lipstick = transform.GetChild(2).gameObject;
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("brush"))
        {
            UiManager.instance._vibrate();
            Destroy(gameObject);
            Controller_Brush.instance.increase_brush();

            print("add brush");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("brush"))
        {
            UiManager.instance._vibrate();
            Destroy(gameObject);
            Controller_Brush.instance.increase_brush();
            
            print("add brush");
        }
    }

    public void show_mascara()
    {
        mascara.SetActive(true);
    }

    public void show_lipstick()
    {
        lipstick.SetActive(true);
    }
}
