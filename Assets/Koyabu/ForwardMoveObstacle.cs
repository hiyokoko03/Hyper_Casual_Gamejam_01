using UnityEngine;

public class ForwardMoveObstacle : MonoBehaviour
{
    [Header("Settings")]
    public float moveSpeed = 3.0f;  // 移動速度

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
        // 時間（進行度）の管理
        if (TimeManager.isRewinding)
        {
            currentPhase -= Time.deltaTime * moveSpeed;
        }
        else
        {
            currentPhase += Time.deltaTime * moveSpeed;
        }

        transform.localPosition = new Vector3(
            initialLocalPos.x,
            transform.localPosition.y,
            initialLocalPos.z - currentPhase
        );
    }
}
