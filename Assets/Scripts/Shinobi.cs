using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// INHERITANCE -- enemy is the parent class, shinobi is a child class and can set it's own speed

public class Shinobi : Enemy
{
    private void Awake()
    {
        Speed = 10;
    }
}
