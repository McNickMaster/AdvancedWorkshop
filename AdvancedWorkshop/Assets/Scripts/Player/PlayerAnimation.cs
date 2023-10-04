using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    public SpriteRenderer sprite;

    private bool lookingLeft = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(PlayerMovement.instance.rb.velocity.x < 0)
        {
            lookingLeft = true;
            sprite.transform.localScale = new Vector3(-1, 1, 1);
        }

        if (PlayerMovement.instance.rb.velocity.x > 0)
        {
            lookingLeft = false; 
            sprite.transform.localScale = new Vector3(1, 1, 1);
        }

        

    }





}
