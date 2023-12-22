using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Basic2DPlatformController : MonoBehaviour
{
    public float walkingAcceleration,desiredHeightFromGround, groundDragCoef, airDragCoef, maxStep, gravity;
    private Vector3 velocity = Vector3.zero;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    bool isGrounded()
    {
        // Bit shift the index of the layer (3) to get a bit mask
        int layerMask = 1 << 3;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector3.down, 10, layerMask);
        // Does the ray intersect any objects excluding the player layer
        if (hit.collider != null)
        {
            Debug.DrawRay(transform.position, Vector3.down * hit.distance, Color.yellow);
            float step = desiredHeightFromGround - hit.distance;
            bool grounded = step >= 0f;
            if (grounded && step < maxStep) transform.Translate(Vector3.up*step);
            return grounded;
        }
        return false;
    }

    // Update is called once per frame
    void Update()
    {
        velocity -= velocity * (airDragCoef * Time.deltaTime);
        if (!isGrounded())
        {
            velocity += Vector3.down * gravity * Time.deltaTime;
        }
        else
        {
            velocity.y = 0;
            velocity -= velocity * (groundDragCoef * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D))
        {
            velocity += Vector3.right * walkingAcceleration * Time.deltaTime;
        }
        if(Input.GetKey(KeyCode.A))
        {
            velocity += Vector3.left * walkingAcceleration * Time.deltaTime;
        }

        transform.Translate(velocity*Time.deltaTime);
    }
}
