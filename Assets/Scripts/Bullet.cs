using UnityEngine;

public class Bullet : MonoBehaviour, ICollidable
{
    private Rigidbody2D _rigidbody;

    [SerializeField]
    private float speed = 500.0f;

    [SerializeField]
    private float maxLifetime = 10.0f; //seconds

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    public void Project(Vector2 direction)
    {
        _rigidbody.AddForce(direction * speed);
        Destroy(gameObject, maxLifetime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }

    public CollidableType GetCollidableType()
    {
        return CollidableType.Bullet;
    }

    public void OnCollide() { }
}
