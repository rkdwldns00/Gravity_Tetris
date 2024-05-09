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
        //줄 순회
        for (int y = -10; y < 10; y++)
        {
            int totalCount = 0;
            int colliderCount = 0;
            List<Collider> colliders = new List<Collider>();
            //한줄 안에서 xy로 0.1간격으로 순회
            for (float x = -4.9f; x < 4.9f; x += 0.1f)
            {
                for (float y2 = y - 0.4f; y2 <= y + 0.4f; y2 += 0.1f)
                {
                    totalCount++;
                    Collider c = RayCast(new Vector3(x, y2, 0.5f), Vector3.back, 1);
                    //감지된 횟수 추가
                    if (c != null)
                    {
                        colliderCount++;
                    }
                    //줄안에 포함된 컬라이더를 리스트에 추가
                    if (c != null && !colliders.Contains(c))
                    {
                        colliders.Add(c);
                    }
                }
            }

            //감지된 횟수와 체크 횟수를 통해 몇퍼센트가 차있는지 확인
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
