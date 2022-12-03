namespace AssetManager.Model;

public class Asset
{
    public int id { get; set; }
    public string name { get; set; }
    public string description { get; set; }
    public int? location_id { get; set; }
    public int? status_id { get; set; }
    public int? type_id { get; set; }
    public string manufacturer { get; set; }
    public string model { get; set; }
    public string serial_number { get; set; }
    public DateTime? purchase_date { get; set; }
    public DateTime? warranty_expiration { get; set; }
    public int? estimated_life { get; set; }
    public double? purchase_price { get; set; }
    public string comments { get; set; }
    public int? item_count { get ; set; }
    public DateTime created { get; set; }
    public int? created_by { get; set; }
    public int? updated_by { get; set; }
    public DateTime last_updated { get; set; }
    public string photo_url { get; set; }
    public string path { get; set; }
    public bool? borrow { get; set; }
}