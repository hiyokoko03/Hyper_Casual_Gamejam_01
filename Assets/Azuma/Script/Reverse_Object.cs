using UnityEngine;
using System.Collections.Generic;

public class RewindableObject : MonoBehaviour
{
    // 記録するデータ構造
    struct PointInTime
    {
        public Vector3 position;
        public Quaternion rotation;

        public PointInTime(Vector3 _pos, Quaternion _rot)
        {
            position = _pos;
            rotation = _rot;
        }
    }

    [Header("Settings")]
    public int maxFrames = 300; // 300フレーム分のログ

    // ログを格納するリスト（リングバッファのように使用）
    LinkedList<PointInTime> pointsInTime = new LinkedList<PointInTime>();

    private Rigidbody rb;
    private bool isRewinding = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // スペースキーの状態をチェック
        if (Input.GetKeyDown(KeyCode.Space)) StartRewind();
        if (Input.GetKeyUp(KeyCode.Space)) StopRewind();
    }

    void FixedUpdate() // 物理挙動と同期させるためFixedUpdateを推奨
    {
        if (isRewinding)
        {
            Rewind();
        }
        else
        {
            Record();
        }
    }

    // 1. 記録フェーズ
    void Record()
    {
        // 300フレームを超えたら一番古いものを消す
        if (pointsInTime.Count >= maxFrames)
        {
            pointsInTime.RemoveLast();
        }

        // 現在の状態をリストの「先頭」に追加
        pointsInTime.AddFirst(new PointInTime(transform.position, transform.rotation));
    }

    // 2. 巻き戻しフェーズ
    void Rewind()
    {
        if (pointsInTime.Count > 0)
        {
            // リストの先頭（直近のデータ）を取得して適用
            PointInTime point = pointsInTime.First.Value;
            transform.position = point.position;
            transform.rotation = point.rotation;

            // 使ったデータは削除
            pointsInTime.RemoveFirst();
        }
        else
        {
            // ログが空になったら強制停止
            StopRewind();
        }
    }

    public void StartRewind()
    {
        isRewinding = true;
        if (rb != null) rb.isKinematic = true; // 巻き戻し中は物理を無効化
    }

    public void StopRewind()
    {
        isRewinding = false;
        if (rb != null) rb.isKinematic = false; // 通常時は物理を有効化
    }
}