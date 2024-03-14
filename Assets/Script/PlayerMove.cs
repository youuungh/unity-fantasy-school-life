using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public static PlayerMove instance;
    public float moveSpeed;
    public UIManager UIManager;
    Rigidbody2D rb;
    Animator anim;
    float h;
    float v;
    bool isHorizonMove;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        h = UIManager.isAct ? 0 : Input.GetAxisRaw("Horizontal");
        v = UIManager.isAct ? 0 : Input.GetAxisRaw("Vertical");

        bool hDown = !UIManager.isAct && Input.GetButton("Horizontal");
        bool vDown = !UIManager.isAct && Input.GetButton("Vertical");
        bool hUp = !UIManager.isAct && Input.GetButtonUp("Horizontal");
        bool vUp = !UIManager.isAct && Input.GetButtonUp("Vertical");

        if (hDown || vUp)
            isHorizonMove = true;
        else if (vDown || hUp)
            isHorizonMove = false;

        // 애니메이션 
        if (anim.GetInteger("hAxisRaw") != h)
        {
            anim.SetBool("isChange", true);
            anim.SetInteger("hAxisRaw", (int)h);
        }
        else if (anim.GetInteger("vAxisRaw") != v)
        {
            anim.SetBool("isChange", true);
            anim.SetInteger("vAxisRaw", (int)v);
        }
        else
            anim.SetBool("isChange", false);
    }

    void FixedUpdate()
    {
        Vector2 movePos = isHorizonMove ? new Vector2(h, 0) : new Vector2(0, v);
        rb.velocity = movePos * moveSpeed;
    }
}
