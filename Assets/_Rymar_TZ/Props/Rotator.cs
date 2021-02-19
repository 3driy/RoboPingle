using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    [SerializeField]
    Vector3 rotateSpeed;
    [SerializeField] float speed;
    [SerializeField] float height;
    float offseter;
    Vector3 startPos;

    // Update is called once per frame
    private void Start()
    {
        startPos = transform.position;
        offseter = Random.Range(0f, 1f);
    }
    void Update()
    {
        transform.Rotate(rotateSpeed, Space.World);
        offseter += Time.deltaTime;
        if (offseter > 10) { offseter -= 10f; }
        float offset = Mathf.Sin(offseter * speed) * height;
        
        transform.position = new Vector3(startPos.x,startPos.y + offset,startPos.z);


    }
}
