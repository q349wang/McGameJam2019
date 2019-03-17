using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace MapGen
{
    public class RectWall
    {
        public RectWall()
        {
            north = new List<GameObject>();
            south = new List<GameObject>();
            east = new List<GameObject>();
            west = new List<GameObject>();
        }

        public List<GameObject> north { get; set; }
        public List<GameObject> south { get; set; }
        public List<GameObject> east { get; set; }
        public List<GameObject> west { get; set; }
    }

    public class MapSpace
    {
        private GameObject _obstacle;
        public MapSpace()
        {
            x = 0;
            y = 0;
            isObstacle = false;
            obstacle = null;
        }
        public MapSpace(int x, int y)
        {
            this.x = x;
            this.y = y;
            isObstacle = false;
            obstacle = null;
        }

        public int x { get; set; }
        public int y { get; set; }
        public bool isObstacle { get; set; }
        public GameObject obstacle
        {
            get { return this._obstacle; }
            set
            {
                this._obstacle = value;
                this.isObstacle = value != null;
            }
        }
    }

    public class GenerateMap : MonoBehaviour
    {

        public int mapHeight;
        public int mapWidth;
        public int mapCellSize;
        public int mapCellDepth;
        public int rowGap;
        public int colGap;

        public int maxObstacles;
        public int minObstacles;


        public GameObject background;
        [SerializeField] private List<GameObject> pickups;

        [SerializeField] private List<GameObject> possibleObstacles;
        private List<GameObject> obstacles;
        private RectWall walls;
        private MapSpace[,] mapSpaces;
        private List<GameObject> manaPickups;
        private List<Tuple<GameObject, int>> weaponPickups;
        private static readonly System.Random rng = new System.Random();

        [SerializeField] private int groupMin;
        [SerializeField] private int groupMax;
        // Use this for initialization
        void Start()
        {
            background.SetActive(true);
            background.GetComponent<SpriteRenderer>().size = new Vector2(mapWidth, mapHeight);
            background.transform.Translate(new Vector2((float)mapWidth / 2, (float)mapHeight / 2));
            obstacles = new List<GameObject>();
            walls = new RectWall();
            mapSpaces = new MapSpace[mapWidth, mapHeight];
            manaPickups = new List<GameObject>();
            weaponPickups = new List<Tuple<GameObject, int>>();
            GenerateWallsRect(0f, 0f, mapWidth, mapHeight);
            GenerateMazeScuffedRecursiveDiv(0, 0, mapWidth, mapHeight, mapCellSize, ref mapSpaces, mapCellDepth);
            GeneratePickups(10, 15, 10, 15, 0, 0, mapWidth, mapHeight, mapCellSize);
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown("space"))
            {
                for (int i = 0; i < obstacles.Count; i++)
                {
                    Destroy(obstacles[i]);
                }
                obstacles.Clear();
                GenerateMazeScuffedRecursiveDiv(0, 0, mapWidth, mapHeight, mapCellSize, ref mapSpaces, mapCellDepth);
            }
        }

        // Generates a maze starting at x, y with width w and height h, and cellSize. Pass in map as ref to keep track of objects and stack size to indicate how many recursions
        private void GenerateMazeScuffedRecursiveDiv(int x, int y, int w, int h, int cellSize, ref MapSpace[,] map, int stackSize)
        {
            for (int i = 0; i < w; i++)
            {
                for (int j = 0; j < h; j++)
                {
                    map[i, j] = new MapSpace(x + i, y + j);
                }
            }
            GenerateMazeScuffedRecursiveDivHelper(x, y, w, h, cellSize, ref map, stackSize, 0);
        }
        private void GenerateMazeScuffedRecursiveDivHelper(int x, int y, int w, int h, int cellSize, ref MapSpace[,] map, int stackSize, int currStack)
        {
            if (currStack == stackSize)
            {
                return;
            }
            if (w / cellSize <= 1 && h / cellSize <= 1)
            {
                return;
            }

            int cellWidth = (w / cellSize <= 0) ? 1 : w / cellSize;
            int cellHeight = (h / cellSize <= 0) ? 1 : h / cellSize;

            int ranX = rng.Next(0, cellWidth - 1);
            int ranY = rng.Next(0, cellHeight - 1);
            int holeN = 0;
            int holeE = 0;
            if (cellHeight != 1)
            {
                holeN = rng.Next(ranY + 1, cellHeight);
            }
            if (cellWidth != 1)
            {
                holeE = rng.Next(ranX + 1, cellWidth);
            }
            int holeS = rng.Next(0, ranY + 1);
            int holeW = rng.Next(0, ranX + 1);

            int[] holeHor = new int[2];
            holeHor[0] = holeE;
            holeHor[1] = holeW;
            int[] holeVer = new int[2];
            holeVer[0] = holeN;
            holeVer[1] = holeS;

            int chooseRemove = rng.Next(0, 4);
            if (cellHeight != 1 && cellWidth != 1)
            {
                switch (chooseRemove)
                {
                    case 0:
                        holeVer[0] = holeS;
                        break;
                    case 1:
                        holeVer[1] = holeN;
                        break;
                    case 2:
                        holeHor[0] = holeW;
                        break;
                    case 3:
                        holeHor[0] = holeE;
                        break;

                }
            }

            if (cellHeight != 1)
            {
                int obsIndex = rng.Next(0, possibleObstacles.Count);
                int toGen = rng.Next(groupMin, groupMax);
                for (int i = 0; i < cellWidth; i++)
                {
                    if (toGen == 0)
                    {
                        obsIndex = rng.Next(0, possibleObstacles.Count);
                        toGen = rng.Next(groupMin, groupMax);
                    }
                    for (int j = 0; j < cellSize; j++)
                    {
                        if (toGen == 0)
                        {
                            obsIndex = rng.Next(0, possibleObstacles.Count);
                            toGen = rng.Next(groupMin, groupMax);
                        }
                        int trueY = ranY * cellSize + cellSize - 1;
                        int trueX = i * cellSize + j;
                        if (i == holeHor[0] || i == holeHor[1])
                        {
                            if (j == cellSize - 1 && i != cellWidth - 1)
                            {
                                Destroy(map[trueX, trueY]);
                                GameObject generate = GenerateSpecificObstacle(trueX + x, trueY + y, obsIndex);
                                map[trueX, trueY].obstacle = generate;
                                toGen--;
                            }
                        }
                        else
                        {
                            Destroy(map[trueX, trueY]);
                            GameObject generate = GenerateSpecificObstacle(trueX + x, trueY + y, obsIndex);
                            map[trueX, trueY].obstacle = generate;
                            toGen--;
                        }
                    }
                }
            }

            if (cellWidth != 1)
            {
                int obsIndex = rng.Next(0, possibleObstacles.Count);
                int toGen = rng.Next(groupMin, groupMax);
                for (int i = 0; i < cellHeight; i++)
                {
                    if (toGen == 0)
                    {
                        obsIndex = rng.Next(0, possibleObstacles.Count);
                        toGen = rng.Next(groupMin, groupMax);
                    }
                    for (int j = 0; j < cellSize; j++)
                    {
                        if (toGen == 0)
                        {
                            obsIndex = rng.Next(0, possibleObstacles.Count);
                            toGen = rng.Next(groupMin, groupMax);
                        }
                        int trueX = ranX * cellSize + cellSize - 1;
                        int trueY = i * cellSize + j;
                        if (i == holeVer[0] || i == holeVer[1])
                        {
                            if (j == cellSize - 1 && i != cellHeight - 1)
                            {
                                Destroy(map[trueX, trueY]);
                                GameObject generate = GenerateSpecificObstacle(trueX + x, trueY + y, obsIndex);
                                map[trueX, trueY].obstacle = generate;
                                toGen--;
                            }
                        }
                        else
                        {
                            Destroy(map[trueX, trueY]);
                            GameObject generate = GenerateSpecificObstacle(trueX + x, trueY + y, obsIndex);
                            map[trueX, trueY].obstacle = generate;
                            toGen--;
                        }
                    }
                }
            }
            currStack++;
            GenerateMazeScuffedRecursiveDivHelper(x, y, cellSize * (ranX + 1), cellSize * (ranY + 1), cellSize, ref map, stackSize, currStack);
            GenerateMazeScuffedRecursiveDivHelper(x + cellSize * (ranX + 1), y, w - cellSize * (ranX + 1), cellSize * (ranY + 1), cellSize, ref map, stackSize, currStack);
            GenerateMazeScuffedRecursiveDivHelper(x, y + cellSize * (ranY + 1), cellSize * (ranX + 1), h - cellSize * (ranY + 1), cellSize, ref map, stackSize, currStack);
            GenerateMazeScuffedRecursiveDivHelper(x + cellSize * (ranX + 1), y + cellSize * (ranY + 1), w - cellSize * (ranX + 1), h - cellSize * (ranY + 1), cellSize, ref map, stackSize, currStack);
        }

        private void GenerateWallsRect(float x, float y, int w, int h)
        {
            for (int i = 0; i < w + 2; i++)
            {
                GameObject generated = Instantiate(possibleObstacles[0] as GameObject);
                generated.SetActive(true);
                generated.transform.Translate(new Vector2(x + i - 1, y - 1));
                walls.south.Add(generated);
                generated = Instantiate(generated as GameObject);
                generated.transform.Translate(new Vector2(0, h + 1));
                walls.north.Add(generated);
            }
            for (int i = 0; i < h; i++)
            {
                GameObject generated = Instantiate(possibleObstacles[0] as GameObject);
                generated.SetActive(true);
                generated.transform.Translate(new Vector2(x - 1, y + i));
                walls.west.Add(generated);
                generated = Instantiate(generated as GameObject);
                generated.transform.Translate(new Vector2(w + 1, 0));
                walls.east.Add(generated);
            }
        }

        private GameObject GenerateObstacle(int x, int y)
        {
            int toGen = rng.Next(0, 1);
            if (toGen == 0)
            {
                GameObject generated = Instantiate(possibleObstacles[rng.Next(0, 3)] as GameObject);
                generated.SetActive(true);
                generated.transform.Translate(new Vector2(x, y));
                obstacles.Add(generated);
                return generated;
            }

            return null;
        }

        private GameObject GenerateSpecificObstacle(int x, int y, int index)
        {
            int toGen = rng.Next(0, 1);
            if (toGen == 0)
            {
                GameObject generated = Instantiate(possibleObstacles[index] as GameObject);
                generated.SetActive(true);
                generated.transform.Translate(new Vector2(x, y));
                obstacles.Add(generated);
                return generated;
            }

            return null;
        }

        private void Destroy(MapSpace space)
        {
            if (space.obstacle != null)
            {
                Destroy(space.obstacle);
                space.obstacle = null;
                space.isObstacle = false;
            }
        }

        private void GeneratePickups(int minMana, int maxMana, int minWeap, int maxWeap, int x, int y, int w, int h, int cellSize)
        {
            int numMana = rng.Next(minMana, maxMana);
            int numWeap = rng.Next(minWeap, maxWeap);
            int cellWidth = w / cellSize;
            int cellHeight = h / cellSize;
            int[,] mapGrid = new int[cellWidth, cellHeight];

            List<Tuple<int, int>> possiblePos = new List<Tuple<int, int>>();
            for (int i = 0; i < cellWidth; i++)
            {
                for (int j = 0; j < cellHeight; j++)
                {
                    possiblePos.Add(new Tuple<int, int>(i, j));
                }
            }
            for (int i = 0; i < numMana; i++)
            {
                if (possiblePos.Count > 0)
                {
                    int random = rng.Next(0, possiblePos.Count);
                    float trueX = (float) possiblePos[random].Item1 * cellSize + ((float) cellSize) / 2 + (float) x;
                    float trueY = (float) possiblePos[random].Item2 * cellSize + ((float) cellSize) / 2 + (float) y;
                    GameObject generated = Instantiate(pickups[0], new Vector2(trueX, trueY), Quaternion.identity);
                    generated.SetActive(true);
                    possiblePos.RemoveAt(random);
                    manaPickups.Add(generated);
                }

            }

            for (int i = 0; i < numWeap; i++)
            {
                if (possiblePos.Count > 0)
                {
                    int random = rng.Next(0, possiblePos.Count);
                    float trueX = (float) possiblePos[random].Item1 * cellSize + ((float) cellSize) / 2 + (float) x;
                    float trueY = (float) possiblePos[random].Item2 * cellSize + ((float) cellSize) / 2 + (float) y;
                    int weap = rng.Next(1, 4);
                    GameObject generated = Instantiate(pickups[weap], new Vector2(trueX, trueY), Quaternion.identity);
                    generated.SetActive(true);
                    possiblePos.RemoveAt(random);
                    weaponPickups.Add(new Tuple<GameObject, int>(generated, weap));
                }

            }
        }
    }
}
