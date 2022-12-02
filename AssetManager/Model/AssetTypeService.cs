using FastMember;
using Microsoft.Data.SqlClient;

namespace AssetManager.Model;

public class AssetTypeService
{
    public static List<AssetType> GetAllTypes()
    {
        // get all types
        ConnectionDB connectionDB = new ConnectionDB();
        connectionDB.OpenConnection();

        string query = "SELECT id, value FROM type";
        SqlDataReader dr = connectionDB.DataReader(query);

        // map types to object list
        List<AssetType> types = new List<AssetType>();
        while (dr.Read())
        {
            types.Add(MapRowToType<AssetType>(dr));
        }

        return types;
    }

    public static AssetType MapRowToType<AssetType>(SqlDataReader dr) where AssetType : class, new()
    {
        Type type = typeof(AssetType);
        var accessor = TypeAccessor.Create(type);
        var members = accessor.GetMembers();
        var assetType = new AssetType();

        for (int i = 0; i < dr.FieldCount; i++)
        {
            string fieldName = dr.GetName(i);

            if (members.Any(m => string.Equals(m.Name, fieldName, StringComparison.OrdinalIgnoreCase)))
            {
                if (!dr.IsDBNull(i))
                {
                    accessor[assetType, fieldName] = dr.GetValue(i);
                }
            }
        }

        return assetType;
    }
}

