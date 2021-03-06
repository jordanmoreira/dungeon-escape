﻿using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D _rb;

    [SerializeField]
    private float _speed = 3.0f;
    [SerializeField]
    private float _jumpHeight = 5.0f;
    private bool _resetJump = false;
    private bool _grounded;

    PlayerAnimation _playerAnimation;
    SpriteRenderer _playerSprite;
    SpriteRenderer _swordArcSprite;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _playerAnimation = GetComponent<PlayerAnimation>();
        _playerSprite = GetComponentInChildren<SpriteRenderer>();
        _swordArcSprite = transform.GetChild(1).GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        Attack();
    }

    void Movement()
    {
        float move = Input.GetAxisRaw("Horizontal");
        _grounded = IsGrounded();

        if (move > 0)
        {
            FlipPlayer(true);
        }
        else if (move < 0)
        {
            FlipPlayer(false);
        }

        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            _rb.velocity = new Vector2(_rb.velocity.x, _jumpHeight);
            StartCoroutine(ResetJumpRoutine());
            _grounded = false;
            _playerAnimation.Jump(true);
        }

        _rb.velocity = new Vector2(move * _speed, _rb.velocity.y);
        _playerAnimation.Move(move);
    }

    bool IsGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 1.0f, 1 << 8);
        if (hit.collider != null)
        {
            Debug.DrawRay(transform.position, Vector2.down, Color.green);
            if (_resetJump == false)
            {
                _playerAnimation.Jump(false);
                return true;
            }
        }

        return false;
    }

    void FlipPlayer(bool isFacingRight)
    {
        if (isFacingRight == true)
        {
            _playerSprite.flipX = false;
            _swordArcSprite.flipX = false;
            _swordArcSprite.flipY = false;

            Vector3 newPos = _swordArcSprite.transform.localPosition;
            newPos.x = 1.01f;
            _swordArcSprite.transform.localPosition = newPos;
        }
        else if (isFacingRight == false)
        {
            _playerSprite.flipX = true;
            _swordArcSprite.flipX = true;
            _swordArcSprite.flipY = true;

            Vector3 newPos = _swordArcSprite.transform.localPosition;
            newPos.x = -1.01f;
            _swordArcSprite.transform.localPosition = newPos;
        }
    }

    void Attack()
    {
        _grounded = IsGrounded();
        if (Input.GetKeyDown(KeyCode.Mouse0) && _grounded)
        {
            _rb.velocity = new Vector2(0, _rb.velocity.y);
            _playerAnimation.Attack();
        }
    }

    IEnumerator ResetJumpRoutine()
    {
        _resetJump = true;
        yield return new WaitForSeconds(0.1f);
        _resetJump = false;
    }

}

