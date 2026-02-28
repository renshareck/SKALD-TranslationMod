using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000178 RID: 376
public static class NavigationTools
{
	// Token: 0x06001426 RID: 5158 RVA: 0x000597A0 File Offset: 0x000579A0
	public static float getLinearDistance(SkaldWorldObject object1, SkaldWorldObject object2)
	{
		return NavigationTools.getLinearDistance(object1.getTileX(), object1.getTileY(), object2.getTileX(), object2.getTileY());
	}

	// Token: 0x06001427 RID: 5159 RVA: 0x000597C0 File Offset: 0x000579C0
	public static float getLinearDistance(int x1, int y1, int x2, int y2)
	{
		int num = x1 - x2;
		int num2 = y1 - y2;
		return Mathf.Abs(Mathf.Sqrt((float)(num * num + num2 * num2)));
	}

	// Token: 0x06001428 RID: 5160 RVA: 0x000597E8 File Offset: 0x000579E8
	public static float getLinearDistance(float x1, float y1, float x2, float y2)
	{
		float num = x1 - x2;
		float num2 = y1 - y2;
		return Mathf.Abs(Mathf.Sqrt(num * num + num2 * num2));
	}

	// Token: 0x06001429 RID: 5161 RVA: 0x0005980C File Offset: 0x00057A0C
	public static int getManhattanDistance(int x1, int y1, int x2, int y2)
	{
		int num = Mathf.Abs(x1 - x2);
		int num2 = Mathf.Abs(y1 - y2);
		return num + num2;
	}

	// Token: 0x0600142A RID: 5162 RVA: 0x0005982C File Offset: 0x00057A2C
	public static void setPath(Party party, int targetX, int targetY, bool traverseWater, bool traverseLand, MapTile[,] tileMap, bool returnApproximate)
	{
		NavigationCourse navigationCourse = NavigationTools.findPath(party.getTileX(), party.getTileY(), targetX, targetY, traverseWater, traverseLand, tileMap, returnApproximate);
		if (navigationCourse != null && navigationCourse.hasNodes())
		{
			party.setNavigationCourse(navigationCourse);
		}
	}

	// Token: 0x0600142B RID: 5163 RVA: 0x00059868 File Offset: 0x00057A68
	public static NavigationCourse findPath(int startX, int startY, int targetX, int targetY, bool traverseWater, bool traverseLand, MapTile[,] tileMap, bool returnApproximate)
	{
		NavigationCourse navigationCourse = NavigationTools.findAStarPath(startX, startY, targetX, targetY, traverseWater, traverseLand, tileMap, returnApproximate);
		if (navigationCourse != null && navigationCourse.hasNodes())
		{
			navigationCourse.popFirstNode();
		}
		return navigationCourse;
	}

	// Token: 0x0600142C RID: 5164 RVA: 0x0005989C File Offset: 0x00057A9C
	private static NavigationCourse findAStarPath(int startX, int startY, int targetX, int targetY, bool traverseWater, bool traverseLand, MapTile[,] tileMap, bool returnApproximate)
	{
		NavigationTools.NodeMap nodeMap = new NavigationTools.NodeMap(startX, startY, targetX, targetY, tileMap, traverseWater, traverseLand);
		if (!nodeMap.isMapValid())
		{
			return null;
		}
		List<NavigationTools.Node> list = new List<NavigationTools.Node>();
		NavigationTools.OpenNodeList openNodeList = new NavigationTools.OpenNodeList();
		NavigationTools.Node node = null;
		openNodeList.addNode(nodeMap.getStartNode());
		while (openNodeList.hasNodes())
		{
			node = openNodeList.popNodeWithLowestHeuristic();
			if (node == nodeMap.getTargetNode())
			{
				return NavigationTools.generateCourseFromNode(node);
			}
			list.Add(node);
			List<NavigationTools.Node> neighbourhood = nodeMap.getNeighbourhood(node.gridX, node.gridY);
			List<NavigationTools.Node> list2 = new List<NavigationTools.Node>();
			foreach (NavigationTools.Node node2 in neighbourhood)
			{
				if (node.water || (!node.water && (!node2.water || node2.vehicle)))
				{
					list2.Add(node2);
				}
			}
			foreach (NavigationTools.Node node3 in list2)
			{
				float num = node.distanceFromStart + 1f;
				if (!list.Contains(node3))
				{
					if (openNodeList.contains(node3))
					{
						if (num > node3.distanceFromStart)
						{
							continue;
						}
					}
					else
					{
						openNodeList.addNode(node3);
					}
					node3.parent = node;
					node3.distanceFromStart = num;
				}
			}
		}
		if (returnApproximate && node != null)
		{
			return NavigationTools.generateCourseFromNode(node);
		}
		return null;
	}

