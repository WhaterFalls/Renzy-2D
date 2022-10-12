using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// POLYMORPHISM -- different subclasses attack at different speeds

public class GhostShinobi : Enemy
{
    private void Awake()
    {
        Speed = 7.5f;
    }
}
