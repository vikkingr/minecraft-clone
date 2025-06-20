using UnityEngine;

public class World : MonoBehaviour
{
	public Transform player;
	public Vector3 spawnPosition;

	public Material material;
	public BlockType[] blockTypes;

	Chunk[,] chunks = new Chunk[VoxelData.WorldSizeInChunks, VoxelData.WorldSizeInChunks];

	private void Start()
	{
		spawnPosition = new Vector3((VoxelData.WorldSizeInChunks * VoxelData.ChunkWidth) / 2f, VoxelData.ChunkHeight + 2f, (VoxelData.WorldSizeInChunks * VoxelData.ChunkWidth) / 2f);
		GenerateWorld();
	}

	void GenerateWorld()
	{
		for (int x = (VoxelData.WorldSizeInChunks / 2) - VoxelData.ViewDistanceInChunks; x < (VoxelData.WorldSizeInChunks / 2) + VoxelData.ViewDistanceInChunks; x++)
		{
			for (int z = (VoxelData.WorldSizeInChunks / 2) - VoxelData.ViewDistanceInChunks; z < (VoxelData.WorldSizeInChunks / 2) + VoxelData.ViewDistanceInChunks; z++)
			{
				CreateNewChunk(x, z);
			}
		}

		player.position = spawnPosition;
	}

	public byte GetVoxel(Vector3 pos)
	{
		if (!IsVoxelInWorld(pos))
		{
			return 0;
		}
		else if (pos.y < 1)
		{
			return 1;
		}
		else if (pos.y == VoxelData.ChunkHeight - 1)
		{
			return 3;
		}
		else
		{
			return 2;
		}
	}

	void CreateNewChunk(int x, int z)
	{
		chunks[x, z] = new Chunk(new ChunkCoord(x, z), this);
	}

	bool IsChunkInWorld(ChunkCoord coord)
	{
		if (coord.x > 0 && coord.x < VoxelData.WorldSizeInChunks - 1
			&& coord.z > 0 && coord.z < VoxelData.WorldSizeInChunks - 1)
		{
			return true;
		}
		else
		{
			return false;
		}
	}

	bool IsVoxelInWorld(Vector3 pos)
	{
		if (pos.x >= 0 && pos.x < VoxelData.WorldSizeInVoxels
			&& pos.y >= 0 && pos.y < VoxelData.ChunkHeight
			&& pos.z >= 0 && pos.z < VoxelData.WorldSizeInVoxels)
		{
			return true;
		}
		else
		{
			return false;
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
