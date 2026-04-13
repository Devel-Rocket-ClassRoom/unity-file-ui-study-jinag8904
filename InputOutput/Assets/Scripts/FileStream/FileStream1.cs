using UnityEngine;
using System.IO;

public class FileStream1: MonoBehaviour
{
    string DirPath;

    private void Start()
    {
        DirPath = Path.Combine(Application.persistentDataPath, "Gwaze1");

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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            File.WriteAllText(Path.Combine(DirPath, "save1.txt"), "Content1");
            File.WriteAllText(Path.Combine(DirPath, "save2.txt"), "Content2");
            File.WriteAllText(Path.Combine(DirPath, "save3.txt"), "Content3");
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            var files = Directory.GetFiles(DirPath);

            foreach (var file in files)
            {
                Debug.Log($"{Path.GetFileName(file)} ({Path.GetExtension(file)})");
            }
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            File.Copy(Path.Combine(DirPath, "save1.txt"), Path.Combine(DirPath, "save1_backup.txt"));
            Debug.Log("save1.txt 복사함");
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            File.Delete(Path.Combine(DirPath, "save3.txt"));
            Debug.Log("save3.txt 삭제함");
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            Debug.Log("===== 작업 후 목록 =====");
            var files = Directory.GetFiles(DirPath);

            foreach (var file in files)
            {
                Debug.Log($"{Path.GetFileName(file)} ({Path.GetExtension(file)})");
            }
        }
    }
}
