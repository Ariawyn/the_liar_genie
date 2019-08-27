using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerDriver : MonoBehaviour
{

    private float curSpeed;

    private Rigidbody body;
    [SerializeField]
    private float maxSpeed = 1;
    [SerializeField]
    private AnimationCurve acceleration;

    private Vector2 input;


    // Start is called before the first frame update
    void Start()
    {
        this.body = this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //this.transform.position += new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")) * speed * Time.deltaTime;
        input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
    }

    private void FixedUpdate()
    {
        float speedPercent = acceleration.Evaluate(input.magnitude);

        this.body.MovePosition(this.body.position + new Vector3(input.x,0,input.y).normalized * (speedPercent * maxSpeed) * Time.fixedDeltaTime);
    }
}
