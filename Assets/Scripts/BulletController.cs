using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    Rigidbody2D rbody;

    public AudioClip hitClip;//命中音效
    // Start is called before the first frame update
    void Awake()
    {
        rbody = GetComponent<Rigidbody2D>();
        Destroy(this.gameObject,2f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Move(Vector2 moveDirection,float moveForce){
    rbody.AddForce(moveDirection*moveForce);
    }

    void OnCollisionEnter2D(Collision2D other){

       EnemyController ec = other.gameObject.GetComponent<EnemyController>();
       if(ec!=null)
       {
          ec.Fixed();    
       }
       Audiomanagement.instance.AudioPlay(hitClip);
       Destroy(this.gameObject);
    }
}
