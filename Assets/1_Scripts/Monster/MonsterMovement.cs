using System.Collections.Generic;
using UnityEngine;

public class MonsterMovement : MonoBehaviour
{
    public List<Vector3> pathNodes;
    public float speed = 2f;

    private int currentNodeIndex = 0;

    void Update()
    {
        if (currentNodeIndex < pathNodes.Count)
        {
            Vector3 targetPosition = pathNodes[currentNodeIndex];
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

            // ��忡 �����ϸ� ���� ���� �̵�
            if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
            {
                currentNodeIndex++;
            }
        }
    }
}
