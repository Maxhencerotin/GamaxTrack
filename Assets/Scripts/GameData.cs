using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Test Github
public static class GameData
{
    public enum MedalType
    {
        Bronze,     
        Silver,    
        Gold,
        Author
    }

    public struct LevelData
    {

        public int levelNumber;
        public float bronzeTime;
        public float silverTime;
        public float goldTime;
        public float authorTime;

        public LevelData(int levelNbr, float b, float s, float g, float a)
        {
            levelNumber = levelNbr;
            bronzeTime = b;
            silverTime = s;
            goldTime = g;
            authorTime = a;
        }
    }

    public const string BESTTIME_DATA_KEYWORD = "BestTime";
    public const int NUMBER_LEVEL = 6;


    //LEVEL1
    public const float BRONZE1 = 15f;   //OK
    public const float SILVER1 = 12f;   //OK 3
    public const float GOLD1 = 9.58f;   //OK 2.42
    public const float AUTHOR1 = 9.251f;

    //LEVEL 2
    public const float BRONZE2 = 15f;   //OK
    public const float SILVER2 = 12.5f; //OK 2.5
    public const float GOLD2 = 11f;     //OK 1.5
    public const float AUTHOR2 = 10.241f;

    //LEVEL 3
    public const float BRONZE3 = 21f;   //OK
    public const float SILVER3 = 18f;   //OK 3
    public const float GOLD3 = 16f;     //OK 2
    public const float AUTHOR3 = 15.236f;

    //LEVEL 4
    public const float BRONZE4 = 19f;   //OK
    public const float SILVER4 = 16.5f; //OK 2.5
    public const float GOLD4 = 15f;     //OK 1.5
    public const float AUTHOR4 = 14.777f;  

    //LEVEL 5
    public const float BRONZE5 = 18.5f;   //OK
    public const float SILVER5 = 16.5f;   //OK 2
    public const float GOLD5 = 15.5f;   //OK 1
    public const float AUTHOR5 = 14.390f;   //vrmt chaud

    //LEVEL 6
    public const float BRONZE6 = 20f;   //OK
    public const float SILVER6 = 18f;   //OK 2
    public const float GOLD6 = 17f;     //OK 1
    public const float AUTHOR6 = 16.703f;

    public static readonly LevelData[] LEVELS = new LevelData[NUMBER_LEVEL] //static readonly is similar to const (which doesn't work for complex objects)
    {
        new LevelData(1, BRONZE1, SILVER1, GOLD1, AUTHOR1), 
        new LevelData(2, BRONZE2, SILVER2, GOLD2, AUTHOR2), 
        new LevelData(3, BRONZE3, SILVER3, GOLD3, AUTHOR3), 
        new LevelData(4, BRONZE4, SILVER4, GOLD4, AUTHOR4), 
        new LevelData(5, BRONZE5, SILVER5, GOLD5, AUTHOR5), 
        new LevelData(6, BRONZE6, SILVER6, GOLD6, AUTHOR6) 
    };
}
