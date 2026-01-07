using UnityEngine;

public class MovingObstacle : MonoBehaviour
{
    [Header("Settings")]
    public float moveSpeed = 3.0f;  // 移動速度
    public float moveWidth = 3.0f;  // 片道の移動距離

    // 内部で管理する「動きの進行度」
    private float currentPhase = 0.0f;
    private Vector3 initialLocalPos;
    private Rigidbody rb;

    void Start()
    {
        // 基準となる初期位置を記憶
        initialLocalPos = transform.localPosition;

        // Rigidbodyがあれば取得しておく（物理干渉対策）
        rb = GetComponent<Rigidbody>();

        // 物理挙動で動かすわけではないので、Kinematicにしておくのが安全
        if (rb != null)
        {
            rb.isKinematic = true;
        }
    }

    void Update()
    {
        // 1. 時間（進行度）の管理
        // ★マイナスになっても制限（Clamp）せずにそのまま流すのがポイント
        if (TimeManager.isRewinding)
        {
            currentPhase -= Time.deltaTime * moveSpeed;
        }
        else
        {
            currentPhase += Time.deltaTime * moveSpeed;
        }

        // 2. 位置の計算 (PingPong動作)
        // ★ここが修正点： t に +0.5f を足す
        // これにより Start時点(t=0) が「0.5（往復の中央）」になります。
        // 結果、プラス時間で0.6(右)、マイナス時間で0.4(左)へと分岐します。
        float pingPongValue = Mathf.PingPong(currentPhase + 0.5f, 1f);

        // 中心(0.5)から -0.5～+0.5 に変換し、幅を掛ける
        float offsetX = (pingPongValue - 0.5f) * 2 * moveWidth;

        // 3. 座標の適用
        transform.localPosition = new Vector3(
            initialLocalPos.x + offsetX,
            transform.localPosition.y,
            initialLocalPos.z
        );
    }
}