namespace AssetManager.Model;

public class MaintenanceService
{
    public static void CreateMaintenanceRequest(int assetId, string description)
    {
        ConnectionDB connectionDB = new ConnectionDB();
        
        // create maintenance record
        connectionDB.OpenConnection();

        string query = "INSERT INTO maintenance VALUES (" +
            assetId + ", " +
            "2, " +
            "1, " + // todo user
            "'" + description + "', " +
            "CAST('" + DateTime.Now + "' AS DATETIME), " +
            "NULL, " +
            "NULL " +
        ")";

        connectionDB.DataReader(query);
        connectionDB.CloseConnection();

        // set asset status to 3 (needs maintenance)
        connectionDB.OpenConnection();

        query = "UPDATE asset SET status_id = 3 WHERE id = " + assetId;

        connectionDB.DataReader(query);
        connectionDB.CloseConnection();
    }

    public static void MaintenanceOut(int id, int assetId)
    {
        ConnectionDB connectionDB = new ConnectionDB();

        // update maintenance status and date out
        connectionDB.OpenConnection();

        string query = "" +
            "UPDATE maintenance " +
            "SET " +
                "maintenance_status_id = 3, " +
                "date_out = CAST('" + DateTime.Now + "' AS DATETIME) " +
            "WHERE id = " + id;

        connectionDB.DataReader(query);
        connectionDB.CloseConnection();

        // update asset status to 4 (under maintenance)
        connectionDB.OpenConnection();

        query = "UPDATE asset SET status_id = 4 WHERE id = " + assetId;

        connectionDB.DataReader(query);
        connectionDB.CloseConnection();
    }

    public static void MaintenanceIn(int id, int assetId)
    {
        ConnectionDB connectionDB = new ConnectionDB();

        // update maintenance status and date in
        connectionDB.OpenConnection();

        string query = "" +
            "UPDATE maintenance " +
            "SET " +
                "maintenance_status_id = 4, " +
                "date_in = CAST('" + DateTime.Now + "' AS DATETIME) " +
            "WHERE id = " + id;

        connectionDB.DataReader(query);
        connectionDB.CloseConnection();

        // update asset status to 1 (active)
        connectionDB.OpenConnection();

        query = "UPDATE asset SET status_id = 1 WHERE id = " + assetId;

        connectionDB.DataReader(query);
        connectionDB.CloseConnection();
    }
}
