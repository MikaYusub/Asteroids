using UnityEngine;

public class Asteroid : MonoBehaviour, ICollidable
{
    public Sprite[] sprites;
    public float size = 1;
    public float speed = 5.0f;
    public float minSize = 0.5f;
    public float maxSize = 1.5f;

    [SerializeField]
    private float maxLifetime = 30.0f; //seconds
    private SpriteRenderer _spriteRenderer;
    private Rigidbody2D _rigidbody;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        _spriteRenderer.sprite = sprites[UnityEngine.Random.Range(0, sprites.Length)];

        transform.eulerAngles = new Vector3(0, 0, UnityEngine.Random.value * 360);
        transform.localScale = Vector3.one * size;
        _rigidbody.mass = size;
    }

    public void SetTrajectory(Vector2 direction)
    {
        _rigidbody.AddForce(direction * speed);

        Destroy(gameObject, maxLifetime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out ICollidable other))
        {
            HandleCollision(other);
        }
    }

    public void HandleCollision(ICollidable other)
    {
        Debug.Log("Handled");
        if (other is Bullet)
        {
            if ((size * 0.5) >= minSize)
            {
                CreateSplit();
                CreateSplit();
            }
            Destroy(gameObject);
        }
        else if (other is Player)
        {
            GameManager.GetInstance().GameOver();
        }
    }

    private void CreateSplit()
    {
        Vector2 position = transform.position;
        position += Random.insideUnitCircle * 0.5f;
        Asteroid half = Instantiate(this, position, transform.rotation);
        half.size = size * 0.5f;
        half.SetTrajectory(Random.insideUnitCircle.normalized * speed);
    }
}
