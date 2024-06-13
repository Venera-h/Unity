using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScreenSettingsManager : MonoBehaviour
{
    [Header("ScreenSettings")]
    [SerializeField] TextMeshProUGUI resolutionText;
    [SerializeField] Toggle fullScreenToggle;
    private const string RESOLUTION_KEY = "Resolution";
    private int currentResIndex = 0;
    private Resolution[] resolutions;

    bool hasFullScreen;
   
    void Start()
    {
        resolutions = Screen.resolutions;
        hasFullScreen = Screen.fullScreen;
        currentResIndex = PlayerPrefs.GetInt(RESOLUTION_KEY,0);
        SetResolutionText(resolutions[currentResIndex]);
    }
    
    public void ApplySettings()
    {
        ApplyResolution(resolutions[currentResIndex]);
    }
   
    public void SetFullScreen()
    {
        if(fullScreenToggle.isOn) hasFullScreen = true;
        else hasFullScreen = false;
    }

    #region ResolutionChange

    // текст для меню
    private void SetResolutionText(Resolution resolution)
    {
        resolutionText.text = $"{resolution.width} x {resolution.height}";
    }
    // выбор сл разрешения в списке
    public void SetNextResolution()
    {
        currentResIndex = GetNextIndex(resolutions, currentResIndex);
        SetResolutionText(resolutions[currentResIndex]);
    }
    // выбор пред идущего разрешения в списке
    public void SetPriviouseResolution()
    {
        currentResIndex = GetPreviousIndex(resolutions, currentResIndex);
        SetResolutionText(resolutions[currentResIndex]);
    }
    // смотрим на список и если он <1 то делаем 0 иначе делаем идекс  +1  делим без ост на длинну
    private int GetNextIndex<T>(IList<T> collection, int currenIndex)
    {
        if (collection.Count < 1) return 0;
        return (currenIndex + 1) % collection.Count;
    }
    // то же что и  GetNextIndex но для убывания
    private int GetPreviousIndex<T>(IList<T> collection, int currenIndex)
    {
        if (collection.Count < 1) return 0;
        if ((currenIndex - 1) < 0) return collection.Count - 1;
        return (currenIndex - 1) % collection.Count;
    }
    #endregion
   
    private void ApplyResolution(Resolution resolution)
    {
        SetResolutionText(resolution);
        Screen.SetResolution(resolution.width, resolution.height,hasFullScreen);
        PlayerPrefs.SetInt(RESOLUTION_KEY, currentResIndex);
    }

}
