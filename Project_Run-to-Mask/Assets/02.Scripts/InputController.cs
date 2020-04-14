using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Purpose: 컨트롤러 입력값을 감지하고 처리하는 클래스
 * Notice: 
 */
public class InputController : MonoBehaviour
{
    [Header("MainCharacter")]
    public BaseCharacter m_mainCharacter;    //조작할 캐릭터

    [Header("Variables")]
    public float m_JumpPower;                 //캐릭터 점프력

    private Rigidbody2D m_rigid;             //조작할 캐릭터의 Rigidbody2D 
    private BoxCollider2D m_coll;            //조작할 캐릭터의 BoxCollider 

    private bool m_isInputLink;                  //조작할 캐릭터와 키입력이 연동되었는가?

    private void Start()
    {
        //note: 나중에 GameManager에서 호출해줘야함.
        Init_Controll(m_mainCharacter);
        Init_Variable();
    }

    #region Init
    /*
     * Purpose: 사용할 캐릭터에 입력값이 적용되도록 컨트롤러 초기화하는 함수
     * Variable: 초기화 할 캐릭터 오브젝트
     * Notice: 
     */
    public void Init_Controll(BaseCharacter character)
    {
        try
        {
            m_isInputLink = false;

            m_rigid = m_mainCharacter.GetComponent<Rigidbody2D>();
            m_coll = m_mainCharacter.GetComponent<BoxCollider2D>();

            if (m_rigid == null || m_coll == null)
            {
                throw (new Exception());
            }
        }
        catch (Exception)
        {
            Debug.LogError("ERROR!!! 매개변수가 잘못되었습니다! ~InputController/InitControll()");
            return;
        }

        m_isInputLink = true;
    }

    /*
     * Purpose: 인게임에서 사용할 기본 변수들을 초기화하는 함수
     * Variable: X
     * Notice: 캐릭터 중력값을 2.5로 맞춰줄 것.
     */
    public void Init_Variable()
    {
        m_JumpPower = 7.5f;
    }
    #endregion

    #region Input
    /*
     * Purpose: 캐릭터 점프하는 버튼용 함수
     * Variable: X
     * Notice: 
     */
    public void Input_Jump()
    {
        if(!m_isInputLink)
        {
            Debug.LogError("ERROR!!! 캐릭터와 키입력이 연동되지 않았습니다! ~~Jump()");
            return;
        }
        if(m_mainCharacter.m_curJumpCnt <= 0)
        {
            return;
        }

        m_rigid.velocity = new Vector2(0, m_JumpPower);
        m_mainCharacter.m_curJumpCnt--;
    }

    #endregion
}//end class