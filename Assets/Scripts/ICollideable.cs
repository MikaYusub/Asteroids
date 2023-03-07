using System;

public interface ICollidable
{
    event Action<ICollidable> OnCollisionEvent;

    void OnCollision(ICollidable other);
}
