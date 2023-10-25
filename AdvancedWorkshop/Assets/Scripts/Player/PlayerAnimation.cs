using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    public static PlayerAnimation instance;

    public SpriteRenderer sprite;

    private bool lookingLeft = false;

    public float squashStretchOffset = 0.2f;
    private Vector3 targetScale = Vector3.one, currentScale = Vector3.one;
    private int playerDir = 1;
    public float squashMod = 20;

    private void Awake()
    {
        instance = this;

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(PlayerMovement.instance.rb.velocity.x < 0 && !lookingLeft)
        {
            lookingLeft = true;
            playerDir = -1;
        }

        if (PlayerMovement.instance.rb.velocity.x > 0 && lookingLeft)
        {
            lookingLeft = false;
            playerDir = 1;
        }

        UpdateSquashStretch();


        if(Input.GetKeyDown(KeyCode.F))
        {
            Squash();
        } else if (Input.GetKeyDown(KeyCode.G))
        {
            Stretch();
        } else if (Input.GetKeyDown(KeyCode.H))
        {
            ResetSS();
        }

    }

    void UpdateSquashStretch()
    {
        currentScale = Vector3.Lerp(currentScale, targetScale, Time.deltaTime * squashMod);
        sprite.transform.localScale = currentScale;

        sprite.transform.localScale = new Vector3(playerDir * sprite.transform.localScale.x, sprite.transform.localScale.y, sprite.transform.localScale.z);
    }


    public void Squash()
    {
        targetScale = new Vector3(1 + squashStretchOffset, 1 - squashStretchOffset, 1);
    }

    public void Stretch()
    {
        targetScale = new Vector3(1 - squashStretchOffset, 1 + squashStretchOffset, 1);
    }

    public void ResetSS()
    {
        targetScale = Vector3.one;
    }






}
