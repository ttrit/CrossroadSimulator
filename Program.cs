// See https://aka.ms/new-console-template for more information


using Microsoft.VisualBasic;

public partial class Program
{
    // Traffic light periods
    private static TimeSpan period1 = TimeSpan.FromSeconds(30);
    private static TimeSpan period2 = TimeSpan.FromSeconds(30);
    
    // Car arrival rates (cars per minute)
    public static int arrivalRateNorth = 10;
    public static int arrivalRateSouth = 5;
    public static int arrivalRateEast = 15;
    public static int arrivalRateWest = 8;

    // Time for each car to clear the intersection
    public static int clearanceTime = 7;

    public static Queue<Car> northQueue = new Queue<Car>();
    public static Queue<Car> southQueue = new Queue<Car>();
    public static Queue<Car> westQueue = new Queue<Car>();
    public static Queue<Car> eastQueue = new Queue<Car>();

    // Initialize state for light in each directions
    private static bool lightNorth = false;
    private static bool lightSouth = false;
    private static bool lightWest = false;
    private static bool lightEast = false;

    private static DateTime nextCarArrivalTime = DateTime.Now;
    private static DateTime nextLightChange = DateTime.Now;

    static void Main(string[] args)
    {
        // Set initial light states
        SetLights(true, true, false, false);

        while (true)
        {
            GenerateCarArrivals();
            ClearIntersection();

            // Switch lights if time elapsed
            if (DateTime.Now >= nextLightChange)
            {
                SwitchLights();
            }

            Thread.Sleep(500);
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
            Console.WriteLine("Generate car loop running");
            for (int i = 0; i < arrivalRateNorth; i++)
            {
                northQueue.Enqueue(new Car() { EntryTime = DateTime.Now });
            }
            Console.WriteLine("Car in north queue:{0}", northQueue.Count);

            for (int i = 0; i < arrivalRateSouth; i++)
            {
                southQueue.Enqueue(new Car() { EntryTime = DateTime.Now });
            }
            Console.WriteLine("Car in south queue:{0}", southQueue.Count);

            for (int i = 0; i < arrivalRateWest; i++)
            {
                westQueue.Enqueue(new Car() { EntryTime = DateTime.Now });
            }
            Console.WriteLine("Car in west queue:{0}", westQueue.Count);

            for (int i = 0; i < arrivalRateEast; i++)
            {
                eastQueue.Enqueue(new Car() { EntryTime = DateTime.Now });
            }
            Console.WriteLine("Car in east queue:{0}", eastQueue.Count);


            nextCarArrivalTime = nextCarArrivalTime.AddMinutes(1);
        }
    }

    public static void ClearIntersection()
    {
        // Check light is on for each ways 
        if (lightNorth == true)
        {
            while (northQueue.Count > 0 && DateTime.Now >= northQueue.Peek().EntryTime + TimeSpan.FromSeconds(clearanceTime))
            {
                Console.WriteLine("Dequeue north");
                northQueue.Dequeue();
                northQueue.Peek().EntryTime = DateTime.Now + TimeSpan.FromSeconds(clearanceTime);
            }
        }

        if (lightSouth == true)
        {
            while (southQueue.Count > 0 && DateTime.Now >= southQueue.Peek().EntryTime + TimeSpan.FromSeconds(clearanceTime))
            {
                Console.WriteLine("Dequeue south");
                southQueue.Dequeue();
                southQueue.Peek().EntryTime = DateTime.Now + TimeSpan.FromSeconds(clearanceTime);
            }
        }

        if (lightWest == true)
        {
            while (westQueue.Count > 0 && DateTime.Now >= westQueue.Peek().EntryTime)
            {
                Console.WriteLine("Dequeue west");
                westQueue.Dequeue();
                westQueue.Peek().EntryTime = DateTime.Now + TimeSpan.FromSeconds(clearanceTime);
            }
        }

        if (lightEast == true)
        {
            while (eastQueue.Count > 0 && DateTime.Now >= eastQueue.Peek().EntryTime + TimeSpan.FromSeconds(clearanceTime))
            {
                Console.WriteLine("Dequeue east");
                eastQueue.Dequeue();
                eastQueue.Peek().EntryTime = DateTime.Now + TimeSpan.FromSeconds(clearanceTime);
            }
        }
    }

    public static void SwitchLights()
    {
        lightNorth = !lightNorth;
        lightSouth = !lightSouth;
        lightWest = !lightWest;
        lightEast = !lightEast;

        nextLightChange = nextLightChange.Add(period1);

        if (nextLightChange > DateTime.Now)
        {
            nextLightChange = nextLightChange.Add(-period1).Add(period2);
        }
    }

    public class Car
    {
        public DateTime EntryTime { get; set; }
    }
}


