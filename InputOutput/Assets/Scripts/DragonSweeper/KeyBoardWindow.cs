using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class KeyBoardWindow : GenericWindow
{
    // 이름 적는 곳 (출력할 곳)
    public TextMeshProUGUI typingField;

    // 알파벳 버튼
    public Button[] keys;

    // 이름 타이핑
    private List<string> strings;
    public int lenLimit = 7;
    public string username; // 최종 결정 저장

    // 아래 버튼 셋
    public Button cancelButton;
    public Button deleteButton;
    public Button acceptButton;

    // 커서 깜박임
    private float blinkTimer = 0f;
    private float blinkInterval = 0.5f;
    private bool isCursorOn = true;
    private bool isCursorInvisible = false;

    private void Awake()
    {
        foreach (var k in keys)
        {
            k.onClick.AddListener(() => OnClick(k.name));
        }

        cancelButton.onClick.AddListener(OnCancel);
        deleteButton.onClick.AddListener(OnDelete);
        acceptButton.onClick.AddListener(OnAccept);
    }

    private void Update()
    {
        blinkTimer += Time.deltaTime;

        if (blinkTimer > blinkInterval && !isCursorInvisible)
        {
            CursorSwitch();
            blinkTimer = 0f;
            isCursorOn = !isCursorOn;
        }
    }

    public void CursorSwitch()
    {
        if (isCursorOn)
        {
            typingField.text = $"{string.Join("", strings)}";
        }

        else
        {
            typingField.text = $"{typingField.text}_";
        }
    }

    public override void Open()
    {
        base.Open();

        typingField.text = "_";
        strings = new();
        username = string.Empty;

        isCursorOn = true;
        isCursorInvisible = false;
    }

    public void OnClick(string alpha)
    {
        if (strings.Count < lenLimit)
        {
            strings.Add(alpha);
        }

        typingField.text = string.Join("", strings);
        if (strings.Count == lenLimit)
        {
            isCursorInvisible = true;
        }
    }

    public void OnCancel()  // 한 글자 삭제
    {
        if (strings.Count > 0)
        {
            strings.RemoveAt(strings.Count -1);
            typingField.text = string.Join("", strings);

            isCursorInvisible = false;
        }
    }

    public void OnDelete() // 전부 삭제
    {
        strings.Clear();
        typingField.text = string.Empty;
        
        isCursorInvisible = false;
    }

    public void OnAccept()  // 결정
    {
        username = string.Join("", strings);
        Debug.Log(username);
        windowManager.Open(0);
    }
}
