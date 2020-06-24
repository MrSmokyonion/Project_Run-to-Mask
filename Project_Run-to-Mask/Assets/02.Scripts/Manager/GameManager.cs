using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/* Purpose: 게임의 핵심적인 메커니즘을 담당하는 매니저.
 * Notice: 이 클래스에서 InputController에 캐릭터 링크를 해줄 함수를 구현할 예정.
 */
public class GameManager : MonoBehaviour
{
    [Header("Minimap Variable")]
    public Transform m_DistanceObject;  //진행한 거리 측정용 오브젝트
    public Slider m_Minimap;            //미니맵 UI
    public Text m_currentDistanceText;  //현재 거리를 알려주는 텍스트 UI

    [Header("GameOver Objects")]
    public Animation m_gameoverAnim;    //게임오버를 출력하는 애니메이션.

    [Header("Managers")]
    public InputController m_InputController;    //InputController 매니저

    private float m_DashTime = 5f;      //대쉬 아이템 지속시간

    public static GameManager instance; //싱글톤 패턴용 변수

    private void Awake()
    {
        //싱글톤 할당.
        instance = this;
    }

    private void Update()
    {
        m_Minimap.value = m_DistanceObject.position.x * -1;
        m_currentDistanceText.text = ((int)m_Minimap.value).ToString();
    }

    #region Do
    /* Purpose: 게임 시작 시키는 함수
     * Variable: X
     * Notice: 게임 시작할 때 움직여야 하는 것들이 대부분 들어감.
     */
    public void Do_GameStart()
    {
        m_Minimap.maxValue = 2000f;
        m_DistanceObject.GetComponent<Rigidbody2D>().velocity = new Vector2(-5.0f, 0f);

        m_InputController.m_playerState = InputController.PLAYERSTATE.Run;
    }

    /* Purpose: 게임 오버 시키는 함수.
     * Variable: X
     * Notice: [20-06-25] 게임 오버 리액션 코드 추가.
     */
    public void Do_GameOver()
    {
        Debug.Log("NOTICE!!! 게임이 종료되었습니다! ~GameManager/Do_GameOver()");
        m_gameoverAnim.Play();
    }
    /* Purpose: 게임을 재시작하는 함수(현재 스테이지).
     * Variable: X
     * Notice: 다수의 씬이 로딩되어 있을 시에 상황을 테스트 해보지 않았음.
     */
    public void Do_GameRestart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }


    /* Purpose: 캐릭터가 지속시간동안 지형지물을 무시하고 달리도록 해주는 함수 (대쉬 아이템)
     * Variable: X
     * Notice: 이 함수는 아이템 메커니즘에 사용될 함수. GameManager나 아니면 다른 클래스에 정리해도 무관함.
     *          지속시간 뒤에 Do_CharacterDash_Cancel() 이 호출되서 대쉬가 끝남.
     */
    public void Do_CharacterDash()
    {
        //매개변수가 Layer 번호와 같은지 확인할 것!
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Trap"), true);
        Invoke("Do_CharacterDash_Cancel", m_DashTime);
    }
    private void Do_CharacterDash_Cancel()
    {
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Trap"), false);
    }
    #endregion
}
