using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MossGiantAnimation : MonoBehaviour
{
    private Animator _animator;

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponentInChildren<Animator>();
    }

    public void Idle()
    {
        _animator.SetTrigger("Idle");
    }
}
