using FastMember;
using Microsoft.Data.SqlClient;

namespace AssetManager.Model;

public class AssetService
{
    public static Task<Asset> GetAssetById(int id)
    {
        // get asset record for the given id
        ConnectionDB connectionDB = new ConnectionDB();
        connectionDB.OpenConnection();

        string query = "SELECT * FROM asset WHERE asset.id = " + id.ToString() + " AND asset.deleted = 0";
        SqlDataReader dr = connectionDB.DataReader(query);

        // map asset record to object
        Asset asset = MapRowToAsset<Asset>(dr);

        return Task.FromResult(asset);
    }
     
    public static Task<Asset> EditAsset(Asset asset)
    {
        ConnectionDB connectionDB = new ConnectionDB();
        connectionDB.OpenConnection();

        string purchaseDate = asset.purchase_date != null ? ("purchase_date = CAST('" + asset.purchase_date + "' AS DATETIME), ") : "purchase_date = NULL, ";
        string warrantyExp = asset.warranty_expiration != null ? ("warranty_expiration = CAST('" + asset.warranty_expiration + "' AS DATETIME), ") : "warranty_expiration = NULL, ";

        string query =
            "UPDATE asset " +
            "SET " +
                "name = '" + asset.name + "', " +
                "description = '" + asset.description + "', " +
                "location_id = " + asset.location_id + ", " +
                "status_id = " + asset.status_id + ", " +
                "type_id = " + asset.type_id + ", " +
                "manufacturer = '" + asset.manufacturer + "', " +
                "model = '" + asset.model + "', " +
                "serial_number = '" + asset.serial_number + "', " +
                purchaseDate +
                warrantyExp +
                "estimated_life = '" + asset.estimated_life + "', " +
                "purchase_price = '" + asset.purchase_price + "', " +
                "comments = '" + asset.comments + "', " +
                "item_count = '" + asset.item_count + "', " +
                "created_by = '" + asset.created_by + "', " +
                "updated_by = '" + asset.updated_by + "', " +
                "last_updated = CAST('" + asset.last_updated + "' AS DATETIME), " +
                "photo_url = '" + asset.photo_url + "', " +
                "path = '" + asset.path + "', " +
                "borrow = '" + asset.borrow + "'" +
            "WHERE id = " + asset.id;
        connectionDB.DataReader(query);

        return Task.FromResult(asset);
    }
    public static Task<Asset> AddAsset(Asset asset)
    {
        ConnectionDB connectionDB = new ConnectionDB();
        connectionDB.OpenConnection();

        string query = "INSERT INTO asset VALUES(" +
                
                "'" + asset.name + "', " +
                "'" + asset.description + "', " +
                asset.location_id + ", " +
                asset.status_id + ", " +
                asset.type_id + ", " +
                "'" + asset.manufacturer + "', " +
                "'" + asset.model + "', " +
                "'" + asset.serial_number + "', " +
                "'" + asset.purchase_date + "', "+
                "'" + asset.warranty_expiration + "', "+
                "'" + asset.estimated_life + "', " +
                "'" + asset.purchase_price + "', " +
                "'" + asset.comments + "', " +
                "'" + asset.item_count + "', " +
                "CONVERT(DATE,'" + asset.created + "'), " +
                "'" + asset.created_by + "', " +
                "'" + asset.updated_by + "', " +
                "CONVERT(DATE,'" + asset.last_updated + "'), " +
                "'" + asset.photo_url + "', " +
                "'" + asset.path + "', " +
                "'" + asset.borrow + "',"+ 
                "'false'" +")";
        connectionDB.DataReader(query);
        return Task.FromResult(asset);
    }
    public static Task<Asset> DeleteAsset(Asset asset)
    {
        ConnectionDB connectionDB = new ConnectionDB();
        connectionDB.OpenConnection();

        string query = "UPDATE asset SET deleted = 1 WHERE id = " + asset.id;
        connectionDB.DataReader(query);

        return Task.FromResult(asset);
    }

    public static Asset MapRowToAsset<Asset>(SqlDataReader dr) where Asset : class, new()
    {
        Type type = typeof(Asset);
        var accessor = TypeAccessor.Create(type);
        var members = accessor.GetMembers();
        var asset = new Asset();
        dr.Read();

        for (int i = 0; i < dr.FieldCount; i++)
        {
            string fieldName = dr.GetName(i);

            if (members.Any(m => string.Equals(m.Name, fieldName, StringComparison.OrdinalIgnoreCase)))
            {
                if (!dr.IsDBNull(i))
                {
                    accessor[asset, fieldName] = dr.GetValue(i);
                }
            }
        }

        return asset;
    }
}

