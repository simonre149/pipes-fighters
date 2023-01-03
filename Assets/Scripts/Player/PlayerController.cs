using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Unity.Netcode;

public class PlayerController : NetworkBehaviour
{
    public float speed = 5f;

    SpriteRenderer sr;
    Rigidbody2D rb;
    Vector2 movement;

    public override void OnNetworkSpawn()
    {
        if (!IsOwner) Destroy(this);

        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }

    void OnMove(InputValue value)
    {
        movement = value.Get<Vector2>();
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
    }
}
