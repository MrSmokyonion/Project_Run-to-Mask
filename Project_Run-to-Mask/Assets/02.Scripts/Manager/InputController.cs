﻿using System;
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
    public BaseCharacter m_mainCharacter;   //조작할 캐릭터

    [Header("Variables")]
    public float m_JumpPower;               //캐릭터 점프력
    public PLAYERSTATE m_playerState;       //플레이어(캐릭터) 상태
    public Transform m_CharacterStaticPos;  //플레이어가 고정적으로 있을 위치값.

    private Rigidbody2D m_rigid;            //조작할 캐릭터의 Rigidbody2D 
    private BoxCollider2D m_coll;           //조작할 캐릭터의 BoxCollider 

    private bool m_isInputLink;             //조작할 캐릭터와 키입력이 연동되었는가?


    private void Start()
    {
        //note: 나중에 GameManager에서 호출해줘야함.
        Init_Controll(m_mainCharacter);
        Init_Variable();
    }

    private void Update()
    {
        Do_CheckPlayerState();
    }

    private void FixedUpdate()
    {
        Do_CheckPlayerPosition();
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
        m_playerState = PLAYERSTATE.Idle;
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
            Debug.LogError("ERROR!!! 캐릭터와 키입력이 연동되지 않았습니다! ~InputContoller/Jump()");
            return;
        }
        if(m_mainCharacter.m_curJumpCnt <= 0)
        {
            return;
        }

        m_rigid.velocity = new Vector2(0, m_JumpPower);
        m_mainCharacter.m_curJumpCnt--;
        m_playerState = PLAYERSTATE.Jump;
    }

    /*
     * Purpose: 캐릭터 슬라이드 버튼 제어용 함수(누르고 있을 때)
     * Variable: X
     * Notice: 
     */
    public void Input_SlideButtonDown()
    {
        if (!m_isInputLink)
        {
            Debug.LogError("ERROR!!! 캐릭터와 키입력이 연동되지 않았습니다! ~InputContoller/Jump()");
            return;
        }

        m_coll.size = new Vector2(1.0f, 0.6f);
    }

    /*
     * Purpose: 캐릭터 슬라이드 버튼 제어용 함수(땠을 때)
     * Variable: X
     * Notice: 
     */
    public void Input_SlideButtonUp()
    {
        if (!m_isInputLink)
        {
            Debug.LogError("ERROR!!! 캐릭터와 키입력이 연동되지 않았습니다! ~InputContoller/Jump()");
            return;
        }

        m_coll.size = new Vector2(1.0f, 1.0f);
    }
    #endregion

    #region Do
    /* Purpose: 플레이어(캐릭터)의 상태를 체크 / 처리하는 함수.
     * Variable: X
     * Notice: 
     */
    private void Do_CheckPlayerState()
    {
        switch (m_playerState)
        {
            case PLAYERSTATE.Idle:
                break;

            case PLAYERSTATE.Run:
                m_mainCharacter.Do_Particle("Run", true);
                m_mainCharacter.Do_Particle("Landing", false);
                break;

            case PLAYERSTATE.Jump:
                m_mainCharacter.Do_Particle("Run", false);
                m_mainCharacter.Do_Particle("Jump", true);
                m_mainCharacter.Do_Particle("Landing", true);
                break;

            case PLAYERSTATE.Landing:
                m_mainCharacter.Do_Particle("Jump", false);
                m_mainCharacter.Do_Particle("Landing", false);
                m_mainCharacter.Do_Particle("Landing", true);

                m_playerState = PLAYERSTATE.Run;
                break;

            case PLAYERSTATE.Slide:
                break;

            case PLAYERSTATE.Dead:
                break;
        }
    }

    /* Purpose: (폐기)캐릭터를 계속해서 달리게 하는 함수.
     * Variable: X
     * Notice: 메인 메커니즘과 내용이 다름. 사용 안될 함수.
     */
    private void Do_CharacterRun()
    {
        if(m_playerState == PLAYERSTATE.Idle || m_playerState == PLAYERSTATE.Dead)
        {
            return;
        }

        Vector2 value = m_rigid.velocity;
        value.x = 3.0f;
        m_rigid.velocity = value;
    }

    /* Purpose: 캐릭터가 고정 좌표에 유지하고 있는지를 체크/이동시키는 함수.
     * Variable: X
     * Notice:
     */
    private void Do_CheckPlayerPosition()
    {
        Vector2 characterVelocity = m_rigid.velocity;
        float value = m_mainCharacter.transform.position.x - m_CharacterStaticPos.position.x;

        if (Math.Abs(value) <= 0.1)
        {
            characterVelocity.x = 0f;
            m_rigid.velocity = characterVelocity;
            return;
        }

        characterVelocity.x = (value <= 0) ? 0.8f : -0.8f;
        m_rigid.velocity = characterVelocity;
    }
    #endregion

    //===============================================
    //===============================================
    /* Purpose: 플레이어(캐릭터)의 현재 상태를 나타내는 enum.
     * Notice: 
     */
    public enum PLAYERSTATE
    {
        Idle = 0,
        Run,
        Jump,
        Landing,
        Slide,
        Dead
    }
}//end class