using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject bulletPrefab;//子弹

    public float speed = 5f;//移动速度

    private int maxHealth = 5;//最大生命值

    private int currentHealth;//当前生命值

    public int MyMaxHealth{ get{return maxHealth;}}

    public int MyCurrentHealth{ get{return currentHealth;}}

    private float invincibleTime = 2f;//无敌时间2秒
    
    private float invincibleTimer;//无敌计时器

    private bool isInvincible;//是否处于无敌状态

   // private GameObject bulletPrefab;//子弹

   public AudioClip hitClip;//受伤音效
   public AudioClip launchClip;//发射齿轮

    Rigidbody2D rbody;

    private Vector2 lookDirection = new Vector2(1,0);//默认朝向

    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        invincibleTimer = 0;
        currentHealth =2;
        rbody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        UImanagement.instance.UpdateHealthBar(currentHealth,maxHealth);

    }

    // Update is called once per frame
    void Update()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        Vector2 moveVector = new Vector2(moveX,moveY);
        if(moveVector.x!=0||moveVector.y!=0)
        {
            lookDirection = moveVector;
        }

        anim.SetFloat("Look X",lookDirection.x);
        anim.SetFloat("Look Y",lookDirection.y);
        anim.SetFloat("Speed",moveVector.magnitude);

        Vector2 position = rbody.position;
      //  position.x += moveX*speed*Time.deltaTime;
      //   position.y += moveY*speed*Time.deltaTime;
        position += moveVector*speed*Time.deltaTime;
        rbody.MovePosition(position);


        if(isInvincible){
        invincibleTimer -= Time.deltaTime;
          if(invincibleTimer<0)
          { 
             isInvincible = false;//取消无敌状态
          }
        }

        //==========按下J键进行攻击==========================
        if(Input.GetKeyDown(KeyCode.J)){
          anim.SetTrigger("Launch");
          Audiomanagement.instance.AudioPlay(launchClip);
           GameObject bullet = Instantiate(bulletPrefab,rbody.position, Quaternion.identity);
           BulletController bc = bullet.GetComponent<BulletController>();
           if(bc!=null)
           {
                  bc.Move(lookDirection,300);
           }

        }

    }

    public void ChangeHealth(int amount){
        if(amount<0)
        {
             if(isInvincible == true){
                  return;
             }
             isInvincible = true;
             Audiomanagement.instance.AudioPlay(hitClip);
             anim.SetTrigger("Hit");
             invincibleTimer = invincibleTime;
        }
        currentHealth = Mathf.Clamp(currentHealth + amount,0,maxHealth);
        UImanagement.instance.UpdateHealthBar(currentHealth,maxHealth);
        Debug.Log(currentHealth + "/" + maxHealth);
    }
}
