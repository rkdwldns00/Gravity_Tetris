using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checker : MonoBehaviour
{
    public static Checker instance;

    private void Start()
    {
        instance = this;
    }

    private void Update()
    {
        CheckAll(false);
    }

    public void CheckAll(bool isDestroyCheck)
    {
        //�� ��ȸ
        for (int y = -10; y < 10; y++)
        {
            int totalCount = 0;
            int colliderCount = 0;
            List<Collider> colliders = new List<Collider>();
            //���� �ȿ��� xy�� 0.1�������� ��ȸ
            for (float x = -4.9f; x < 4.9f; x += 0.1f)
            {
                for (float y2 = y - 0.4f; y2 <= y + 0.4f; y2 += 0.1f)
                {
                    totalCount++;
                    Collider c = RayCast(new Vector3(x, y2, 0.5f), Vector3.back, 1);
                    //������ Ƚ�� �߰�
                    if (c != null)
                    {
                        colliderCount++;
                    }
                    //�پȿ� ���Ե� �ö��̴��� ����Ʈ�� �߰�
                    if (c != null && !colliders.Contains(c))
                    {
                        colliders.Add(c);
                    }
                }
            }

            //������ Ƚ���� üũ Ƚ���� ���� ���ۼ�Ʈ�� ���ִ��� Ȯ��
            if (isDestroyCheck && (float)colliderCount / totalCount >= 0.9f)
            {
                colliders.ForEach((c) => { Destroy(c.gameObject); });
            }
        }
    }

    Collider RayCast(Vector3 startPos, Vector3 direction, float distance)
    {
        RaycastHit hit;
        Physics.Raycast(startPos, direction, out hit, distance);
        Color color = Color.red;
        Collider collider = hit.collider;
        if (collider != null && hit.transform.GetComponent<Block>() != null && !hit.transform.GetComponent<Block>().isFalling)
        {
            color = Color.green;
        }
        else
        {
            collider = null;
        }
        Debug.DrawRay(startPos, direction, color);

        return collider;
    }
}
