using System;
public class PrintQueueModel{
    public int PrintQueueId { get; set; }
    public int? ItemId { get; set; }
    public string ItemCode { get; set; }
    public bool? IsPrinted { get; set; }
    public DateTime CreatedDate { get; set; }
}