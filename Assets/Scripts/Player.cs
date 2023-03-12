using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour, ICollidable
{
    [SerializeField]
    private Bullet bulletPrefab;
    private Rigidbody2D _rigidbody;
    private bool _thrusting;

    [SerializeField]
    private float thrustSpeed = 1.0f;
    private GameManager gameManager;

    [SerializeField]
    private float shootingDelay = 0.2f;
    private bool shootingDelayed;

    [SerializeField]
    private ScreenBoundaries screenBoundaries;

    private void Start()
    {
        gameManager = GameManager.GetInstance();
    }

    private float _turndirection;

    [SerializeField]
    private float turnSpeed = 1.0f;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Move();
        Vector3 tempPosition = transform.localPosition;
        if (screenBoundaries.AmIOutOfBounds(tempPosition))
        {
            Vector2 newPosition = screenBoundaries.CalculateWrappedPosition(tempPosition);
            transform.position = newPosition;
        }
        else
        {
            transform.position = tempPosition;
        }
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    private void Move()
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
        if (shootingDelayed == false)
        {
            shootingDelayed = true;
            Bullet bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
            bullet.Project(transform.up);
            _ = StartCoroutine(DelayShooting());
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.TryGetComponent(out ICollidable other))
        {
            other.HandleCollision(this);
        }
    }

    public void HandleCollision(ICollidable other)
    {
        if (other is Asteroid)
        {
            gameManager.GameOver();
        }
    }

    private IEnumerator DelayShooting()
    {
        yield return new WaitForSeconds(shootingDelay);
        shootingDelayed = false;
    }
}
