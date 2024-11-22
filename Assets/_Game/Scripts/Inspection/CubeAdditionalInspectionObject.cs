using IDosGames;
using System;
using System.Linq;
using TMPro;
using UnityEngine;

public class CubeAdditionalInspectionObject : AdditionalInspectionObject
{
	[SerializeField] private SkinData _skinData;
	[SerializeField] private MeshRenderer _meshRenderer;
	[SerializeField] private TMP_Text[] _faces;

	public override void Set(string ItemID)
	{
		string cubeNumber = ItemID.Split("_").Last();

		SetFaces(cubeNumber);
		SetMeshMaterial(cubeNumber);
	}

	private void SetFaces(string cubeNumber)
	{
		foreach (var face in _faces)
		{
			face.text = cubeNumber;
		}
	}

	private void SetMeshMaterial(string cubeNumber)
	{
		Enum.TryParse("_" + cubeNumber, out CubeNumber.Index index);

		var defaultMaterial = _skinData.GetCubeDefaultMaterial(index);

		_meshRenderer.material = defaultMaterial;
	}
}
