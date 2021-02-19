using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveControll : MonoBehaviour
{
    [SerializeField] FloatingJoystick joystick;
    Vector2 inputSpeed;
    //CharacterController controller;
    [SerializeField] float moveSpeed = 3;
    [SerializeField] float tracksSpeed = 1;
    [SerializeField] Transform lookAtObj;
    [SerializeField] Transform playerHolder;
    [SerializeField] GameObject playerModel;
    Animator animator;
    Material trackMat;
    float trackOffset;
    Rigidbody body;
    [HideInInspector] public float inputMagnitude;
    [SerializeField]
    float waterheight = 0.2f;
    float speedMul = 1f;
    //Vector3 surfaceNormal;
    // Start is called before the first frame update
    void Start()
    {
        //controller = GetComponent<CharacterController>();
        body = GetComponent<Rigidbody>();
        animator = playerModel.GetComponent<Animator>();
        trackMat = playerModel.transform.GetChild(1).gameObject.GetComponent<Renderer>().materials[1];
    }

    // Update is called once per frame
    void Update()
    {
        inputSpeed = new Vector2(joystick.Horizontal, joystick.Vertical);
        inputSpeed = Vector2.ClampMagnitude(inputSpeed, 1f);
        inputMagnitude = inputSpeed.magnitude;
        if (inputSpeed.x > 0.1f | inputSpeed.y > 0.1f | inputSpeed.x < -0.1f | inputSpeed.y < -0.1f)
        {
            TrackOffset();
            PlayerMove();

        }
        else
        {
            animator.SetBool("Moving", false);
        }
        if (transform.position.y <= waterheight)
        {
            speedMul = 0.7f;
        }
        else {
            speedMul = 1f;
        }
    }


    void PlayerMove() {

        Vector3 moveOrient = new Vector3(inputSpeed.x,0, inputSpeed.y);
        lookAtObj.localPosition = moveOrient;

        LayerMask layerMask = ~LayerMask.NameToLayer("Environment");
        RaycastHit hit;
        if (Physics.Raycast(transform.position + Vector3.up, Vector3.down, out hit, 2f, layerMask))
        {

            Quaternion rotation = Quaternion.FromToRotation(Vector3.up, hit.normal);
            playerHolder.transform.rotation = rotation;
        }

        playerModel.transform.LookAt(lookAtObj.position);
        animator.SetBool("Moving",true);
        animator.SetFloat("MovingSpeedMul", inputMagnitude);
        trackMat.SetFloat("_Offset",trackOffset);
        body.MovePosition(transform.position + moveOrient * moveSpeed * speedMul);
        //controller.SimpleMove(moveOrient * moveSpeed);

    }

    void TrackOffset() {

        if (trackOffset > 1) {
            trackOffset -= 1;
        }

        trackOffset += Time.deltaTime * inputMagnitude * tracksSpeed;
    }


}
