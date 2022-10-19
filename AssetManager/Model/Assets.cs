﻿using System;
using Microsoft.Data.SqlClient;


namespace AssetManager.Model
{
    public class Asset
    {
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public int location_id { get; set; }
        public int status_id { get; set; }
        public string manufacture { get; set; }
        public string model { get; set; }
        public string serial_number { get; set; }
        public string purchase_date { get; set; }
        public string warranty_expiration { get; set; }
        public string estimated_life { get; set; }
        public double purchase_price { get; set; }
        public string comments { get; set; }
        public int itemCount { get; set; }
        public string created { get; set; }
        public int created_by { get; set; }
        public string last_updated { get; set; }
        public string photo_url { get; set; }
        public string path { get; set; }
        public bool borrow { get; set; }
        public bool deleted { get; set; }


        public List<string> getAssets(string query)
        {
            ConnectionDB connectionDB = new ConnectionDB();
            connectionDB.OpenConnection();
            //string query = "SELECT * from " + currentTable;
            //bool flag = false;
            //if (currentTable == "dbo.asset")
            //{
            //    query += " where deleted = 0";
            //    flag = true;

            //}

            //this.dgvAssets.DataSource = connectionDB.ShowDataInGridView(query);
            SqlDataReader dr = connectionDB.DataReader(query);

            dr.Read();
            var columns = new List<string>();
            columns.Add("");
            for (int i = 0; i < dr.FieldCount; i++)
            {
                columns.Add(dr.GetName(i));
            }
            return columns;
        }


    }

}