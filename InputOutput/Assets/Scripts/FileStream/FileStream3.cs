using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class FileStream3 : MonoBehaviour
{
    string DirPath;
    Dictionary<string, string> settings = new();

    void Start()
    {
        DirPath = Path.Combine(Application.persistentDataPath, "Gwaze3");

        if (Directory.Exists(DirPath))
        {
            var files = Directory.GetFiles(DirPath);

            foreach (var file in files)
            {
                File.Delete(file);
            }

            Directory.Delete(DirPath);
        }

        Directory.CreateDirectory(DirPath);
        Debug.Log("생성됨");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            using (FileStream fs = File.Create(Path.Combine(DirPath, "settings.cfg")))
            using (StreamWriter sw = new StreamWriter(fs))
            {
                sw.WriteLine("master_volume=80");
                sw.WriteLine("bgm_volume=70");
                sw.WriteLine("sfx_volume=90");
                sw.WriteLine("language=kr");
                sw.WriteLine("show_damage=true");

                Debug.Log("파일 저장 완료");
            }
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            var datas = File.ReadAllLines(Path.Combine(DirPath, "settings.cfg"));

            foreach (var data in datas)
            {
                var parsed = data.Split('=');
                settings.Add(parsed[0], parsed[1]); // 키, 값
            }

            Debug.Log($"설정 로드 완료 (항목 {settings.Count}개)");
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            settings["bgm_volume"] = "50";
            settings["language"] = "en";

            using (FileStream fs = File.Create(Path.Combine(DirPath, "settings.cfg")))
            using (StreamWriter sw = new StreamWriter(fs))
            {
                foreach (var pair in settings)
                {
                    sw.WriteLine($"{pair.Key}={pair.Value}");
                }

                Debug.Log($"덮어쓰기 완료");
            }            
        }
        
        if (Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log("=== 최종 파일 내용 ===");
            Debug.Log($"{File.ReadAllText(Path.Combine(DirPath, "settings.cfg"))}");
        }
    }
}
