using IDosGames;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using UnityEngine;

public class SavedMergeableCubesLoader : MonoBehaviour
{
	private const string PLAYER_PREF_SAVED_MERGEABLE_CUBES = "SAVED_MERGEABLE_CUBES";

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
        LoadFromServer(); //Load();
        //StartCoroutine(AutoSaveCoroutine());
    }

	private void OnApplicationFocus(bool focus)
	{
		if (!focus)
		{
            SaveToServer(); //Save();

        }
	}

	private void OnApplicationQuit()
	{
        //Save();
        SaveToServer();
    }

    private IEnumerator AutoSaveCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(60);
            SaveToServer(); //Save();
        }
    }

    private void OnGameOver()
	{
		SaveJArrayToPlayerPrefs(new JArray());
	}

	public void Save()
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

		SaveJArrayToPlayerPrefs(jArray);
	}

    private void Load()
    {
        var loadedData = PlayerPrefs.GetString(PLAYER_PREF_SAVED_MERGEABLE_CUBES, string.Empty);

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

    public void SaveToServer()
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

        SaveJArrayToServer(jArray);
    }

    private void LoadFromServer()
    {
        var loadedData = UserDataService.GetCachedCustomUserData(PLAYER_PREF_SAVED_MERGEABLE_CUBES);

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

    private void SaveJArrayToServer(JArray jArray)
    {
        UserDataService.UpdateCustomUserData(PLAYER_PREF_SAVED_MERGEABLE_CUBES, jArray.ToString());
    }

}