using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_AttackReady : StateMachineBehaviour
{
    private EnemyBase _enemy; // EnemyBase 스크립트를 가진 게임 오브젝트를 참조하기 위한 변수

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _enemy = animator.GetComponent<EnemyBase>(); // EnemyBase 스크립트를 가진 게임 오브젝트를 참조
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (_enemy.Distance() <= _enemy._attackRange)
        {
            if (_enemy.CandShot() && !animator.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
            {
                animator.SetTrigger("Attack");
                return;
            }
        }
        else
        {
            animator.SetBool("AttackRange", false);
        }

        _enemy.Rotate();
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
}
