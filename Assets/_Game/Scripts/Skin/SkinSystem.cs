using IDosGames;
using System;
using System.Linq;
using UnityEngine;

public class SkinSystem : MonoBehaviour
{
	[SerializeField] private SkinData _skinData;

	public SkinData SkinData => _skinData;

	private void OnEnable()
	{
        UserDataService.SkinCatalogItemsUpdated += UpdateSkinMaterials;
        UserDataService.EquippedSkinsUpdated += UpdateSkinMaterials;
	}

	private void OnDisable()
	{
        UserDataService.SkinCatalogItemsUpdated -= UpdateSkinMaterials;
        UserDataService.EquippedSkinsUpdated -= UpdateSkinMaterials;
	}

	private void UpdateSkinMaterials()
	{
		var equippedSkins = UserDataService.EquippedSkins;

		foreach (var cube in _skinData.Cubes)
		{
			if (cube.CubeIndex > CubeNumber.Index._2048)
			{
				break;
			}

			string skinItemID = GetSkinItemIDFromCube(cube);

			if (equippedSkins.Contains(skinItemID))
			{
				var skinItem = UserDataService.GetCachedSkinItem(skinItemID);

				if (skinItem == null)
				{
					continue;
				}

				if (skinItem.Collection == cube.SelectedSkinCollection.ToString())
				{
					continue;
				}

				Debug.Log("UpdateSkinMaterials from equipped list: " + cube.CubeIndex);

				Enum.TryParse(skinItem.Collection, out CubeSkinCollection skinCollection);

				cube.SelectCollection(skinCollection);
				_skinData.UpdateSkinMaterialTexture(cube.CubeIndex);

				SetActiveCubesMaterials(cube);
			}
			else
			{
				if (cube.SelectedSkinCollection == CubeSkinCollection.Default)
				{
					continue;
				}

				Debug.Log("UpdateSkinMaterials to Default: " + cube.CubeIndex);

				cube.SelectCollection(CubeSkinCollection.Default);

				SetActiveCubesMaterials(cube);
			}
		}

		foreach (var equippedSkinID in equippedSkins)
		{
			var skinItem = UserDataService.GetCachedSkinItem(equippedSkinID);
			var cube = GetCubeFromSkinItemID(equippedSkinID);

			if (skinItem == null)
			{
				continue;
			}

			if (skinItem.Collection == cube.SelectedSkinCollection.ToString())
			{
				continue;
			}

			Debug.Log("UpdateSkinMaterials from equipped list: " + cube.CubeIndex);

			Enum.TryParse(skinItem.Collection, out CubeSkinCollection skinCollection);

			cube.SelectCollection(skinCollection);
			_skinData.UpdateSkinMaterialTexture(cube.CubeIndex);

			SetActiveCubesMaterials(cube);
		}
	}

	private void SetActiveCubesMaterials(CubeSkinInfo cube)
	{
		foreach (var activeCube in CubesPool.ActiveCubes)
		{
			if (activeCube.Index != cube.CubeIndex)
			{
				continue;
			}

			activeCube.View.SetMaterials(_skinData.GetCubeMaterials(cube.CubeIndex));
		}
	}

	private string GetSkinItemIDFromCube(CubeSkinInfo cube)
	{
		return $"skin_{cube.SelectedSkinCollection.ToString().ToLower()}{cube.CubeIndex}";
	}

	private CubeSkinInfo GetCubeFromSkinItemID(string itemID)
	{
		string cubeIndex = itemID.Split("_").Last();

		return _skinData.Cubes.FirstOrDefault(x => x.CubeIndex.ToString() == $"_{cubeIndex}");
	}
}
