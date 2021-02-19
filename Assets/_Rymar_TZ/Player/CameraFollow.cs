using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    Transform player;
    [SerializeField] float smoothSpeed = 0.125f;
    Transform mainCamera;
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = transform.GetChild(0).transform;
        //Camera.main.depthTextureMode = DepthTextureMode.Depth;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 toPos = player.position;
        Vector3 smoothedPos = Vector3.Lerp(transform.position, toPos, smoothSpeed * Time.deltaTime);
        transform.position = smoothedPos;
        mainCamera.LookAt(player.position);
    }
}
