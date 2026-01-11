using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// UI 요소를 사용하기 위한 네임스페이스 (Image 컴포넌트 등)
using UnityEngine.UI;

// 게임의 전반적인 상태를 관리하는 클래스
// HP 게이지 관리 등의 게임 로직을 담당합니다.
public class GameManager : MonoBehaviour
{
    // HP 게이지를 표시하는 UI Image 컴포넌트
    // Unity 인스페이터에서 할당해야 하며, fillAmount 속성을 사용하여 HP를 표시합니다.
    public Image hpGauge;
    
    // HP를 감소시키는 메서드
    // 화살에 맞았을 때 등에 호출되어 HP 게이지를 0.2만큼 감소시킵니다.
    // fillAmount는 0.0(비어있음)부터 1.0(가득참)까지의 값을 가집니다.
    public void DecreaseHp()
    {
        // HP 게이지의 채워진 양을 0.2만큼 감소
        // fillAmount가 0.0이 되면 HP가 모두 소진된 상태입니다.
        hpGauge.fillAmount -= 0.2f;
        
        // 부동소수점 오차로 인해 음수가 되거나 매우 작은 값이 남는 것을 방지
        // 매우 작은 값(0.001 이하)도 0으로 처리하여 부동소수점 오차 문제 해결
        if (hpGauge.fillAmount < 0.001f)
        {
            hpGauge.fillAmount = 0f;
        }
        // 0과 1 사이의 값으로 제한
        hpGauge.fillAmount = Mathf.Clamp01(hpGauge.fillAmount);

        // 부동소수점 오차를 고려하여 0 이하인지 확인
        // Mathf.Approximately를 사용하여 부동소수점 오차를 허용한 비교
        if (hpGauge.fillAmount <= 0f || Mathf.Approximately(hpGauge.fillAmount, 0f))
        {
            Debug.Log("Game Over!");
            // 게임 오버 처리 로직을 여기에 추가할 수 있습니다.
        }
    }
}
