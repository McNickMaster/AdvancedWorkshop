using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour
{
    public MonoBehaviour scriptToActivate;
    public Sprite[] sprites;

    public SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Awake()
    {
        scriptToActivate.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer.Equals(10))
        {
            scriptToActivate.enabled = true;
            spriteRenderer.sprite = sprites[1];
        }
    }
}
