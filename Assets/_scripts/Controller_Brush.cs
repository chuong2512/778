using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Controller_Brush : MonoBehaviour
{
    public static Controller_Brush instance;

    Rigidbody rb;
    public bool game_run , do_makeUp , is_finish;
    public float speed_player, horizontal_speed;
    Vector2 presspos, actualpos, pressSlowMotion;
    Vector3 mvm;
    Vector3 velocity = Vector3.zero;
    // list brushes
    public List<Transform> list_brushes;
    public float smooth_speed, correct_smooth;
    public int count_brushes , total_brushes;

    // boolean : maskara , lipstick
    public bool mascara_, lipstick_;
    

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
#if UNITY_EDITOR
        horizontal_speed = 1f;
#endif

        rb = GetComponent<Rigidbody>();

        //anim_brushes_position();
    }

    // Update is called once per frame
    void Update()
    {
        player_movements();
        anim_brushes_position();
    }

    void FixedUpdate()
    {
        //player_movements();
    }

    void player_movements()
    {
        if (!do_makeUp && game_run)
        {
            //player move forward
            transform.Translate(transform.forward * speed_player * Time.deltaTime);

            // player move right & left
            if (Input.GetMouseButtonDown(0))
            {
                presspos = Input.mousePosition;
            }

            if (Input.GetMouseButton(0)/* && is_tap*/)
            {
                //actualpos = Input.mousePosition;

                //mvm = new Vector3(Input.GetAxis("Mouse X"), 0f, 0f) * Time.smoothDeltaTime * horizontal_speed;
                //Vector3 tmp = list_brushes[0].position;

                //tmp.x += mvm.x;
                //tmp.x = Mathf.Clamp(tmp.x, -3, 3);
                //list_brushes[0].position = tmp;

                //presspos = actualpos;

                actualpos = Input.mousePosition;

                //mvm = new Vector3(Input.GetAxis("Mouse X"), 0f, 0f) * Time.smoothDeltaTime * horizontal_speed;
                Vector3 tmp = list_brushes[0].position;

                float xdiff = (actualpos.x - presspos.x) * Time.smoothDeltaTime * horizontal_speed;

                tmp.x += xdiff;
                tmp.x = Mathf.Clamp(tmp.x, -3, 3);
                list_brushes[0].position = tmp;

                presspos = actualpos;
            }

            if (Input.GetMouseButtonUp(0))
            {
                mvm = Vector3.zero;
            }

        }
        
    }

    

    void anim_brushes_position()
    {
        for (int i = 1; i < list_brushes.Count; i++)
        {
            Vector3 tmp_pos = list_brushes[i - 1].position;
            tmp_pos.z = list_brushes[i].position.z;

            //print(list_brushes[i - 1].name);
            list_brushes[i].position = Vector3.SmoothDamp(list_brushes[i].position, tmp_pos, ref velocity , smooth_speed + i/correct_smooth);
        }
    }

    public void increase_brush()
    {
        count_brushes++;
        total_brushes++;
        if (count_brushes >= list_brushes.Count) return;

        list_brushes[count_brushes].gameObject.SetActive(true);
        if (mascara_)
        {
            list_brushes[count_brushes].GetChild(1).gameObject.SetActive(true);
            list_brushes[count_brushes].GetComponent<BrushGroup>().has_mascara = true;
        }
            
        if (lipstick_)
        {
            list_brushes[count_brushes].GetChild(2).gameObject.SetActive(true);
            list_brushes[count_brushes].GetComponent<BrushGroup>().has_lipstick = true;
        }
            

        //animate group
        list_brushes[count_brushes].GetComponent<BrushGroup>().animate_group_brush();
    }

    public void decrease_brush()
    {

        if (count_brushes != 0)
        {
            list_brushes[count_brushes].gameObject.SetActive(false);

            //reset to simple
            list_brushes[count_brushes].GetComponent<BrushGroup>().upgrate_to_simple();

            //reset brush
            list_brushes[count_brushes].GetChild(1).gameObject.SetActive(false);
            list_brushes[count_brushes].GetComponent<BrushGroup>().has_mascara = false;

            list_brushes[count_brushes].GetChild(2).gameObject.SetActive(false);
            list_brushes[count_brushes].GetComponent<BrushGroup>().has_lipstick = false;

            count_brushes--;
            
        }
        total_brushes--;
    }

    public void move_forward_in_make_up()
    {
        //Vector3 tmp = transform.position;
        //transform.position += Vector3.forward;
        Vector3 tmp = transform.position;
        tmp.z += 1f ;

        transform.DOMoveZ(tmp.z, .5f);
    }

    public void move_horizontal_in_makeUp(Vector3 new_pos)
    {
        //Vector3 tmp = list_brushes[0].position;
        //tmp.x = new_pos.x;

        list_brushes[0].DOMoveX(new_pos.x, .3f);

        //list_brushes[0].position = tmp;
    }

    public void move_all_to_center_finish_level()
    {
        for (int i = 0; i < list_brushes.Count; i++)
        {
            list_brushes[i].DOMoveX(0f, .4f);
        }
    }
}
