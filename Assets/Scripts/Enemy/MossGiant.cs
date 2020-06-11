using UnityEngine;

public class MossGiant : Enemy
{
    Vector3 _currentTarget;
    Animator _animator;
    MossGiantAnimation _mossGiantAnimation;
    SpriteRenderer _sprite;

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponentInChildren<Animator>();
        _mossGiantAnimation = GetComponent<MossGiantAnimation>();
        _sprite = GetComponentInChildren<SpriteRenderer>();
    }

    public override void Update()
    {
        // prevent gameobject from moving while the Idle animation is playing
        if (_animator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        {
            return;
        }
        Movement();
    }

    void Movement()
    {
        FlipMossGiant();

        if (transform.position == pointA.position)
        {
            _mossGiantAnimation.Idle();
            _currentTarget = pointB.position;
        }
        else if (transform.position == pointB.position)
        {
            _mossGiantAnimation.Idle();
            _currentTarget = pointA.position;
        }

        transform.position = Vector3.MoveTowards(transform.position, _currentTarget, speed * Time.deltaTime);
    }

    void FlipMossGiant()
    {
        if (_currentTarget == pointA.position)
        {
            _sprite.flipX = true;
        }
        else
        {
            _sprite.flipX = false;
        }
    }
}
