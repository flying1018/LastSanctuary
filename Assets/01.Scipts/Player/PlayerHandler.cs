using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHandler : MonoBehaviour
{
    [Header("Gravity")]
    [SerializeField] private float gravityPower = 2.5f;
    [SerializeField] private float maxFallSpeed = -20f;
    private float currentVelocity;

    private Player _player;
    private PlayerCondition _playerCondition;
    public Vector2 velocity;

    private void Awake()
    {
        _playerCondition = GetComponent<PlayerCondition>();
        _player = GetComponent<Player>();
    }

    public void ApplyGravity()
    {
        velocity = _player.Rigidbody.velocity;

        if (!_player.IsGround())
        {
            currentVelocity += Physics2D.gravity.y * gravityPower * Time.deltaTime;

            if (currentVelocity < maxFallSpeed) { currentVelocity = maxFallSpeed; }
            velocity.y = currentVelocity;

        }
        else
        {
            if (currentVelocity < 0)
                currentVelocity = 0;
        }
        velocity.y = currentVelocity;
    }
}
