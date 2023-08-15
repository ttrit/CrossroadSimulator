// See https://aka.ms/new-console-template for more information


public partial class Program
{
    // Traffic light periods
    private TimeSpan period1 = TimeSpan.FromSeconds(30);
    private TimeSpan period2 = TimeSpan.FromSeconds(30);
    
    // Car arrival rates (cars per minute)
    private int arrivalRateNorth = 10;
    private int arrivalRateSouth = 5;
    private int arrivalRateEast = 15;
    private int arrivalRateWest = 8;

    // Time for each car to clear the intersection
    private int clearanceTime = 2;

    public static Queue<Car> northQueue = new Queue<Car>();
    public static Queue<Car> southQueue = new Queue<Car>();
    public static Queue<Car> westQueue = new Queue<Car>();
    public static Queue<Car> eastQueue = new Queue<Car>();

    private static bool lightNorth = false;
    private static bool lightSouth = false;
    private static bool lightWest = false;
    private static bool lightEast = false;

    static void Main(string[] args)
    {
        SetLights(true, true, false, false);
    }

    public static void SetLights(bool north, bool south, bool west, bool east)
    {
        lightNorth = north;
        lightSouth = south;
        lightWest = west;
        lightEast = east;
    }

    public class Car
    {
    }
}


