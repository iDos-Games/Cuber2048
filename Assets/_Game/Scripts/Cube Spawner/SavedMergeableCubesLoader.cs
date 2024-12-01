using IDosGames;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using UnityEngine;

public class SavedMergeableCubesLoader : MonoBehaviour
{
	private const string NAME_SAVED_MERGEABLE_CUBES = "SAVED_MERGEABLE_CUBES";

	[SerializeField] private GameCubeSpawner _spawner;

	private void OnEnable()
	{
		Playground.GameOver += OnGameOver;
        UserDataService.FirstTimeDataUpdated += LoadBoard;

#if UNITY_WEBGL
        if (WebFunctionHandler.Instance != null)
        {
            WebFunctionHandler.Instance.OnWebAppQuitEvent += SaveToServer;
        }
#endif
    }

    private void OnDisable()
	{
		Playground.GameOver -= OnGameOver;
        UserDataService.FirstTimeDataUpdated -= LoadBoard;

#if UNITY_WEBGL
        if (WebFunctionHandler.Instance != null)
        {
            WebFunctionHandler.Instance.OnWebAppQuitEvent -= SaveToServer;
        }
#endif
    }

	private void OnApplicationFocus(bool focus)
	{
		if (!focus)
		{
            SaveToServer();
        }
	}

	private void OnApplicationQuit()
	{
        SaveToServer();
    }

    private IEnumerator AutoSaveCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(60);
            SaveToServer(); 
        }
    }

    private void OnGameOver()
	{
        SaveJArrayToServer(new JArray());
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

    private void LoadBoard()
    {
        Invoke(nameof(LoadFromServer), 0.5f);
    }

    private void LoadFromServer()
    {
        var loadedData = UserDataService.GetCachedCustomUserData(NAME_SAVED_MERGEABLE_CUBES);

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

    private void SaveJArrayToServer(JArray jArray)
    {
        UserDataService.UpdateCustomUserData(NAME_SAVED_MERGEABLE_CUBES, jArray.ToString());
    }

}