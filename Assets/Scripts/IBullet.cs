using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBullet
{
    // Start is called before the first frame update
    void SetNewBulletValues(Vector2 newDirection, Vector3 newPosition);
}
