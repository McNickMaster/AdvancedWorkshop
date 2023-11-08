using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlayerAtPoint : MonoBehaviour
{
    public Transform point;

    public bool _OnAwake = true;

    private void Awake()
    {
        if(_OnAwake)
        {
            MovePlayer();
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void MovePlayer()
    {
        PlayerMovement.instance.transform.position = point.position;
    }
}
