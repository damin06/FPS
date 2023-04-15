using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Follow : StateMachineBehaviour
{
    private EnemyBase _enemy; // EnemyBase 스크립트를 가진 게임 오브젝트를 참조하기 위한 변수
    private Transform _curPos; // 현재 위치를 가져오기 위한 변수

    // 상태가 시작될 때 호출되는 함수
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _enemy = animator.GetComponent<EnemyBase>(); // EnemyBase 스크립트를 가진 게임 오브젝트를 참조
        _curPos = animator.GetComponent<Transform>(); // 현재 위치를 저장
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        if (_enemy.Distance() <= _enemy._attackRange && _enemy.CandShot())
        {
            animator.SetTrigger("Attack");
            return;
        }

        _enemy.FollowPlayer();
        _enemy.Rotate();
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
}
