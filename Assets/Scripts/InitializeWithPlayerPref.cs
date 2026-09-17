using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerPref
{
    public struct PrefInfo
    {
        public enum DataType { Bool, Float, Int, String }
        public string Key;
        public DataType PrefDataType;

        public PrefInfo(string key, DataType prefDataType)
        {
            this.Key = key;
            this.PrefDataType = prefDataType;
        }
    }

    public enum Key { None, AudioMuted }

    /// <summary> instance of the player pref containing it's key string and data type </summary>
    public static readonly Dictionary<Key, PrefInfo> Of = new Dictionary<Key, PrefInfo>
    {
        [Key.AudioMuted] = new PrefInfo("SettingsAudioMuted", PrefInfo.DataType.Bool)
    };
}

public class InitializeWithPlayerPref : MonoBehaviour
{
    [SerializeField]
    [Tooltip("PlayerPref this control should be initialized with")]
    private PlayerPref.Key prefInitializer;

    public UnityEvent<bool> OnPrefLoadedBool;

    void Start()
    {
        var pref = PlayerPref.Of[prefInitializer];
        switch (pref.PrefDataType)
        {
            case PlayerPref.PrefInfo.DataType.Bool:
                bool resultBool = PlayerPrefs.GetInt(pref.Key) == 1;
                Debug.Log(pref.Key + " PlayerPref initialized with:" + resultBool);
                OnPrefLoadedBool.Invoke(resultBool);
                break;
            case PlayerPref.PrefInfo.DataType.Float:
                break;
            case PlayerPref.PrefInfo.DataType.Int:
                break;
            case PlayerPref.PrefInfo.DataType.String:
                break;
            default:
                break;
        }
    }
}
