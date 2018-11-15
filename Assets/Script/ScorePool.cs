using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using System.Text;

//
// ハイスコアの貯蔵をデバイス内に行う
// シリアライズにはJSONを使用する
//
[Serializable]
public class ScorePool {
    public int[] List = new int[5];

    public ScorePool() {
        Clear();
    }

    FileInfo GetScorePoolFileInfo() {
        string filePath = Application.persistentDataPath + "/ScorePool.json";
        Debug.Log("filePath = " + filePath);
        return new FileInfo(filePath);
    }

    void WriteScorePoolJson(String json) {
        FileInfo fileInfo = GetScorePoolFileInfo();
        using (StreamWriter streamWriter = fileInfo.CreateText()) {
            streamWriter.Write(json);
        }
    }

    String ReadScorePoolJson() {
        ScorePool scorePool = new ScorePool();          // 空のスコアプールを作成
        String json = JsonUtility.ToJson(scorePool);    // 初期値としてのjsonを作成
        FileInfo fileInfo = GetScorePoolFileInfo();
        try {
            using (StreamReader streamReader = new StreamReader(fileInfo.OpenRead(), Encoding.UTF8)) {
                json = streamReader.ReadToEnd();
            }
        }
        catch (Exception e) {
            Debug.Log(e.ToString());
            WriteScorePoolJson(json);
        }
        return json;
    }

    public void Load() {
        string json = ReadScorePoolJson();
        Debug.Log("Load json\n" + json);
        JsonUtility.FromJsonOverwrite(json, this);
        if (List.Length < 5) {
            Array.Resize(ref List, 5);
        }
        Debug.Log("Array length = " + List.Length);
    }

    public void Save() {
        string json = JsonUtility.ToJson(this);
        Debug.Log("Save json\n" + json);
        WriteScorePoolJson(json);
    }

    public void SetScore(int newScore) {
        int index = -1;
        for (int i = 0; i < List.Length; ++i) {
            if (List[i] == newScore) {
                break;
            }
            if (List[i] < newScore) {
                index = i;
                break;
            }
        }
        if (List.Length == 0) {
            index = 0;
        }
        if (index < 0) {
            return;
        }
        for (int i = List.Length - 1; i > index; --i) {
            List[i] = List[i - 1];
        }
        List[index] = newScore;
    }

    public void Clear() {
        for (int i = 0; i < List.Length; ++i) {
            List[i] = 0;
        }
    }
}
