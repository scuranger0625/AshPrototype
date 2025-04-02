using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

public class VoiceManager : MonoBehaviour
{
    [System.Serializable]
    public class VoiceLine
    {
        public string id;
        public string jp;
        public string zh;
    }

    public AudioSource audioSource;
    public Text subtitleText; // 指向 UI 上的字幕 Text 元件
    public List<AudioClip> audioClips; // 放 Ash 的語音檔

    private Dictionary<string, VoiceLine> subtitleDict = new Dictionary<string, VoiceLine>();
    private Dictionary<string, AudioClip> clipDict = new Dictionary<string, AudioClip>();

    void Start()
    {
        LoadSubtitles();
        BuildClipDict();
    }

    void LoadSubtitles()
    {
        string path = Path.Combine(Application.dataPath, "../Audio/SubtitleText_JP_CN.json");
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            var lines = JsonConvert.DeserializeObject<List<VoiceLine>>(json);
            foreach (var line in lines)
            {
                subtitleDict[line.id] = line;
            }
        }
    }

    void BuildClipDict()
    {
        foreach (var clip in audioClips)
        {
            string key = clip.name.ToLower();
            if (!clipDict.ContainsKey(key))
                clipDict.Add(key, clip);
        }
    }

    public void PlayVoice(string voiceId)
    {
        string key = voiceId.ToLower();
        if (clipDict.ContainsKey(key))
        {
            audioSource.clip = clipDict[key];
            audioSource.Play();

            if (subtitleDict.ContainsKey(voiceId))
            {
                subtitleText.text = subtitleDict[voiceId].zh;
                Invoke("ClearSubtitle", 3f);
            }
        }
        else
        {
            Debug.LogWarning("No voice found for: " + voiceId);
        }
    }

    void ClearSubtitle()
    {
        subtitleText.text = "";
    }
}
