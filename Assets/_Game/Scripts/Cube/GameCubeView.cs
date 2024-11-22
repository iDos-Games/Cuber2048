using TMPro;
using UnityEngine;

public class GameCubeView : MonoBehaviour
{
	[SerializeField] private MeshRenderer _mesh;
	[SerializeField] private GameObject _aimLine;
	[SerializeField] private TMP_Text[] _faces;

	public bool IsAiming { get; private set; }

	public void SetActiveAim(bool active)
	{
		IsAiming = active;
		_aimLine.SetActive(active);
	}

	public void SetFaces(CubeNumber.Index number)
	{
		var text = CubeNumber.GetDisplayText(number);

		foreach (var face in _faces)
		{
			face.text = text;
		}
	}

	public void SetMaterials(Material[] materials)
	{
		_mesh.materials = materials;
	}
}
