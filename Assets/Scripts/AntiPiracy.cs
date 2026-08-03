using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// copied from https://youtu.be/TDS2LBEqrlg / https://github.com/JOrbits/UnitySitelockingExample/blob/main/WebGLWhitelist/Assets/Scripts/AntiPiracy.cs
/// </summary>
public class AntiPiracy : MonoBehaviour
{
    [Tooltip("modifying this boolean does nothing")]
    [Header("On URL mismatch, loads\n02AntiPiracy.unity instead")]
    public bool ofTheActualGame;

    private string[] allowedHosts =
    {
        "://shawn.featherly.net/",
        "file://",
        "://localhost:",
        "://127.0.0.1:",
    };

    private void Start()
    {
        if (false == Application.isEditor)
        {
            ValidateURL();
        }
    }

    private void ValidateURL()
    {
        if (false == IsValidURL(allowedHosts))
        {
            Debug.LogWarning("Bad URL:" + Application.absoluteURL.ToLower());
            SceneManager.LoadScene("02AntiPiracy");
        }
    }

    private static bool IsValidURL(IEnumerable<string> urls)
    {
        return urls.Any(url => Application.absoluteURL.ToLower().Contains(url));
    }
}
