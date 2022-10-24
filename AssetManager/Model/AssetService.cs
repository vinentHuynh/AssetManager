﻿using FastMember;
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

        connectionDB.CloseConnection();    
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
                accessor[asset, fieldName] = dr.GetValue(i);
            }
        }

        return asset;
    }
}
