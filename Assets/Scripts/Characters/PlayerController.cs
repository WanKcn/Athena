using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    // 获得NavMeshAgent 使PlayerController可以控制人物的Agent进行移动
    private NavMeshAgent agent;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        MouseManager.Instance.OnMouseClicked += MoveToTarget;
    }

    public void MoveToTarget(Vector3 target)
    {
        agent.destination = target;
    }
}