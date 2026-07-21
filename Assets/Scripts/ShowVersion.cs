using TMPro;
using UnityEngine;

/// <summary> Overlays version number over multiple scenes using DontDestroyOnLoad.
/// Non-DontDestroyOnLoad code is at https://github.com/Feddas/Clawful/blob/main/Assets/Scripts/ShowVersion.cs </summary>
[RequireComponent(typeof(TextMeshProUGUI))]
public class ShowVersion : MonoBehaviour
{
    public static ShowVersion Instance { get; private set; }

    /// <summary> automatically find text that will hold the version string </summary>
    private TextMeshProUGUI versionLabel
    {
        get
        {
            return _versionLabel ??= this.GetComponent<TextMeshProUGUI>();
        }
    }
    private TextMeshProUGUI _versionLabel;

    void Awake()
    {
        // Guard clause: if Instance is already set, destroy this second instance so that a singleton can be maintained
        var root = this.transform.root;
        if (Instance != null && Instance.transform.root != root)
        {
            Destroy(root);
            Debug.LogWarning(Time.frameCount + ": " + this.name + " caused a singleton violation of " + Instance.name);
            return;
        }

        // Set instance to be DontDestroyOnLoad
        Instance = this;
        DontDestroyOnLoad(root);
    }

    void Start()
    {
        OnValidate();
    }

    void OnValidate()
    {
        versionLabel.text = Application.version;
    }
}
