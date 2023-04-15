using UnityEngine;

public class Enemy_Idle : StateMachineBehaviour
{
    private EnemyBase _enemy; // EnemyBase 스크립트를 가진 게임 오브젝트를 참조하기 위한 변수

    // 상태가 시작될 때 호출되는 함수
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _enemy = animator.GetComponent<EnemyBase>(); // EnemyBase 스크립트를 가진 게임 오브젝트를 참조

    }

    // 상태가 업데이트될 때 호출되는 함수
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // if (_enemy.Distance() <= _enemy._attackRange && _enemy.CandShot())
        //     animator.SetTrigger("Attack");
        if (_enemy.Distance() > _enemy._followRange)
            animator.SetBool("IsFollow", false);


        // 플레이어와 일정 거리 이내에 있으면 IsFollow를 true로 변경하여 Follow 상태로 전환
        if (_enemy.Distance() <= _enemy._followRange)
            animator.SetBool("IsFollow", true);
    }

    // 상태가 종료될 때 호출되는 함수
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
}
