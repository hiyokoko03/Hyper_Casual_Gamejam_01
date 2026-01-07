using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move_Floor : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("移動設定")]
    public float speed = 3f;           // 移動速度（1秒あたりの距離）

    private Vector3 pointAPos;         // PointA のワールド座標
    private Vector3 pointBPos;         // PointB のワールド座標
    private Vector3 targetPos;         // 今向かっている目標地点

    void Awake()
    {
        // 子オブジェクトから PointA / PointB を探す
        Transform pointA = transform.Find("PointA");
        Transform pointB = transform.Find("PointB");

        if (pointA == null || pointB == null)
        {
            Debug.LogError("PointA または PointB が見つかりません。Move_Floor の子に PointA / PointB を作ってください。");
            enabled = false;
            return;
        }

        // 最初の位置を保存（ワールド座標）
        pointAPos = pointA.position;
        pointBPos = pointB.position;

        // Move_Floor 自体を PointA に置く
        transform.position = pointAPos;
        targetPos = pointBPos;

        // 目印オブジェクトはゲーム中は不要なら非表示にしてもOK
        // pointA.gameObject.SetActive(false);
        // pointB.gameObject.SetActive(false);
    }

    void Update()
    {
        // 目標地点まで直線移動
        transform.position = Vector3.MoveTowards(
            transform.position,
            targetPos,
            speed * Time.deltaTime
        );

        // 目標に十分近づいたら、行き先を反転して往復
        if (Vector3.Distance(transform.position, targetPos) < 0.01f)
        {
            // 今の target が B なら A に、A なら B に切り替え
            if (targetPos == pointBPos)
                targetPos = pointAPos;
            else
                targetPos = pointBPos;
        }
    }

    void OnCollisionEnter(Collision other)
    {
        // 「Player タグの付いた親」を探す
        Transform t = other.transform;
        Transform playerRoot = null;

        while (t != null)
        {
            if (t.CompareTag("Player"))
            {
                playerRoot = t;
                break;
            }
            t = t.parent;
        }

        if (playerRoot != null)
        {
            Debug.Log("Floor Enter: " + playerRoot.name);
            playerRoot.SetParent(transform, true); // プレイヤー階層ごと床の子にする
        }
    }

    void OnCollisionExit(Collision other)
    {
        // Exit も同じ親を探して外す
        Transform t = other.transform;
        Transform playerRoot = null;

        while (t != null)
        {
            if (t.CompareTag("Player"))
            {
                playerRoot = t;
                break;
            }
            t = t.parent;
        }

        if (playerRoot != null && playerRoot.parent == transform)
        {
            Debug.Log("Floor Exit: " + playerRoot.name);
            playerRoot.SetParent(null, true);
        }
    }
}
