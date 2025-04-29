using UnityEngine;

public class World : MonoBehaviour
{
	public Material material;
	public BlockType[] blockTypes;

	private void Start()
	{
		GenerateWorld();
	}

	void GenerateWorld()
	{
		for (int x = 0; x < VoxelData.WorldSizeInChunks; x++)
		{
			for (int z = 0; z < VoxelData.WorldSizeInChunks; z++)
			{
				Chunk newChunk = new Chunk(new ChunkCoord(x, z), this);
			}
		}
	}

	[System.Serializable]
	public class BlockType
	{
		public string blockName;
		public bool isSolid;

		[Header("Texture Values")]
		public int backFaceTexture;
		public int frontFaceTexture;
		public int topFaceTexture;
		public int botttomFaceTexture;
		public int leftFaceTexture;
		public int rightFaceTexture;

		// Back, Front, Top, Bottom, Left, Right
		public int GetTextureId(int faceIndex)
		{
			switch (faceIndex)
			{
				case 0:
					return backFaceTexture;
				case 1:
					return frontFaceTexture;
				case 2:
					return topFaceTexture;
				case 3:
					return botttomFaceTexture;
				case 4:
					return leftFaceTexture;
				case 5:
					return rightFaceTexture;
				default:
					Debug.Log($"Error in {nameof(this.GetTextureId)}; Invalid face index!");
					return 0;
			}
		}
	}
}
