using System.Collections.Generic;
using UnityEngine;

public class SegmentManager : MonoBehaviour
{
    [Header("References")]
    public Transform playerTransform; // プレイヤーのTransform

    [Header("Map Settings")]
    public List<GameObject> segmentList; // 【仕様】固定順序のプレハブリスト
    public float segmentLength = 100f;   // 1セグメントの長さ
    public int segmentsOnScreen = 3;     // 先読みして表示しておく数
    public bool loopList = true;         // リストが尽きたら最初に戻るか（デバッグ用）

    // 【追加】プレイヤーの後ろ何メートルで消すか（セグメント長さ＋余裕を持たせる）
    [Tooltip("プレイヤーの後ろ何メートルで消すか。セグメント長さ(100)より大きくしてください")]
    public float destroyDistance = 200f;

    // 内部管理用
    private List<GameObject> activeSegments = new List<GameObject>();
    private float spawnZ = 0.0f;         // 次に生成するZ座標
    private int listIndex = 0;           // 現在リストの何番目か

    void Start()
    {
        // 最初に必要な分だけ生成しておく
        for (int i = 0; i < segmentsOnScreen; i++)
        {
            SpawnSegment();
        }
    }

    void Update()
    {
        // プレイヤーが「生成済み地点の少し手前」まで来たら次を作る
        // (spawnZ - segmentsOnScreen * segmentLength) は「先頭セグメントの終わり」付近を指す
        if (playerTransform.position.z > (spawnZ - segmentsOnScreen * segmentLength))
        {
            SpawnSegment();
        }
        // 【変更】削除ロジックを常時チェックするように分離
        RemoveOldSegment();
    }

    void SpawnSegment()
    {
        GameObject prefabToSpawn = null;

        // リストからプレハブを選択
        if (listIndex < segmentList.Count)
        {
            prefabToSpawn = segmentList[listIndex];
            listIndex++;
        }
        else
        {
            // リストを使い切った場合
            if (loopList)
            {
                listIndex = 0;
                prefabToSpawn = segmentList[listIndex];
                listIndex++;
            }
            else
            {
                return; // 生成終了（ゴール等の処理へ）
            }
        }

        // 生成処理
        if (prefabToSpawn != null)
        {
            // インスタンス化して、SegmentManagerの子オブジェクトにする（整理整頓）
            GameObject go = Instantiate(prefabToSpawn, transform);
            // 位置合わせ（重要：Pivotが入口にある前提）
            go.transform.position = Vector3.forward * spawnZ;

            activeSegments.Add(go);

            // 次の生成位置を更新
            spawnZ += segmentLength;
        }
    }

    // 【変更】距離ベースで削除する処理に変更
    void RemoveOldSegment()
    {
        // リストに中身がある場合のみチェック
        if (activeSegments.Count > 0)
        {
            // 一番古い（リスト先頭の）セグメントを取得
            GameObject oldSegment = activeSegments[0];

            // 「プレイヤーの現在地」と「古いセグメントの位置（入口）」の距離を計算
            float distance = playerTransform.position.z - oldSegment.transform.position.z;

            // その距離が設定値(destroyDistance)を超えていたら削除
            if (distance > destroyDistance)
            {
                activeSegments.RemoveAt(0); // リストから外す
                Destroy(oldSegment);        // ゲーム内から消す
            }
        }
    }
}