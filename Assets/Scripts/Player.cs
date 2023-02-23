using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    private bool _thrusting;
    public float thrustSpeed = 1.0f;
    private float _turndirection;
    public float turnSpeed = 1.0f;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        _thrusting = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow);
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            _turndirection = 1.0f;
        }
        else
        {
            _turndirection =
                Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow) ? -1.0f : 0.0f;
        }
    }

    /// <summary>
    /// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
    /// </summary>
    void FixedUpdate()
    {
        if (_thrusting)
        {
            _rigidbody.AddForce(this.transform.up * this.thrustSpeed);
        }
        if (_turndirection != 0)
        {
            _rigidbody.AddTorque(_turndirection * this.turnSpeed);
        }
    }
}
