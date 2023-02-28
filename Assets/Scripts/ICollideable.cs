public enum CollidableType
{
    Player,
    Bullet,
    Asteroid,
}

public interface ICollidable
{
    CollidableType GetCollidableType();
    void OnCollide();
}
