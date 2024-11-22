using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SkinData : ScriptableObject
{
	[SerializeField] private List<CubeSkinInfo> _cubes;
	public IReadOnlyList<CubeSkinInfo> Cubes => _cubes.AsReadOnly();

	[SerializeField] private List<CubeSkinTextureInfo> _skinCollection;
	public IReadOnlyList<CubeSkinTextureInfo> SkinCollection => _skinCollection.AsReadOnly();

	public Material[] GetCubeMaterials(CubeNumber.Index index)
	{
		var cube = _cubes.First(x => x.CubeIndex == index);

		Material[] materials = { cube.DefaultMaterial };

		if (cube.SelectedSkinCollection != CubeSkinCollection.Default)
		{
			if (cube.SkinMaterial != null)
			{
				materials = materials.Append(cube.SkinMaterial).ToArray();
			}
		}

		return materials;
	}

	public Material GetCubeDefaultMaterial(CubeNumber.Index index)
	{
		var cube = _cubes.First(x => x.CubeIndex == index);

		return cube.DefaultMaterial;
	}

	public void UpdateSkinMaterialTexture(CubeNumber.Index index)
	{
		var cube = _cubes.First(x => x.CubeIndex == index);

		var skinTexture = _skinCollection.FirstOrDefault(x => x.Collection == cube.SelectedSkinCollection)?.Texture;

		cube.SetSkinMaterialTexture(skinTexture);
	}
}