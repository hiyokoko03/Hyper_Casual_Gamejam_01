using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float forwardSpeed = 20f;  // 前進スピード
    public float strafeSpeed = 10f;   // 横移動スピード
    public float limitX = 4.5f;       // 横移動の制限範囲（道路からはみ出さない用）

    void Update()
    {
        // 1. 自動前進 (Z軸)
        transform.Translate(Vector3.forward * forwardSpeed * Time.deltaTime);

        // 2. 左右移動 (X軸)
        float inputX = Input.GetAxis("Horizontal"); // A/Dキー または 矢印
        float moveX = inputX * strafeSpeed * Time.deltaTime;

        // 現在の位置を取得して、X移動だけ加算
        Vector3 currentPos = transform.position;
        currentPos.x += moveX;

        // 移動範囲を制限 (Clamp)
        currentPos.x = Mathf.Clamp(currentPos.x, -limitX, limitX);

        // 位置を適用
        transform.position = new Vector3(currentPos.x, transform.position.y, transform.position.z);
    }

    // 【追加】何かにぶつかった瞬間に呼ばれる関数
    void OnCollisionEnter(Collision collision)
    {
        // ぶつかった相手のタグが "Obstacle" だったら
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            Debug.Log("Game Over!"); // コンソールに表示
            SceneManager.LoadScene("Result"); // Resultシーンへ移動
        }
    }
}