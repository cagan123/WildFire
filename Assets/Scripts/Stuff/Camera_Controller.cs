using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Controller : MonoBehaviour
{
    private float X_pos;
    private float Y_pos;
    void Update()
    {
        X_pos = PlayerManager.instance.player.transform.position.x;
        Y_pos = PlayerManager.instance.player.transform.position.y;
        transform.position = new Vector3(X_pos, Y_pos, transform.position.z);
    }
}
