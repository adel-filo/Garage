class Program
{
    static Garage<Vehicle> garage;

    static void Main(string[] args)
    {
        Console.Write("Enter capacity for the new garage: ");
        if (int.TryParse(Console.ReadLine(), out int capacity) && capacity > 0)
        {
            garage = new Garage<Vehicle>(capacity);
            Console.WriteLine($"A garage with space for {capacity} vehicles has been created.");
        }
        else
        {
            garage = new Garage<Vehicle>(10);
            Console.WriteLine("Invalid size. Default capacity 10 has been set.");
        }

        bool running = true;
        while (running)
        {
            Console.WriteLine("\n--- GARAGE MENU ---");
            Console.WriteLine("1. List all parked vehicles");
            Console.WriteLine("2. List vehicle types and counts");
            Console.WriteLine("3. Park vehicle");
            Console.WriteLine("4. Unpark vehicle");
            Console.WriteLine("5. Search vehicle by registration number");
            Console.WriteLine("0. Exit");
            Console.Write("Choose an option: ");

            string choice = Console.ReadLine();
            Console.Clear();

            switch (choice)
            {
                case "1":
                    ListAllVehicles();
                    break;
                case "2":
                    ListVehicleTypesAndCounts();
                    break;
                case "3":
                    ParkVehicleMenu();
                    break;
                case "4":
                    UnparkVehicleMenu();
                    break;
                case "5":
                    SearchByRegNo();
                    break;
                case "0":
                    running = false;
                    break;
                default:
                    Console.WriteLine("Invalid choice, try again.");
                    break;
            }
        }
    }

    static void ListAllVehicles()
    {
        Console.WriteLine("--- Parked vehicles ---");
        Vehicle[] vehicles = garage.GetVehicles();
        bool anyFound = false;

        for (int i = 0; i < vehicles.Length; i++)
        {
            if (vehicles[i] != null)
            {
                anyFound = true;
                Console.WriteLine($"Slot {i + 1}: Type: {vehicles[i].GetType().Name}, RegNo: {vehicles[i].RegistrationNumber}, Color: {vehicles[i].Color}, Wheels: {vehicles[i].NumberOfWheels}");
            }
        }

        if (!anyFound)
        {
            Console.WriteLine("The garage is empty.");
        }
    }

    static void ListVehicleTypesAndCounts()
    {
        Vehicle[] vehicles = garage.GetVehicles();
        string[] types = new string[vehicles.Length];
        int[] counts = new int[vehicles.Length];
        int uniqueTypesCount = 0;

        for (int i = 0; i < vehicles.Length; i++)
        {
            if (vehicles[i] != null)
            {
                string typeName = vehicles[i].GetType().Name;
                bool found = false;

                for (int j = 0; j < uniqueTypesCount; j++)
                {
                    if (types[j] == typeName)
                    {
                        counts[j]++;
                        found = true;
                        break;
                    }
                }

                if (!found)
                {
                    types[uniqueTypesCount] = typeName;
                    counts[uniqueTypesCount] = 1;
                    uniqueTypesCount++;
                }
            }
        }

        Console.WriteLine("--- Vehicle types and counts ---");
        if (uniqueTypesCount == 0)
        {
            Console.WriteLine("No vehicles in the garage.");
        }
        else
        {
            for (int i = 0; i < uniqueTypesCount; i++)
            {
                Console.WriteLine($"{types[i]}: {counts[i]}");
            }
        }
    }

    static void ParkVehicleMenu()
    {
        if (garage.IsFull())
        {
            Console.WriteLine("Failed: The garage is full!");
            return;
        }

        Console.WriteLine("What type of vehicle do you want to park?");
        Console.WriteLine("1. Car");
        Console.WriteLine("2. Motorcycle");
        Console.WriteLine("3. Airplane");
        Console.WriteLine("4. Bus");
        Console.WriteLine("5. Boat");
        Console.Write("Choice: ");
        string typeChoice = Console.ReadLine();

        if (typeChoice != "1" && typeChoice != "2" && typeChoice != "3" && typeChoice != "4" && typeChoice != "5")
        {
            Console.WriteLine("Invalid vehicle type choice.");
            return;
        }

        Console.Write("Enter registration number: ");
        string regNo = Console.ReadLine();

        if (garage.IsRegistrationNumberTaken(regNo))
        {
            Console.WriteLine("Failed: A vehicle with this registration number already exists!");
            return;
        }

        Console.Write("Enter color: ");
        string color = Console.ReadLine();

        int wheels = 0;
        while (true)
        {
            Console.Write("Enter number of wheels: ");
            if (int.TryParse(Console.ReadLine(), out wheels) && wheels >= 0)
            {
                break;
            }
            Console.WriteLine("Invalid input! Please enter a valid number for wheels.");
        }

        Vehicle vehicleToPark = null;

        if (typeChoice == "1")
        {
            int doors = 0;
            while (true)
            {
                Console.Write("Enter number of doors: ");
                if (int.TryParse(Console.ReadLine(), out doors) && doors >= 0)
                {
                    break;
                }
                Console.WriteLine("Invalid input! Please enter a valid number for doors.");
            }
            vehicleToPark = new Car(regNo, color, wheels, doors);
        }
        else if (typeChoice == "2")
        {
            double cyl = 0;
            while (true)
            {
                Console.Write("Enter cylinder volume: ");
                if (double.TryParse(Console.ReadLine(), out cyl) && cyl >= 0)
                {
                    break;
                }
                Console.WriteLine("Invalid input! Please enter a valid number for cylinder volume.");
            }
            vehicleToPark = new Motorcycle(regNo, color, wheels, cyl);
        }
        else if (typeChoice == "3")
        {
            int engines = 0;
            while (true)
            {
                Console.Write("Enter number of engines: ");
                if (int.TryParse(Console.ReadLine(), out engines) && engines >= 0)
                {
                    break;
                }
                Console.WriteLine("Invalid input! Please enter a valid number for engines.");
            }
            vehicleToPark = new Airplane(regNo, color, wheels, engines);
        }
        else if (typeChoice == "4")
        {
            int seats = 0;
            while (true)
            {
                Console.Write("Enter number of seats: ");
                if (int.TryParse(Console.ReadLine(), out seats) && seats >= 0)
                {
                    break;
                }
                Console.WriteLine("Invalid input! Please enter a valid number for seats.");
            }
            vehicleToPark = new Bus(regNo, color, wheels, seats);
        }
        else if (typeChoice == "5")
        {
            double length = 0;
            while (true)
            {
                Console.Write("Enter length: ");
                if (double.TryParse(Console.ReadLine(), out length) && length >= 0)
                {
                    break;
                }
                Console.WriteLine("Invalid input! Please enter a valid number for length.");
            }
            vehicleToPark = new Boat(regNo, color, wheels, length);
        }

        if (vehicleToPark != null)
        {
            bool success = garage.ParkVehicle(vehicleToPark);
            if (success)
            {
                Console.WriteLine("The vehicle has been parked successfully!");
            }
        }
    }

    static void UnparkVehicleMenu()
    {
        Console.Write("Enter registration number of the vehicle to unpark: ");
        string regNo = Console.ReadLine();

        bool success = garage.UnparkVehicle(regNo);
        if (success)
        {
            Console.WriteLine("The vehicle has been unparked from the garage.");
        }
        else
        {
            Console.WriteLine("The vehicle was not found.");
        }
    }

    static void SearchByRegNo()
    {
        Console.Write("Enter registration number to search for: ");
        string searchReg = Console.ReadLine();

        Vehicle[] vehicles = garage.GetVehicles();
        bool found = false;

        for (int i = 0; i < vehicles.Length; i++)
        {
            if (vehicles[i] != null && vehicles[i].RegistrationNumber.Equals(searchReg, StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine($"Found! Type: {vehicles[i].GetType().Name}, RegNo: {vehicles[i].RegistrationNumber}, Color: {vehicles[i].Color}");
                found = true;
                break;
            }
        }

        if (!found)
        {
            Console.WriteLine("No vehicle with that registration number was found.");
        }
    }
}