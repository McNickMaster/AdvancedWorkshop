using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveSign : MonoBehaviour
{
    [SerializeField]
    Canvas messageCanvas;
    // Start is called before the first frame update
    void Start()
    {
        messageCanvas.enabled = false;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "PlayerSprite")
        {
            TurnOnMessage();
        }
    }

    private void TurnOnMessage()
    {
        messageCanvas.enabled = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        TurnOffMessage();
    }

    private void TurnOffMessage()
    {
        messageCanvas.enabled = false;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
