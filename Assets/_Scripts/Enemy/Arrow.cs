using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// This is to be instantiated by the ranged enemy type when the player is in range of sight

// These are used to ensure that when the GameObject is instantiated, that it contains the following components:
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]
public class Arrow : MonoBehaviour
{
}
