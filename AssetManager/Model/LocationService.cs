using FastMember;
using Microsoft.Data.SqlClient;

namespace AssetManager.Model;

public class LocationService
{
    public static Location GetLocationById(int id)
    {
        ConnectionDB connectionDB = new ConnectionDB();
        connectionDB.OpenConnection();

        string query = "SELECT id, value FROM location WHERE id = " + id;
        SqlDataReader dr = connectionDB.DataReader(query);
        dr.Read();

        return MapRowToLocation<Location>(dr);
    }

    public static List<Location> GetAllLocations()
    {
        // get all locations
        ConnectionDB connectionDB = new ConnectionDB();
        connectionDB.OpenConnection();

        string query = "SELECT id, value FROM location";
        SqlDataReader dr = connectionDB.DataReader(query);

        // map locations to object list
        List<Location> locations = new List<Location>();
        while (dr.Read())
        {
            locations.Add(MapRowToLocation<Location>(dr));
        }

        return locations;
    }

    public static Location MapRowToLocation<Location>(SqlDataReader dr) where Location : class, new()
    {
        Type type = typeof(Location);
        var accessor = TypeAccessor.Create(type);
        var members = accessor.GetMembers();
        var location = new Location();

        for (int i = 0; i < dr.FieldCount; i++)
        {
            string fieldName = dr.GetName(i);

            if (members.Any(m => string.Equals(m.Name, fieldName, StringComparison.OrdinalIgnoreCase)))
            {
                if (!dr.IsDBNull(i))
                {
                    accessor[location, fieldName] = dr.GetValue(i);
                }
            }
        }

        return location;
    }
}

