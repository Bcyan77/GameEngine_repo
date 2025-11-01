using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("이동 설정")]
    public float moveSpeed = 5.0f;
    
    [Header("점프 설정")]
    public float jumpForce = 10.0f;
    
    private Rigidbody2D rb;
    private bool isGrounded = false;
    private Vector3 startPosition;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
        // 게임 시작 시 위치를 저장 - 새로 추가!
        startPosition = transform.position;
        Debug.Log("시작 위치 저장: " + startPosition);
    }
    
    void Update()
    {
        // 좌우 이동
        float moveX = 0f;
        if (Input.GetKey(KeyCode.A)) moveX = -1f;
        if (Input.GetKey(KeyCode.D)) moveX = 1f;
        
        rb.velocity = new Vector2(moveX * moveSpeed, rb.velocity.y);
        
        // 점프 (지난 시간에 배운 내용)
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }
    
    // 바닥 충돌 감지 (Collision)
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
        // 장애물 충돌 시 생명 감소로 변경!
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            Debug.Log("⚠️ 장애물 충돌! 생명 -1");
            // GameManager 찾아서 생명 감소
            GameManager gameManager = FindObjectOfType<GameManager>();
            
            if (gameManager != null)
            {
                gameManager.TakeDamage(1);  // 생명 1 감소
            }
            
            // 짧은 무적 시간 (0.5초 후 원래 위치로)
            transform.position = startPosition;
            rb.velocity = Vector2.zero;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
            Debug.Log("isGrounded: " + isGrounded);
        }
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        // 골 도달 - 새로 추가!
        if (other.CompareTag("Goal"))
        {
            Debug.Log("🎉 Goal Reached!");
            GameManager gameManager = FindObjectOfType<GameManager>();
            if (gameManager != null)
            {
                gameManager.GameClear();  // 게임 클리어 함수 호출
            }
        }
    }
}