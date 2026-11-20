using UnityEngine;
using TMPro; // 匯入 TextMeshPro 函式庫

public class PocketController : MonoBehaviour
{
    public TextMeshProUGUI countText; // 連結左上角分數文字 (CountText)
    public GameObject winTextObject;  // 連結中間勝利文字 (WinText)

    // 加上 static，讓 6 個球洞共用同一個分數與進球數
    private static int count;
    private static int ballsPocketed; // 獨立計算已進洞的目標球數量（避免扣分導致無法通關）

    void Start()
    {
        count = 0;
        ballsPocketed = 0;
        SetCountText();

        // 遊戲開始時先隱藏 "You Win!" 文字
        if (winTextObject != null)
        {
            winTextObject.SetActive(false);
        }
    }

    // 參數 other：撞進洞裡的物件
    void OnTriggerEnter(Collider other)
    {
        // 狀況 1：目標球（9 顆球）進洞 -> 隱藏球、加分
        if (other.gameObject.CompareTag("Ball"))
        {
            other.gameObject.SetActive(false);
            count = count + 1;
            ballsPocketed = ballsPocketed + 1;
            SetCountText();
        }
        // 狀況 2：母球（白球）進洞 -> 扣分、放回桌上並完全靜止
        else if (other.gameObject.CompareTag("Player"))
        {
            count = count - 1;
            SetCountText();

            // 將母球放回球桌中央 (0, 0.5, 0)
            other.gameObject.transform.position = new Vector3(0f, 0.5f, 0f);

            // 取得母球剛體，將移動速度與旋轉速度歸零（靜止）
            Rigidbody rb = other.gameObject.GetComponent<Rigidbody>();
            rb.linearVelocity = Vector3.zero;        // 移動速度 = 0 (Unity 6 可用 rb.linearVelocity)
            rb.angularVelocity = Vector3.zero; // 旋轉速度 = 0
        }
    }

    void SetCountText()
    {
        if (countText != null)
        {
            countText.text = "Count: " + count.ToString();
        }

        // 當 9 顆目標球全部進洞時，顯示 "You Win!"
        if (ballsPocketed >= 9 && winTextObject != null)
        {
            winTextObject.SetActive(true);
        }
    }
}
