using FastMember;
using Microsoft.Data.SqlClient;

namespace AssetManager.Model;

public class StatusService
{
    public static Status GetStatusById(int id)
    {
        ConnectionDB connectionDB = new ConnectionDB();
        connectionDB.OpenConnection();

        string query = "SELECT id, value FROM status WHERE id = " + id;
        SqlDataReader dr = connectionDB.DataReader(query);
        dr.Read();

        return MapRowToStatus<Status>(dr);
    }

    public static List<Status> GetAllStatuses()
    {
        // get all statuses
        ConnectionDB connectionDB = new ConnectionDB();
        connectionDB.OpenConnection();

        string query = "SELECT id, value FROM status";
        SqlDataReader dr = connectionDB.DataReader(query);

        // map statuses to object list
        List<Status> statuses = new List<Status>();
        while (dr.Read())
        {
            statuses.Add(MapRowToStatus<Status>(dr));
        }

        return statuses;
    }

    public static Status MapRowToStatus<Status>(SqlDataReader dr) where Status : class, new()
    {
        Type type = typeof(Status);
        var accessor = TypeAccessor.Create(type);
        var members = accessor.GetMembers();
        var status = new Status();

        for (int i = 0; i < dr.FieldCount; i++)
        {
            string fieldName = dr.GetName(i);

            if (members.Any(m => string.Equals(m.Name, fieldName, StringComparison.OrdinalIgnoreCase)))
            {
                if (!dr.IsDBNull(i))
                {
                    accessor[status, fieldName] = dr.GetValue(i);
                }
            }
        }

        return status;
    }
}

