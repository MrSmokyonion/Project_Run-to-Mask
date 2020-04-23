using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Purpose: 캐릭터가 카메라 밖으로 나가 DeadLine에 부딛혔는지 감지하는 클래스
 * Notice: DeadLine 트리거에 달아주어야 함!
 */
public class DeadLineTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            GameManager.instance.Do_GameOver();
        }
    }
}
