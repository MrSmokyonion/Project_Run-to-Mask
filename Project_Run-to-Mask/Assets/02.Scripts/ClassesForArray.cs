/* Purpose: 배열로 사용할 데이터들을 편하게 탐색하기 위해서 만든 스크립트
 * Notice: ARRAY_@@@ 형태로 클래스들을 만들어 둘 예정.
 */
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/* Purpose: 파티클을 배열로 저장하고 사용하기 위해 만듬.
 * Notice: 배열의 형태로 사용될 예정.
 */
[Serializable]
public class ARRAY_PARTICLE
{
    public string m_name;
    public ParticleSystem m_particle;
    public bool isPlayed;

    public ARRAY_PARTICLE(string _name, ParticleSystem _particle)
    {
        m_name = _name;
        m_particle = _particle;
        isPlayed = false;
    }
}
