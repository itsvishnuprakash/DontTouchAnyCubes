using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PingpongObstacleMovement : MonoBehaviour
{
    public float movespeed=2f;
    bool pingpongStart=false;
    float leftEnd=-2.7f;
    float xi;
    private void Start() 
    {
        pingpongStart=false;
        xi=transform.position.x;
    }
    void Update()
    {
        if(!pingpongStart)
        {
            if(transform.position.x<=leftEnd)
            {
                pingpongStart=true;
            }
            transform.Translate(new Vector3(-1f,0f,0f)*movespeed*Time.deltaTime);
        }
        else
        {
            transform.position= new Vector3(Mathf.PingPong(Time.time+xi,4.9f)-2.7f,transform.position.y,transform.position.z);
        }
        
    }
}
