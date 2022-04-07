using UnityEngine;

public class Categories : MonoBehaviour
{
    public const string HOUSING = "Housing";
    public const string SPECIAL_BUILDINGS = "SpecialBuildings";
    public const string SHOPS = "Shops";
    public const string ROADS = "Roads";
    public const string VEGETATION = "Vegetation";
    public const string TERRAFORMING = "Terraforming";
}

public class Cities : MonoBehaviour
{

    public const string SULIMO = "Sulimo";
    public const string ULMO = "Ulmo";
    public const string WILWAR = "Wilwar";
    public const string ANAR = "Anar";
}

public class Dimensions : MonoBehaviour
{
    public const int pointEspacement = 4;
}

public class Options : MonoBehaviour
{
    public const string FULLSCREEN = "Fullscreen";
    public const string BORDERLESS = "Borderless Windowed";
    public const string WINDOWED = "Windowed";

    public const int THIRTY_FPS = 30;
    public const int SIXTY_FPS = 60;
    public const int HUNDRED_TWENTY_FPS = 120;

    public const string FRENCH = "FR-fr";
    public const string ENGLISH = "EN-en";
}
