using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Possible directions for tile placement
public enum Directions
{
    north, //0
    northeast, //1
    east, //2
    southeast, //3
    south, //4
    southwest, //5
    west, //6
    northwest, //7
}

// Extensions to the functionality of Directions enum
public static class DirectionExtensions
{
    public static Directions Opposite(this Directions direction)
    {
        return (int)direction < 4 ? (direction + 4) : (direction - 4);
    }
}


// Possible modes for Tiles
public enum TileMode
{
    minimal,
    quarter,
    half,
    maximum
}
// Extensions to the functionality of Directions enum
public static class TileModeExtensions
{
    public static TileMode Reduce(this TileMode mode)
    {
        return mode > 0 ? mode - 1 : 0;
    }

    public static TileMode Greater(this TileMode mode, TileMode other)
    {
        return mode > other ? mode : other;
    }
}

// Info held within each tile
public class TileInfo
{
    public Color color = Color.gray;
    public int value = 1;

    public TileInfo(TileMode mode = TileMode.minimal)
    {
        switch (mode)
        {
            case TileMode.maximum:
                value = 16;
                color = Color.yellow;
                break;
            case TileMode.half:
                value = 8;
                color = new Color(1, 0.4f, 0); //orange
                //color = Color.blue;
                break;
            case TileMode.quarter:
                value = 4;
                color = Color.red;
                break;
        }
    }
}

[RequireComponent(typeof(Image), typeof(Button), typeof(RectTransform))]
public class GameTile : MonoBehaviour
{

    public bool isScanned = false;
    public TileInfo Info = new TileInfo();
    public RectTransform rectTransform;


    // public property allows us to, when Mode is changed, also update Info
    public TileMode Mode
    {
        get { return mode; }
        set
        {
            mode = value;
            Info = new TileInfo(mode);
        }
    }


    private TileMode mode;
    private Image image;
    private Button button;
    //[SerializeField]
    private GameTile[] neighbors = new GameTile[8];

    private void Awake()
    {
        image = GetComponent<Image>();
        button = GetComponent<Button>();
        rectTransform = GetComponent<RectTransform>();
    }

    public void TileActivation()
    {
        if (GameInfo.Scanning)
        {
            GameInfo.Scans--;
            isScanned = true;
            UpdateImage();
            foreach (GameTile tile in neighbors)
            {
                if (tile != null)
                {
                    tile.isScanned = true;
                    tile.UpdateImage();
                }
            }
        }
        else if (!GameInfo.Scanning && GameInfo.CanExtract)
        {
            isScanned = true;
            UpdateImage();
            GameInfo.Score += Info.value;
            GameInfo.Extractions--;

            foreach (GameTile tile in neighbors)
            {
                if (tile != null)
                {
                    tile.Mode = tile.Mode.Reduce();
                    tile.UpdateImage();
                }
            }

            Mode = TileMode.minimal;
            UpdateImage();
            Debug.Log(GameInfo.Score);
        }
    }

    private void UpdateImage()
    {
        if (isScanned)
        {
            image.color = Info.color;
        }
    }

    public void SetNeighbor(Directions direction, GameTile tile)
    {
        neighbors[(int)direction] = tile;
        tile.neighbors[(int)direction.Opposite()] = this;
    }

    public GameTile GetNeighbor(Directions direction)
    {
        return neighbors[(int)direction];
    }
}
