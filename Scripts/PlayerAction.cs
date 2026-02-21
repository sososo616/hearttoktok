using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : MonoBehaviour
{   
    public GameManager manager; // GameManager 참조

    float horizontal;
    float vertical;
    bool isHorizonMove;
    Vector3 dirVec;
    GameObject scanObject;
    Rigidbody2D rigid;
    Animator anim;
    public float Speed = 5f;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        // Move Value
        horizontal = manager.isAction ? 0 : Input.GetAxisRaw("Horizontal");
        vertical = manager.isAction ? 0 : Input.GetAxisRaw("Vertical");

        // Check Button Down&Up
        bool hDown = manager.isAction ? false : Input.GetButtonDown("Horizontal");
        bool vDown = manager.isAction ? false : Input.GetButtonDown("Vertical");
        bool hUp = manager.isAction ? false : Input.GetButtonUp("Horizontal");
        bool vUp = manager.isAction ? false : Input.GetButtonUp("Vertical");

        // Check Horizontal Move
        if (hDown)
            isHorizonMove = true;
        else if (vDown)
            isHorizonMove = false;
        else if (hUp || vUp)
            isHorizonMove = horizontal != 0; // 'h'를 'horizontal'로 수정

        // Animation
        if (anim.GetInteger("hAxisRaw") != (int)horizontal)
        {
            anim.SetInteger("hAxisRaw", (int)horizontal);
            anim.SetTrigger("isChange");
        }
        else if (anim.GetInteger("vAxisRaw") != (int)vertical)
        {
            anim.SetInteger("vAxisRaw", (int)vertical);
            anim.SetTrigger("isChange");
        }

        // Direction
        if (vertical > 0)
            dirVec = Vector3.up;
        else if (vertical < 0)
            dirVec = Vector3.down;
        else if (horizontal < 0)
            dirVec = Vector3.left;
        else if (horizontal > 0)
            dirVec = Vector3.right;

        // Scan Object
        if (Input.GetButtonDown("Jump") && scanObject != null)
            manager.Action(scanObject);
    }

    void FixedUpdate()
    {   
        // Move 방향에 따라 이동 벡터 설정
        Vector2 moveVec = isHorizonMove ? new Vector2(horizontal, 0) : new Vector2(0, vertical);
        // 속도 설정
        rigid.velocity = moveVec * Speed;

        // Ray
        Debug.DrawRay(rigid.position, dirVec * 0.7f, new Color(0, 1, 0));
        
        // Raycast
        RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, dirVec, 0.7f, LayerMask.GetMask("Object"));

        // Raycast 결과 처리
        if (rayHit.collider != null)
        {
            scanObject = rayHit.collider.gameObject; // 스캔된 오브젝트 설정
        }
        else
        {
            scanObject = null; // 스캔된 오브젝트가 없으면 null
        }
    }
}
