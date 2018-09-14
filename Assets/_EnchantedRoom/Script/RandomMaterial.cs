using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct ButterflySet
{
	public Texture diffuse;
	public Texture normal;
}

public class RandomMaterial : MonoBehaviour
{
	public SkinnedMeshRenderer _renderer;
	public ButterflySet[] butteryflySets;

	private MaterialPropertyBlock _propBlock;

	void Awake()
	{
		_propBlock = new MaterialPropertyBlock();
		_renderer = GetComponent<SkinnedMeshRenderer>();

		int setIndex = Random.Range(0, butteryflySets.Length);
		ChangeTexture(setIndex);
	}

	private void ChangeTexture(int i)
	{
		// Get the property block.
		_renderer.GetPropertyBlock(_propBlock);

		// Assign our new values
		_propBlock.SetTexture("_Diffuse", butteryflySets[i].diffuse);
		_propBlock.SetTexture("_Normal", butteryflySets[i].normal);

		// Apply the edited values to the renderer.
		_renderer.SetPropertyBlock(_propBlock);
	}
}
