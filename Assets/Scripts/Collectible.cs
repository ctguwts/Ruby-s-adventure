using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    public AudioClip CollectClip;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        PlayerController pc = other.GetComponent<PlayerController>();
        if(pc!=null)
        {
            if(pc.MyCurrentHealth < pc.MyMaxHealth)
            pc.ChangeHealth(1);
            Destroy(this.gameObject);
            Debug.Log("玩家碰到了草莓!");
            Audiomanagement.instance.AudioPlay(CollectClip);
        }
    }
}
