using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Purpose: 캐릭터가 플랫폼 아래로 떨어져서 GameOver 되는지 확인하는 클래스
 * Notice: 
 *  - 플랫폼 최하단의 콜리전(FallingDeath Check Object)에 달아 주어야 함.
 *  - 작동 방식은 DeadLineTrigger와 매우 유사함! 확실한 구분을 위해 만듬.
 */
public class FallingDeathTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            GameManager.instance.Do_GameOver();
        }
    }
}
