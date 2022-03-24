using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockCharacterCollision : MonoBehaviour
{
    public BoxCollider2D characterCollider;
    public BoxCollider2D chatacterBlockerCollider;

    private void Start()
    {
        Physics2D.IgnoreCollision(characterCollider, chatacterBlockerCollider, true);
    }

}
