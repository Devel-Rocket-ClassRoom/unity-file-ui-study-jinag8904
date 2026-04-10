using NUnit.Framework;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class FileStream2 : MonoBehaviour
{
    string DirPath;

    void Start()
    {
        DirPath = Path.Combine(Application.persistentDataPath, "Gwaze2");

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
            File.WriteAllText(Path.Combine(DirPath, "secret.txt"), "Hello Unity World");
            Debug.Log($"원본: {File.ReadAllText(Path.Combine(DirPath, "secret.txt"))}");
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            using (FileStream fs1 = File.OpenRead(Path.Combine(DirPath, "secret.txt")))
            {
                using (FileStream fs2 = File.OpenWrite(Path.Combine(DirPath, "encrypted.dat")))
                {
                    int b;
                    while ((b = fs1.ReadByte()) != -1)
                    {
                        fs2.WriteByte(b ^ "0xAB");
                    }
                }
            }
        }
    }
}
