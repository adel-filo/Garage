public class Garage<T> where T : Vehicle
{
    private T[] vehicles;
    public int Capacity { get; private set; }

    public Garage(int capacity)
    {
        Capacity = capacity;
        vehicles = new T[capacity];
    }

    public bool IsRegistrationNumberTaken(string regNo)
    {
        for (int i = 0; i < vehicles.Length; i++)
        {
            if (vehicles[i] != null && vehicles[i].RegistrationNumber.Equals(regNo, StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }
        }
        return false;
    }

    public bool ParkVehicle(T vehicle)
    {
        if (IsRegistrationNumberTaken(vehicle.RegistrationNumber))
        {
            return false;
        }

        for (int i = 0; i < vehicles.Length; i++)
        {
            if (vehicles[i] == null)
            {
                vehicles[i] = vehicle;
                return true;
            }
        }
        return false;
    }

    public bool UnparkVehicle(string regNo)
    {
        for (int i = 0; i < vehicles.Length; i++)
        {
            if (vehicles[i] != null && vehicles[i].RegistrationNumber.Equals(regNo, StringComparison.OrdinalIgnoreCase))
            {
                vehicles[i] = null;
                return true;
            }
        }
        return false;
    }

    public T[] GetVehicles()
    {
        return vehicles;
    }

    public bool IsFull()
    {
        for (int i = 0; i < vehicles.Length; i++)
        {
            if (vehicles[i] == null)
            {
                return false;
            }
        }
        return true;
    }
}