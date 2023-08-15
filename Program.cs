// See https://aka.ms/new-console-template for more information


public partial class Program
{
    // Traffic light periods
    private TimeSpan period1 = TimeSpan.FromSeconds(30);
    private TimeSpan period2 = TimeSpan.FromSeconds(30);
    
    // Car arrival rates (cars per minute)
    public static int arrivalRateNorth = 10;
    public static int arrivalRateSouth = 5;
    public static int arrivalRateEast = 15;
    public static int arrivalRateWest = 8;

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

    private static DateTime nextCarArrivalTime = DateTime.Now;

    static void Main(string[] args)
    {
        // Set initial light states
        SetLights(true, true, false, false);

        while (true)
        {

        }
    }

    public static void SetLights(bool north, bool south, bool west, bool east)
    {
        lightNorth = north;
        lightSouth = south;
        lightWest = west;
        lightEast = east;
    }

    public static void GenerateCarArrivals()
    {
        if (DateTime.Now >= nextCarArrivalTime)
        {
            for (int i = 0; i < arrivalRateNorth; i++)
            {
                northQueue.Enqueue(new Car());
            }

            for (int i = 0; i < arrivalRateSouth; i++)
            {
                southQueue.Enqueue(new Car());
            }

            for (int i = 0; i < arrivalRateWest; i++)
            {
                westQueue.Enqueue(new Car());
            }

            for (int i = 0; i < arrivalRateEast; i++)
            {
                eastQueue.Enqueue(new Car());
            }

            nextCarArrivalTime = nextCarArrivalTime.AddMinutes(1);
        }
    }

    public class Car
    {
    }
}


