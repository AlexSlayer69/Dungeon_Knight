using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float speed;
    private float pos_x;
    private float pos_y;
    private Vector3 velocity = Vector3.zero;
    
    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.SmoothDamp(transform.position,new Vector3(pos_x,transform.position.y,transform.position.z),ref velocity, speed);
    }

    public void Change_Rooms(Transform new_room){
        pos_x = new_room.position.x;
    }
}
