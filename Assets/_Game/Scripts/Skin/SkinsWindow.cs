using System;
using System.Linq;
using TMPro;
using UnityEngine;

public class SkinsWindow : MonoBehaviour
{
	[SerializeField] private SkinData _skinData;
	[SerializeField] private TMP_Dropdown _cubeDropdown;
	[SerializeField] private TMP_Dropdown _skinDropdown;
	[SerializeField] private SkinSystem _skinSystem;

	private void Start()
	{
		_cubeDropdown.onValueChanged.AddListener(OnCubeValueChanged);
		_skinDropdown.onValueChanged.AddListener(OnSkinValueChanged);
		OnCubeValueChanged(0);
	}

	private void OnCubeValueChanged(int index)
	{
		var value = _cubeDropdown.options[index].text;

		Enum.TryParse("_" + value, out CubeNumber.Index cubeIndex);

		var cube = _skinData.Cubes.FirstOrDefault(x => x.CubeIndex == cubeIndex);

		var skinIndex = _skinData.SkinCollection.ToList().FindIndex(x => x.Collection == cube.SelectedSkinCollection);

		_skinDropdown.value = skinIndex + 1;
	}

	private void OnSkinValueChanged(int index)
	{
		var skinValue = _skinDropdown.options[index].text;
		Enum.TryParse(skinValue, out CubeSkinCollection collection);

		var cubeValue = _cubeDropdown.options[_cubeDropdown.value].text;
		Enum.TryParse("_" + cubeValue, out CubeNumber.Index cubeIndex);

		var cube = _skinData.Cubes.FirstOrDefault(x => x.CubeIndex == cubeIndex);

		cube.SelectCollection(collection);

		_skinData.UpdateSkinMaterialTexture(cube.CubeIndex);

		foreach (var activeCube in CubesPool.ActiveCubes)
		{
			if (activeCube.Index != cube.CubeIndex)
			{
				continue;
			}

			activeCube.View.SetMaterials(_skinData.GetCubeMaterials(cube.CubeIndex));
		}
	}
}
