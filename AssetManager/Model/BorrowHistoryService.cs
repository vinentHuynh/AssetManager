namespace AssetManager.Model;

public class BorrowHistoryService
{
    public static void CreateBorrowRequest(int userId, int assetId, string comments)
    {
        ConnectionDB connectionDB = new ConnectionDB();

        // create borrow history record
        connectionDB.OpenConnection();

        string query = "INSERT INTO borrow_history VALUES (" +
            userId + ", " +
            assetId + ", " +
            "CAST('" + DateTime.Now + "' AS DATETIME), " +
            "NULL, " +
            "NULL, " +
            "'" + comments + "' " +
        ")";

        connectionDB.DataReader(query);
        connectionDB.CloseConnection();

        // set asset status to 5 (requested)
        connectionDB.OpenConnection();

        query = "UPDATE asset SET status_id = 5 WHERE id = " + assetId;

        connectionDB.DataReader(query);
        connectionDB.CloseConnection();
    }

    public static void ApproveBorrowRequest(int id, int assetId)
    {
        ConnectionDB connectionDB = new ConnectionDB();

        // update borrow history record
        connectionDB.OpenConnection();

        string query = "" +
            "UPDATE borrow_history " +
            "SET " +
                "date_borrowed = CAST('" + DateTime.Now + "' AS DATETIME) " +
            "WHERE id = " + id;

        connectionDB.DataReader(query);
        connectionDB.CloseConnection();

        // update asset status to 6 (borrowed)
        connectionDB.OpenConnection();

        query = "UPDATE asset SET status_id = 6 WHERE id = " + assetId;

        connectionDB.DataReader(query);
        connectionDB.CloseConnection();
    }

    public static void ReturnAsset(int id, int assetId)
    {
        ConnectionDB connectionDB = new ConnectionDB();

        // update borrow history record
        connectionDB.OpenConnection();

        string query = "" +
            "UPDATE borrow_history " +
            "SET " +
                "date_returned = CAST('" + DateTime.Now + "' AS DATETIME) " +
            "WHERE id = " + id;

        connectionDB.DataReader(query);
        connectionDB.CloseConnection();

        // update asset status to 7 (returned)
        connectionDB.OpenConnection();

        query = "UPDATE asset SET status_id = 7 WHERE id = " + assetId;

        connectionDB.DataReader(query);
        connectionDB.CloseConnection();
    }
}
