using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private const string IS_WALKING = "IsWalking";
    private Animator animator;

    [SerializeField] private Player player;

    void Update()
    {
        animator.SetBool(IS_WALKING, player.IsWalking());
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
        
    }
}
