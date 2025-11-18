using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Maybe later we can put all the constant values of the game here
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
    public const float BRONZE1 = 20f;   
    public const float SILVER1 = 14f;   
    public const float GOLD1 = 11f;   
    public const float AUTHOR1 = 9.58f;

    //LEVEL 2
    public const float BRONZE2 = 20f;   
    public const float SILVER2 = 15f; 
    public const float GOLD2 = 13f;     
    public const float AUTHOR2 = 11f;

    //LEVEL 3
    public const float BRONZE3 = 30f;   
    public const float SILVER3 = 23f;   
    public const float GOLD3 = 18f;     
    public const float AUTHOR3 = 16f;

    //LEVEL 4
    public const float BRONZE4 = 30f;   
    public const float SILVER4 = 22f; 
    public const float GOLD4 = 18f;     
    public const float AUTHOR4 = 15f;  

    //LEVEL 5
    public const float BRONZE5 = 30f;   
    public const float SILVER5 = 22f;   
    public const float GOLD5 = 18f;   
    public const float AUTHOR5 = 15.5f;   

    //LEVEL 6
    public const float BRONZE6 = 30f;  
    public const float SILVER6 = 23f;   
    public const float GOLD6 = 19f;     
    public const float AUTHOR6 = 17f;

    public static readonly LevelData[] LEVELS = new LevelData[NUMBER_LEVEL] //static readonly is similar to const (which doesn't work for complex objects)
    {
        new LevelData(1, BRONZE1, SILVER1, GOLD1, AUTHOR1), 
        new LevelData(2, BRONZE2, SILVER2, GOLD2, AUTHOR2), 
        new LevelData(3, BRONZE3, SILVER3, GOLD3, AUTHOR3), 
        new LevelData(4, BRONZE4, SILVER4, GOLD4, AUTHOR4), 
        new LevelData(5, BRONZE5, SILVER5, GOLD5, AUTHOR5), 
        new LevelData(6, BRONZE6, SILVER6, GOLD6, AUTHOR6) 
    };

    public const string SAVEDDATA_FILENAME = "savedData.dat";
}
