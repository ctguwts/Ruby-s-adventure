using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
     public float speed = 3;

     public float changeDirectionTime = 2f;//改变方向的时间

     private float changeTimer;//改变方向的计时器

     public bool isVertical;

     public Vector2 moveDirection;//移动方向

     private Rigidbody2D rbody;

     private Animator anim;

     private bool isFixed;

     public AudioClip fixedClip;
    // Start is called before the first frame update
    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();

        anim = GetComponent<Animator>();

        moveDirection = isVertical ? Vector2.up : Vector2.right;

        changeTimer = changeDirectionTime;

        isFixed = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(isFixed) return;
        changeTimer -= Time.deltaTime;   
        
        if(changeTimer<0)
        {
        moveDirection *= -1;
        changeTimer = changeDirectionTime;
        }


        Vector2 position = rbody.position;

        position.x += moveDirection.x*speed*Time.deltaTime;

        position.y += moveDirection.y*speed*Time.deltaTime;

        rbody.MovePosition(position);

        anim.SetFloat("moveX",moveDirection.x);
        anim.SetFloat("moveY",moveDirection.y);
    }
    void OnCollisionEnter2D(Collision2D other){
        PlayerController pc = other.gameObject.GetComponent<PlayerController>();
        if(pc != null){
            pc.ChangeHealth(-1);
        }
    }

    public void Fixed() {
        Audiomanagement.instance.AudioPlay(fixedClip);
        isFixed = true;
        rbody.simulated = false;
        anim.SetTrigger("fixed");
    }
}
