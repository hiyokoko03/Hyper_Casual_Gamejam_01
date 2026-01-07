using UnityEngine;

public class Pendulum : MonoBehaviour
{
    [Header("振り子設定")]
    [Tooltip("片側の最大角度（度数法）")]
    public float maxAngle = 45f;      // 片側の最大角度（度）

    [Tooltip("揺れの速さ（値を大きくすると速く揺れる）")]
    public float speed = 1f;          // 揺れの速さ

    [Tooltip("どの軸で振るか（2D横向きなら Z 軸が多い）")]
    public Vector3 axis = new Vector3(0, 0, 1); // 回転軸

    private Quaternion _startRot;

    // ★ 自前の「時間（位相）」を持つ
    private float _phase = 0f;

    void Start()
    {
        // 最初の回転を記録しておく
        _startRot = transform.localRotation;
    }

    void Update()
    {
        // ★ スペースを押している間だけ「時間の進む向き」を逆にする
        float dir = Input.GetKey(KeyCode.Space) ? -1f : 1f;

        // 位相を更新（dirが-1のときは時間が逆に進むイメージ）
        _phase += Time.deltaTime * speed * dir;

        // -1 ～ 1 の値で揺れる
        float t = Mathf.Sin(_phase);

        // -maxAngle ～ +maxAngle に変換
        float angle = t * maxAngle;

        // 指定した軸周りに angle 度だけ回転
        Quaternion swingRot = Quaternion.AngleAxis(angle, axis.normalized);

        // 初期回転から左右に振る
        transform.localRotation = _startRot * swingRot;
    }

    // （おまけ）Sceneビューで支点からの線を表示したい場合
    void OnDrawGizmos()
    {
        // 子がいれば、支点→子への線を表示
        if (transform.childCount > 0)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(transform.position, transform.GetChild(0).position);
        }
    }
}
