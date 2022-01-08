using System;

public class ProcessReportModel{
    public DateTime CreatedDate { get; set; }
    public string StepName { get; set; }
    public string ItemNo { get; set; }
    public string ItemName { get; set; }
    
    public bool IsOk { get; set; }
    public int Duration { get; set; }
}