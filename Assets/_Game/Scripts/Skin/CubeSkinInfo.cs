using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CubeSkinInfo
{
	[SerializeField] public string _name;
	[SerializeField] private CubeNumber.Index _cubeIndex;
	[SerializeField] private CubeSkinCollection _selectedSkinCollection;
	[SerializeField] private Material _defaultMaterial;
	[SerializeField] private Material _skinMaterial;
	[SerializeField] private List<CubeRenderInfo> _renders;

	public CubeNumber.Index CubeIndex => _cubeIndex;
	public CubeSkinCollection SelectedSkinCollection => _selectedSkinCollection;
	public Material DefaultMaterial => _defaultMaterial;
	public Material SkinMaterial => _skinMaterial;
	public List<CubeRenderInfo> Renders => _renders;

	public void SelectCollection(CubeSkinCollection collection)
	{
		_selectedSkinCollection = collection;
	}

	public void SetSkinMaterialTexture(Texture texture)
	{
		_skinMaterial.mainTexture = texture;
	}
}
