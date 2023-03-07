using System;
using UnityEngine;

public class Player : MonoBehaviour, ICollidable
{
    public Bullet bulletPrefab;
    private Rigidbody2D _rigidbody;
    private bool _thrusting;

    [SerializeField]
    private float thrustSpeed = 1.0f;

    [SerializeField]
    private GameManager gameManager = GameManager.Instance;

    private float _turndirection;

    [SerializeField]
    private float turnSpeed = 1.0f;

    public event Action<ICollidable> OnCollisionEvent;

    private void Awake()
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
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            _turndirection = -1.0f;
        }
        else
        {
            _turndirection = 0.0f;
        }
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    private void FixedUpdate()
    {
        if (_thrusting)
        {
            _rigidbody.AddForce(transform.up * thrustSpeed);
        }
        if (_turndirection != 0)
        {
            _rigidbody.AddTorque(_turndirection * turnSpeed);
        }
    }

    public void Shoot()
    {
        Bullet bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
        bullet.Project(transform.up);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        ICollidable other = collision.gameObject.GetComponent<ICollidable>();
        if (other != null)
        {
            OnCollision(other);
        }
    }

    public void OnCollision(ICollidable other)
    {
        if (other is Asteroid)
        {
            gameManager.GameOver();
        }
        OnCollisionEvent?.Invoke(other);
    }

    private void OnDestroy()
    {
        OnCollisionEvent = null;
    }
}
