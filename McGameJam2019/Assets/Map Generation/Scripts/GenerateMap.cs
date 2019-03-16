using System.Collections;
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

    public class GenerateMap : MonoBehaviour
    {

        public int mapHeight;
        public int mapWidth;

        public int rowGap;
        public int colGap;

        public int maxObstacles;
        public int minObstacles;

        public GameObject crate;
        public GameObject barrel;
        public GameObject wall;

        private List<GameObject> obstacles;
        private RectWall walls;
        private static readonly System.Random rng = new System.Random();
        // Use this for initialization
        void Start()
        {
            obstacles = new List<GameObject>();
            walls = new RectWall();
            GenerateObstaclesRowPriorityCenterOut(rng.Next(minObstacles, maxObstacles + 1));
            GenerateWallsRect(0f, 0f, mapWidth, mapHeight);
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
                GenerateObstaclesRowPriorityCenterOut(rng.Next(minObstacles, maxObstacles + 1));
            }
        }

        private void GenerateWallsRect(float x, float y, int w, int h)
        {
            for (int i = 0; i < w + 2; i++)
            {
                GameObject generated = Instantiate(wall as GameObject);
                generated.SetActive(true);
                generated.transform.Translate(new Vector2(x + i - 1, y - 1));
                walls.south.Add(generated);
                generated = Instantiate(generated as GameObject);
                generated.transform.Translate(new Vector2(0, h + 1));
                walls.north.Add(generated);
            }
            for (int i = 0; i < h; i++)
            {
                GameObject generated = Instantiate(wall as GameObject);
                generated.SetActive(true);
                generated.transform.Translate(new Vector2(x - 1, y + i));
                walls.west.Add(generated);
                generated = Instantiate(generated as GameObject);
                generated.transform.Translate(new Vector2(w + 1, 0));
                walls.east.Add(generated);
            }
        }

        private void GenerateObstaclesRadial(int maxNumToGen)
        {
            Debug.Log("GenerateObstacles: " + maxNumToGen);
            int[] didGenCol = new int[mapWidth];
            int[] didGenRow = new int[mapHeight];
            int leftToGen = maxNumToGen;
            int leftY = mapHeight / 2;
            int rightY = mapHeight / 2 + 1;
            while ((leftY >= 0 || rightY <= mapHeight - 1) && leftToGen > 0)
            {
                if (leftY >= 0 && leftToGen > 0)
                {
                    leftToGen -= GenerateObstacleRowCenterOut(leftY--, leftToGen);
                }
                if (rightY <= mapHeight - 1 && leftToGen > 0)
                {
                    leftToGen -= GenerateObstacleRowCenterOut(rightY++, leftToGen);
                }
            }
        }

        private void GenerateObstaclesRowPriorityCenterOut(int maxNumToGen)
        {
            Debug.Log("GenerateObstacles: " + maxNumToGen);
            int[] columnsGen = new int[mapWidth];
            int leftToGen = maxNumToGen;
            int leftY = mapHeight / 2;
            int rightY = mapHeight / 2 + 1;
            while ((leftY >= 0 || rightY <= mapHeight - 1) && leftToGen > 0)
            {
                if (leftY >= 0 && leftToGen > 0)
                {
                    leftToGen -= GenerateObstacleRowCenterOut(leftY--, leftToGen);
                }
                if (rightY <= mapHeight - 1 && leftToGen > 0)
                {
                    leftToGen -= GenerateObstacleRowCenterOut(rightY++, leftToGen);
                }
            }
        }
        private int GenerateObstacleRowCenterOut(int rowNum, int maxNumToGen)
        {
            int didGen = 0;
            int leftX = mapWidth / 2;
            int rightX = mapWidth / 2 + 1;
            bool didGenerate = false;
            while ((leftX >= 0 || rightX <= mapWidth - 1) && didGen < maxNumToGen && mapWidth - didGen > rowGap)
            {
                if (leftX >= 0 && didGen < maxNumToGen && mapWidth - didGen > rowGap)
                {
                    didGenerate = GenerateObstacle(leftX--, rowNum);
                    if (didGenerate)
                    {
                        didGen++;
                    }
                }
                if (rightX <= mapWidth - 1 && didGen < maxNumToGen && mapWidth - didGen > rowGap)
                {
                    didGenerate = GenerateObstacle(rightX++, rowNum);
                    if (didGenerate)
                    {
                        didGen++;
                    }
                }
            }
            return didGen;
        }

        private bool GenerateObstacle(int x, int y)
        {
            int toGen = rng.Next(0, 2);
            if (toGen == 1)
            {
                int barrelOrCrate = rng.Next(0, 2);
                GameObject generated = Instantiate((barrelOrCrate == 1) ? barrel : crate) as GameObject;
                generated.SetActive(true);
                generated.transform.Translate(new Vector2(x, y));
                obstacles.Add(generated);
            }

            return toGen == 1;
        }
    }
}
