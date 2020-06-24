using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Purpose: 엔딩에서 출력될 크래딧을 제어하는 클래스
 * Notice: 크래딧은 애니메이션으로 연출됨. Skip 제어 함수를 포함함.
 */
public class EndingCredit : MonoBehaviour
{
    public Animation m_endingCreditAnim;    //엔딩 크래딧 애니메이션
    public Animation m_skipAnim;            //스킵을 강조하는 애니메이션

    private bool m_skipBool = false;                //스킵 두번 터치 확인용 bool

    private void Start()
    {
        m_endingCreditAnim.Play();
    }

    private void Update()
    {
        if(m_endingCreditAnim.isPlaying == false)
        {
            Do_CreditEnd();
        }
    }

    /* Purpose: 엔딩 크래딧 스킵 버튼용 함수.
     * Variable: X
     * Notice: 처음 눌렀을 때 Skip 가능을 알리는 텍스트 출력. 두번째 눌렀을 때 Skip.
     */
    public void Do_Skip()
    {
        if(m_skipBool == false)
        {
            m_skipBool = true;
            m_skipAnim.Play();
        }
        else
        {
            Debug.Log("엔딩 크래딧 스킵 확인");
            Do_CreditEnd();
        }
    }

    /* Purpose: 크래딧 끝났을 때 Scene을 Load하는 함수.
     * Variable: X
     * Notice: 크래딧 애니메이션이 끝날 때 or Do_Skip()에 의하여 호출됨.
     */
    public void Do_CreditEnd()
    {
        //스킵하고 이동할 씬을 여기에 넣어주세요.
        Debug.Log("엔딩 크래딧 끝");
    }
}
