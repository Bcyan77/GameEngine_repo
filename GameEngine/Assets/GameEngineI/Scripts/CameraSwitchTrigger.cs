using UnityEngine;
using Unity.Cinemachine;  // ⭐ Cinemachine 3: Unity.Cinemachine

public class CameraSwitchTrigger : MonoBehaviour
{
    [Header("카메라 설정")]
    [Tooltip("활성화할 Cinemachine Camera")]
    public CinemachineCamera targetCamera;  // ⭐ CinemachineCamera (Cinemachine 2: CinemachineVirtualCamera)
    
    [Tooltip("활성화 시 Priority")]
    public int activePriority = 11;
    
    [Tooltip("비활성화 시 Priority")]
    public int inactivePriority = 5;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        // 플레이어가 Trigger에 진입했을 때
        if (other.CompareTag("Player"))
        {
            if (targetCamera != null)
            {
                // 타겟 카메라의 Priority를 높여서 활성화
                targetCamera.Priority = activePriority;
                Debug.Log("보스 카메라 활성화! Priority: " + activePriority);
            }
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        // 플레이어가 Trigger에서 나갔을 때
        if (other.CompareTag("Player"))
        {
            if (targetCamera != null)
            {
                // 타겟 카메라의 Priority를 낮춰서 비활성화
                targetCamera.Priority = inactivePriority;
                Debug.Log("보스 카메라 비활성화! Priority: " + inactivePriority);
            }
        }
    }
}