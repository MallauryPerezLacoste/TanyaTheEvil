using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.IO;

public class GenerateMap : MonoBehaviour
{
    [SerializeField] GameObject Hexagone;
    Vector2 pair;

    [SerializeField] Vector2 Dimensions;
    [SerializeField] float xpadding;
    [SerializeField] float ypadding;

    [SerializeField] string groundFile;
    [SerializeField] GameObject[] groundsObjects;
    private int[][] groundMatrix;

    [SerializeField] string materialFile;
    [SerializeField] Material[] materialSupport;
    private int[][] materialMatrix;

    [SerializeField] string charactersFile;
    [SerializeField] GameObject[] charactersList;
    private int[][] anyCharacterMatrix;

    [SerializeField] string teamFile;
    private int[][] teamMatrix;

    [SerializeField] private GameObject ManageGame;



    void Start()
    {
        pair = Hexagone.GetComponent<Hexagone>().getmapGeneratingoffset();

        groundMatrix = new int[(int)Dimensions[0]][];
        materialMatrix = new int[(int)Dimensions[0]][];
        anyCharacterMatrix = new int[(int)Dimensions[0]][];
        teamMatrix = new int[(int)Dimensions[0]][];
        for (int i = 0; i < Dimensions[1]; i++)
        {
            groundMatrix[i] = new int[(int)Dimensions[1]];
            materialMatrix[i] = new int[(int)Dimensions[1]];
            anyCharacterMatrix[i] = new int[(int)Dimensions[1]];
            teamMatrix[i] = new int[(int)Dimensions[1]];
            
            for (int j = 0; j < Dimensions[1]; j++){
                groundMatrix[i][j] = -1;
                materialMatrix[i][j] = -1;
                anyCharacterMatrix[i][j] = -1;
                teamMatrix[i][j] = -1;
            }
        }

        generateMap();
        Instantiate(ManageGame, new Vector3(), new Quaternion());
        Destroy(gameObject);
    }


    void generateMap()
    {
        parseGroundMaterial();

        for (int i=0; i < Dimensions[0]; i++)
        {
            for (int j = 0; j < Dimensions[1]; j++)
            {
                InstantiateNewSupport( i,  j);
            }
        }
    }

    void InstantiateNewSupport(int i,int j)
    {
        Vector3 newCoodinates = newHexagonePosition(i, j, pair, xpadding, ypadding);

        //new support
        GameObject newHexagone = Instantiate(Hexagone, newCoodinates, new Quaternion());
        //new position
        newHexagone.GetComponent<Hexagone>().setCoordinates(new Vector2(i, j));
        //new name
        newHexagone.name = newSupportName(i, j);
        //new ground
        if (groundMatrix[i][j] >-1)
        {
            GameObject newGround = Instantiate(groundsObjects[groundMatrix[i][j]], newCoodinates,new Quaternion(),newHexagone.transform);
            newHexagone.GetComponent<Hexagone>().Ground = newGround;
        }
        //new material
        if (materialMatrix[i][j] > -1)
        {
            newHexagone.GetComponent<Renderer>().material = materialSupport[groundMatrix[i][j]];
        }
        //new character
        if (anyCharacterMatrix[i][j] > -1)
        {
            GameObject newCharacter = Instantiate(charactersList[anyCharacterMatrix[i][j]], newCoodinates, new Quaternion());
            newHexagone.GetComponent<Hexagone>().Player = newCharacter;
            newCharacter.GetComponent<anyCharacter>().setCurrentHexagone(newHexagone);
            newCharacter.GetComponent<anyCharacter>().setTeam(Convert.ToBoolean(teamMatrix[i][j]));
        }
    }

    Vector3 newHexagonePosition(int i,int j,Vector2 x,float xpadding,float yppadding)
    {
        float X, Z;
        X = (float)(i * x[0] + xpadding*i);
        Z = (float)(j * x[1] + yppadding*j);
        if (j % 2 == 1)
        {
            X += x[0] / 2;
        }
        return new Vector3(X,0,Z);
    }

    public string newSupportName(int i, int j) { return "Support_" + i.ToString() + ":" + j.ToString(); }


    void parseFileToMatrix(int[][] ground, string file)
    {
        if (file == "") { return; }
        char[] Separator = { ';' };
        string[] lines = System.IO.File.ReadAllLines(file);

        for(int i = 0; i < lines.Length && i<Dimensions[0]; i++)
        {
            string[] values = lines[i].Split(Separator);
            for(int j = 0; j < values.Length && j<Dimensions[1]; j++)
            {
                int.TryParse(values[j], out ground[i][j]);
            }
        }
    }
    
    void parseGroundMaterial()
    {
        parseFileToMatrix(groundMatrix, groundFile);
        parseFileToMatrix(materialMatrix, materialFile);
        parseFileToMatrix(anyCharacterMatrix, charactersFile);
        parseFileToMatrix(teamMatrix, teamFile);
    }


}
