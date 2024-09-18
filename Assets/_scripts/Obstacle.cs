using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using MoreMountains.NiceVibrations;

public class Obstacle : MonoBehaviour
{
    Sequence sequence;
    public Vector3 pos;
    public float durations;
    public GameObject brush_pref;
    public float power;

    // Start is called before the first frame update
    void Start()
    {
        transform.DOLocalMove(pos, durations).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear);

        //sequence = DOTween.Sequence();
        //float pos_y_old = transform.localPosition.y;
        //float pos_y_new = transform.localPosition.y + .5f;

        //sequence

        //            .Append(transform.DOLocalMoveY(pos_y_new, time).SetEase(ease_linear))
        //            .Append(transform.DOLocalMoveY(pos_y_old, time).SetEase(ease_bounce));
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.back * Time.smoothDeltaTime * 250);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("brush"))
        {

            UiManager.instance._vibrate();


            Controller_Brush.instance.decrease_brush();
            other.GetComponent<BrushGroup>().clean_brush();

            Vector3 tmp = transform.position;
            tmp.z += 4f;

            GameObject gm = Instantiate(brush_pref, tmp, brush_pref.transform.rotation);

            Vector3 shoot = new Vector3(0f, Random.Range(2, 5), Random.Range(2, 6));
            gm.GetComponent<Rigidbody>().AddForce(shoot * power, ForceMode.Impulse);
        }
    }
}
