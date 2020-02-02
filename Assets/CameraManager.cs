using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField]
    private TargetJoint2D tJ2D;

    [SerializeField]
    private GameObject player;

    void FixedUpdate(){
        tJ2D.target = new Vector2(player.transform.position.x, Mathf.Clamp(player.transform.position.y, -10, 12));
    }
}
