using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Purpose: 플레이어가 조종할 캐릭터들의 기본 베이스가 되는 클래스
 * Notice: 이 클래스 자체가 사용되진 않음. 항상 상속해서 사용됨.
 */
public class BaseCharacter : MonoBehaviour
{
    [Header("Variable")]
    public float m_maxTime;   //캐릭터가 달릴 수 있는 최대 시간
    public float m_curTime;   // `` 현재 시간
    public int m_maxJumpCnt;  //캐릭터가 점프할 수 있는 최대 횟수
    public int m_curJumpCnt;  // `` 현재 가능 횟수

    [Space(10)]
    public LayerMask m_groundLayer;  //ground(바닥) 레이어마스크 담는 변수

    protected Rigidbody2D m_rigid;    //캐릭터 Rigidbody2D

    #region Init
    /*
     * Purpose: 캐릭터 기본 변수를 초기화하는 함수
     * Variable: X
     * Notice: 오버라이드로 캐릭터별 조정 가능, layer_ground 도 초기화함.
     */
    virtual public void Init_Variable()
    {
        try
        {
            m_maxTime = 60f;
            m_curTime = m_maxTime;
            m_maxJumpCnt = 2;
            m_curJumpCnt = m_maxJumpCnt;

            m_groundLayer = LayerMask.GetMask("Ground");
            if (m_groundLayer.value == 0)
                throw new Exception("LayerMask에 Ground가 존재하지 않습니다!");

            m_rigid = GetComponent<Rigidbody2D>();
            if (m_rigid == null)
                throw new Exception("캐릭터 객체에 Rigidbody2D가 존재하지 않습니다!");
        }
        catch(Exception ex)
        {
            Debug.Log("ERROR!!! " + ex.Message + " ~BaseCharacter/Init_Variable()");
        }
    }
    #endregion

    #region Do
    /*
     * Purpose: 점프 카운트를 리필해주는 함수
     * Variable: X
     * Notice: Is_Ground() 를 통해 바닥에 붙어있는지 확인
     */
    protected void Do_RefillJumpCnt()
    {
        if(m_rigid.velocity.y >= 0f)
        {
            return;
        }

        if (m_curJumpCnt <= 0 && Is_Ground())
        {
            m_curJumpCnt = m_maxJumpCnt;
        }
    }
    #endregion

    #region Is
    /*
     * Purpose: 캐릭터가 바닥에 붙어있는지 확인하는 함수
     * Variable: X
     * Notice: 붙어있으면 true, 아니면 false
     */
    protected bool Is_Ground()
    {
        Debug.DrawRay(transform.position, Vector2.down * 0.6f, Color.red);

        if (Physics2D.Raycast(transform.position, Vector2.down, 1.5f, m_groundLayer.value))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    #endregion


}//end class