	// Token: 0x0600142D RID: 5165 RVA: 0x00059A1C File Offset: 0x00057C1C
	private static NavigationCourse generateCourseFromNode(NavigationTools.Node node)
	{
		NavigationCourse navigationCourse = new NavigationCourse();
		navigationCourse.addNode(node.worldX, node.worldY);
		for (NavigationTools.Node parent = node.parent; parent != null; parent = parent.parent)
		{
			navigationCourse.addNode(parent.worldX, parent.worldY);
		}
		navigationCourse.reverseCourse();
		return navigationCourse;
	}

	// Token: 0x020002B4 RID: 692
	private class OpenNodeList
	{
		// Token: 0x06001B31 RID: 6961 RVA: 0x0007574A File Offset: 0x0007394A
		public OpenNodeList()
		{
			this.nodeList = new List<NavigationTools.Node>();
		}

		// Token: 0x06001B32 RID: 6962 RVA: 0x0007575D File Offset: 0x0007395D
		public bool hasNodes()
		{
			return this.nodeList.Count > 0;
		}

		// Token: 0x06001B33 RID: 6963 RVA: 0x00075770 File Offset: 0x00073970
		public void remove(NavigationTools.Node node)
		{
			this.nodeList.Remove(node);
		}

		// Token: 0x06001B34 RID: 6964 RVA: 0x0007577F File Offset: 0x0007397F
		public void addNode(NavigationTools.Node newNode)
		{
			this.nodeList.Add(newNode);
		}

		// Token: 0x06001B35 RID: 6965 RVA: 0x0007578D File Offset: 0x0007398D
		public bool contains(NavigationTools.Node n)
		{
			return this.nodeList.Contains(n);
		}

		// Token: 0x06001B36 RID: 6966 RVA: 0x0007579C File Offset: 0x0007399C
		public NavigationTools.Node popNodeWithLowestHeuristic()
		{
			if (this.nodeList == null || this.nodeList.Count == 0)
			{
				return null;
			}
			this.bestNode = this.nodeList[0];
			this.lowestScore = this.bestNode.getHeuristicScore();
			foreach (NavigationTools.Node node in this.nodeList)
			{
				float heuristicScore = node.getHeuristicScore();
				if (heuristicScore < this.lowestScore)
				{
					this.lowestScore = heuristicScore;
					this.bestNode = node;
				}
			}
			this.remove(this.bestNode);
			return this.bestNode;
		}

		// Token: 0x040009F7 RID: 2551
		private List<NavigationTools.Node> nodeList;

		// Token: 0x040009F8 RID: 2552
		private NavigationTools.Node bestNode;

		// Token: 0x040009F9 RID: 2553
		private float lowestScore;
	}

	// Token: 0x020002B5 RID: 693
	private class NodeMap
	{
		// Token: 0x06001B37 RID: 6967 RVA: 0x00075854 File Offset: 0x00073A54
		public NodeMap(int startX, int startY, int targetX, int targetY, MapTile[,] tileMap, bool traverseWater, bool traverseLand)
		{
			this.generateNodeMap(startX, startY, targetX, targetY, tileMap, traverseWater, traverseLand);
		}

