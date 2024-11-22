using System;
using UnityEngine;

[Serializable]
public class CubeSkinTextureInfo
{
	[SerializeField] private string _name;
	[SerializeField] private CubeSkinCollection _collection;
	public CubeSkinCollection Collection => _collection;

	[SerializeField] private Texture _texture;
	public Texture Texture => _texture;
}
