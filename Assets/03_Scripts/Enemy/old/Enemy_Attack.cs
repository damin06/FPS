using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Attack : StateMachineBehaviour
{
    private EnemyBase _enemy; // EnemyBase 스크립트를 가진 게임 오브젝트를 참조하기 위한 변수

    // 상태가 시작될 때 호출되는 함수
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _enemy = animator.GetComponent<EnemyBase>(); // EnemyBase 스크립트를 가진 게임 오브젝트를 참조
        _enemy.StartCoroutine(_enemy.OnShot());
    }

    // 상태가 업데이트될 때 호출되는 함수
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _enemy.Rotate();
    }

    // 상태가 종료될 때 호출되는 함수
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (_enemy.CandShot() && !animator.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {
            animator.SetTrigger("Attack");
            return;
        }
    }
}
