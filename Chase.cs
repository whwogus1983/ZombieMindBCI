using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chase : MonoBehaviour
{
    public Transform target = null; // 쫒아갈 대상의 Transform 컴포넌트
    public Transform player; // 캐릭터의 Transform 컴포넌트
    public float speed = 3f; // 몬스터의 이동 속도
    public float stoppingDistance = 1f; // 몬스터가 캐릭터를 멈출 거리
    public float meditation = 5f; // 외부 BCI 장비로 받을 이완 수치. 이 수치가 커질 수록 몬스터에게 감지되는 범위 증가

    void Start()
    {
        // 쫒아갈 대상을 Player로 지정
        // player = GameObject.Find("PlayerCapsule").transform; // 플레이어 오브젝트
    }

    private void Update()
    {
        // 몬스터와 캐릭터 사이의 거리를 계산
        float distance = Vector3.Distance(transform.position, player.position);

        if(target == null)
        {
            // meditation 수치를 감안하여 몬스터와 player 사이 거리가 충분히 가까우면 몬스터가 player를 쫒는다.
            if(distance < meditation)
            {
                OnDetectPlayer(player);
            }
        }
        else
        {
            if(distance > meditation)
            {
                OnLosePlayer();
            }
            // 몬스터가 캐릭터를 바라본다.
            transform.LookAt(target);
            // 캐릭터를 향해 이동한다.
            transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        }

        // 몬스터가 캐릭터에게 일정 거리 이내에 있으면 멈춘다.
        // if (distance > stoppingDistance)
        // {
        //     // 몬스터가 캐릭터를 향해 이동합니다.
        //     transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        // }
        // // 몬스터가 캐릭터에게 충분히 접근했으면 공격한다.
        // else
        // {
        //     // 몬스터가 캐릭터를 공격한다.
        //     // TryAttack();
        // }
    }

    // 캐릭터가 몬스터와 충분히 가까워졌을 때 호출되는 메서드
    public void OnDetectPlayer(Transform player)
    {
        // 몬스터가 캐릭터를 쫒아갈 대상으로 설정
        target = player;
    }

    // 캐릭터가 몬스터의 시야에서 벗어났을 때 호출되는 메서드
    public void OnLosePlayer()
    {
        // 몬스터가 쫒아갈 대상을 null로 설정
        target = null;
    }
}
