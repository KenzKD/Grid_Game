using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface AI
{
    IEnumerator MoveToTile();
    IEnumerator FollowPath();
    List<TileInformation> GetNeighbors(TileInformation tile);
    List<Vector2Int> CalculatePath();
} 