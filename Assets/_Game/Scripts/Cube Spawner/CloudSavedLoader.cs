using IDosGames;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using UnityEngine;

public class CloudSavedLoader : MonoBehaviour
{
    private static string PLAYER_PREF_SAVED_MERGEABLE_CUBES { get; set; }

    [SerializeField] private GameCubeSpawner _spawner;

    private void OnEnable()
    {
        Playground.GameOver += OnGameOver;
    }

    private void OnDisable()
    {
        Playground.GameOver -= OnGameOver;
    }

    private void Start()
    {
        PLAYER_PREF_SAVED_MERGEABLE_CUBES = "SAVED_MERGEABLE_CUBES" + AuthService.UserID;
        //Load();
    }

    private void OnApplicationFocus(bool focus)
    {
        if (!focus)
        {
            //Save();
        }
    }

    private void OnApplicationQuit()
    {
        //Save();
    }

    private void OnGameOver()
    {
        SaveJArrayToPlayerPrefs(new JArray());
    }

    public void CloudSave()
    {
        JArray jArray = new();

        foreach (var cube in CubesPool.ActiveCubes)
        {
            if (GameCubeSpawner.CurrentGameCube == cube)
            {
                continue;
            }

            JObject jObject = new()
            {
                [JsonProperty.INDEX] = cube.Index.ToString(),
                [JsonProperty.TRANSFORM] = JsonConvert.SerializeObject(new SerializedTransform(cube.transform))
            };

            jArray.Add(jObject);
        }

        //SaveJArrayToPlayerPrefs(jArray);
        UserDataService.UpdateCustomUserData("saved_gameplay_data", jArray.ToString());
    }

    public void CloudLoad()
    {
        var loadedData = UserDataService.GetCachedCustomUserData(CustomUserDataKey.saved_gameplay_data); //PlayerPrefs.GetString(PLAYER_PREF_SAVED_MERGEABLE_CUBES, string.Empty);

        //Debug.Log(loadedData);

        if (loadedData == string.Empty)
        {
            return;
        }

        JArray jArray = JsonConvert.DeserializeObject<JArray>(loadedData);

        foreach (var jObject in jArray)
        {
            if (jObject[JsonProperty.INDEX] == null || jObject[JsonProperty.TRANSFORM] == null)
            {
                return;
            }

            Enum.TryParse((string)jObject[JsonProperty.INDEX], out CubeNumber.Index index);

            var transform = JsonConvert.DeserializeObject<JObject>((string)jObject[JsonProperty.TRANSFORM]);
            var position = new Vector3((float)transform[JsonProperty.POSITION][0], (float)transform[JsonProperty.POSITION][1], (float)transform[JsonProperty.POSITION][2]);
            var rotation = new Quaternion((float)transform[JsonProperty.ROTATION][1], (float)transform[JsonProperty.ROTATION][2], (float)transform[JsonProperty.ROTATION][3], (float)transform[JsonProperty.ROTATION][0]);

            _spawner.SpawnSavedMergeableCube(index, position, rotation);
        }
    }

    private void SaveJArrayToPlayerPrefs(JArray jArray)
    {
        PlayerPrefs.SetString(PLAYER_PREF_SAVED_MERGEABLE_CUBES, jArray.ToString());
    }
}
