// Unity의 기본 컬렉션 클래스들을 사용하기 위한 네임스페이스
using System.Collections;
// 제네릭 컬렉션 클래스들을 사용하기 위한 네임스페이스
using System.Collections.Generic;
// Unity 엔진의 핵심 기능들을 사용하기 위한 네임스페이스
using UnityEngine;
// UI 요소를 사용하기 위한 네임스페이스 (Image 컴포넌트 등)
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// 게임의 전반적인 상태를 관리하는 클래스
public class GameManager : MonoBehaviour
{
    // 게임 오버 상태를 추적하는 정적 변수
    // 다른 스크립트에서 게임 오버 상태를 확인할 수 있습니다.
    public static bool isGameOver = false;

    // Unity 인스페이터에서 UI 참조 변수들을 그룹화하여 표시
    [Header("UI References")]
    // HP 게이지를 표시하는 UI Image 컴포넌트 (fillAmount 속성으로 HP 표시)
    public Image hpGauge;
    // 게임 재시작 버튼 오브젝트
    public GameObject restartButton;
    // 게임 오버 텍스트 오브젝트
    public GameObject gameOverText;

    // 플레이어 오브젝트의 참조를 저장하는 변수
    // 게임 오버 시 플레이어 이동을 제어하기 위해 사용됩니다.
    GameObject player;

    // 게임 시작 시 한 번만 호출되는 Unity 생명주기 메서드
    void Start()
    {
        // 게임 오버 상태 초기화
        isGameOver = false;
        
        // 씬에서 "player"라는 이름의 게임 오브젝트를 찾아 참조를 저장
        player = GameObject.Find("player");
        
        // 재시작 버튼을 비활성화 (게임 시작 시에는 보이지 않음)
        restartButton.SetActive(false);
        // 게임 오버 텍스트를 비활성화 (게임 시작 시에는 보이지 않음)
        gameOverText.SetActive(false);
    }

    /// HP를 0.2만큼 감소시키고, HP가 0이 되면 게임 오버 처리
    public void DecreaseHp()
    {
        // HP 게이지의 채워진 양을 0.2만큼 감소시킴
        hpGauge.fillAmount -= 0.2f;
        
        // 부동소수점 오차 방지: 매우 작은 값(0.001 이하)을 0으로 처리
        // 0.2 - 0.2 연산 시 부동소수점 오차로 인해 매우 작은 값이 남는 것을 방지
        if (hpGauge.fillAmount < 0.001f)
        {
            // HP를 정확히 0으로 설정
            hpGauge.fillAmount = 0f;
        }
        
        // fillAmount 값을 0과 1 사이로 제한 (음수나 1 초과 값 방지)
        hpGauge.fillAmount = Mathf.Clamp01(hpGauge.fillAmount);

        // 게임 오버 체크: HP가 0 이하이거나 거의 0인 경우
        // Mathf.Approximately는 부동소수점 오차를 고려한 비교 함수
        if (hpGauge.fillAmount <= 0f || Mathf.Approximately(hpGauge.fillAmount, 0f))
        {
            // 게임 오버 상태로 설정
            isGameOver = true;
            
            // 게임 오버 텍스트를 활성화하여 표시
            gameOverText.SetActive(true);
            // 재시작 버튼을 활성화하여 표시
            restartButton.SetActive(true);

            // 게임 일시정지
            // Time.timeScale = 0;
        }
    }

    public void restartGame()
    {
        // 게임 오버 상태를 초기화
        isGameOver = false;
        
        SceneManager.LoadScene("SampleScene");
    }
}
