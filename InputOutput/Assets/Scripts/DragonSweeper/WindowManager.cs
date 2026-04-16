using UnityEngine;

public class WindowManager : MonoBehaviour
{
    public GenericWindow[] windows;

    public int currentWindowId;
    public int defaultWindowId;

    private void Awake()
    {
        foreach (var window in windows)
        {
            window.gameObject.SetActive(false);
            window.Init(this);
        }

        currentWindowId = defaultWindowId;
        windows[currentWindowId].Open();
    }

    public GenericWindow Open(int windowId)
    {
        windows[currentWindowId].Close();
        currentWindowId = windowId;
        windows[currentWindowId].Open();

        return windows[currentWindowId];
    }
}