		// Token: 0x06001B38 RID: 6968 RVA: 0x00075870 File Offset: 0x00073A70
		private bool isTargetNodeAcessible()
		{
			if (this.targetNode == null)
			{
				return false;
			}
			using (List<NavigationTools.Node>.Enumerator enumerator = this.getNeighbourhood(this.targetNode.gridX, this.targetNode.gridY).GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.passable)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06001B39 RID: 6969 RVA: 0x000758EC File Offset: 0x00073AEC
		private void generateNodeMap(int startX, int startY, int targetX, int targetY, MapTile[,] tileMap, bool traverseWater, bool traverseLand)
		{
			NavigationTools.Node[,] array = new NavigationTools.Node[tileMap.GetLength(0), tileMap.GetLength(1)];
			for (int i = 0; i < tileMap.GetLength(0); i++)
			{
				for (int j = 0; j < tileMap.GetLength(1); j++)
				{
					MapTile mapTile = tileMap[i, j];
					if (mapTile != null)
					{
						NavigationTools.Node node = new NavigationTools.Node(i, j, mapTile, targetX, targetY);
						if (!mapTile.isPassable())
						{
							node.passable = false;
						}
						else if (mapTile.getParty() != null)
						{
							node.passable = false;
						}
						else if (!traverseWater && node.water && !node.vehicle)
						{
							node.passable = false;
						}
						else if (!traverseLand && !node.water)
						{
							node.passable = false;
						}
						if (mapTile.getTileX() == startX && mapTile.getTileY() == startY)
						{
							this.setStartNode(node);
						}
						if (mapTile.getTileX() == targetX && mapTile.getTileY() == targetY)
						{
							this.setTargetNode(node);
						}
						array[i, j] = node;
					}
				}
			}
			this.nodes = array;
		}

		// Token: 0x06001B3A RID: 6970 RVA: 0x000759FD File Offset: 0x00073BFD
		private void setTargetNode(NavigationTools.Node node)
		{
			this.targetNode = node;
			this.targetNode.passable = true;
		}

		// Token: 0x06001B3B RID: 6971 RVA: 0x00075A12 File Offset: 0x00073C12
		private void setStartNode(NavigationTools.Node node)
		{
			this.startNode = node;
			this.startNode.passable = true;
		}

		// Token: 0x06001B3C RID: 6972 RVA: 0x00075A27 File Offset: 0x00073C27
		public NavigationTools.Node getStartNode()
		{
			return this.startNode;
		}

		// Token: 0x06001B3D RID: 6973 RVA: 0x00075A2F File Offset: 0x00073C2F
		public NavigationTools.Node getTargetNode()
		{
			return this.targetNode;
		}

		// Token: 0x06001B3E RID: 6974 RVA: 0x00075A38 File Offset: 0x00073C38
		private string printDebug(NavigationTools.Node[,] _nodes)
		{
			string text = "";
			for (int i = 0; i < _nodes.GetLength(0); i++)
			{
				for (int j = 0; j < _nodes.GetLength(1); j++)
				{
					NavigationTools.Node node = _nodes[i, j];
					if (node == null)
					{
						text += "N";
					}
					else if (node == this.targetNode)
					{
						text += "T";
					}
					else if (node == this.startNode)
					{
						text += "S";
					}
					else if (node.passable)
					{
						text += ".";
					}
					else
					{
						text += "X";
					}
				}
				text += "\n";
			}
			MainControl.log(text);
			return text;
		}

		// Token: 0x06001B3F RID: 6975 RVA: 0x00075AF4 File Offset: 0x00073CF4
		public List<NavigationTools.Node> getNeighbourhood(int x, int y)
		{
			List<NavigationTools.Node> list = new List<NavigationTools.Node>();
			if (this.isTileValid(x + 1, y))
			{
				list.Add(this.nodes[x + 1, y]);
			}
			if (this.isTileValid(x - 1, y))
			{
				list.Add(this.nodes[x - 1, y]);
			}
			if (this.isTileValid(x, y + 1))
			{
				list.Add(this.nodes[x, y + 1]);
			}
			if (this.isTileValid(x, y - 1))
			{
				list.Add(this.nodes[x, y - 1]);
			}
			return list;
		}

		// Token: 0x06001B40 RID: 6976 RVA: 0x00075B8C File Offset: 0x00073D8C
		private bool isTileValid(int x, int y)
		{
			return this.nodes != null && (x >= 0 && x < this.nodes.GetLength(0) && y >= 0 && y < this.nodes.GetLength(1) && this.nodes[x, y] != null && this.nodes[x, y].passable);
		}

		// Token: 0x06001B41 RID: 6977 RVA: 0x00075BEF File Offset: 0x00073DEF
		public bool isMapValid()
		{
			return this.isTargetNodeAcessible() && this.startNode != null && this.targetNode != null && this.nodes != null;
		}

		// Token: 0x040009FA RID: 2554
		private NavigationTools.Node[,] nodes;

		// Token: 0x040009FB RID: 2555
		private NavigationTools.Node startNode;

		// Token: 0x040009FC RID: 2556
		private NavigationTools.Node targetNode;
	}

	// Token: 0x020002B6 RID: 694
	private class Node
	{
		// Token: 0x06001B42 RID: 6978 RVA: 0x00075C14 File Offset: 0x00073E14
		public Node(int gridX, int gridY, MapTile mapTile, int targetX, int targetY)
		{
			this.gridX = gridX;
			this.gridY = gridY;
			this.worldX = mapTile.getTileX();
			this.worldY = mapTile.getTileY();
			this.water = mapTile.isWater();
			this.vehicle = mapTile.hasVehicle();
			this.distanceToTarget = (float)NavigationTools.getManhattanDistance(this.worldX, this.worldY, targetX, targetY);
		}

		// Token: 0x06001B43 RID: 6979 RVA: 0x00075C88 File Offset: 0x00073E88
		public float getHeuristicScore()
		{
			return this.distanceFromStart + this.distanceToTarget;
		}

		// Token: 0x040009FD RID: 2557
		public int gridX;

		// Token: 0x040009FE RID: 2558
		public int gridY;

		// Token: 0x040009FF RID: 2559
		public int worldX;

		// Token: 0x04000A00 RID: 2560
		public int worldY;

		// Token: 0x04000A01 RID: 2561
		public bool passable = true;

		// Token: 0x04000A02 RID: 2562
		public bool water;

		// Token: 0x04000A03 RID: 2563
		public bool vehicle;

		// Token: 0x04000A04 RID: 2564
		public float distanceToTarget;

		// Token: 0x04000A05 RID: 2565
		public float distanceFromStart;

		// Token: 0x04000A06 RID: 2566
		public NavigationTools.Node parent;
	}
}
