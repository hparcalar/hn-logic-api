public class HnProcessModel {
    public int HnProcessId { get; set; }
    public string Name { get; set; }
    public bool IsActive { get; set; }
    public int HnAppId { get; set; }
    public ProcessStepModel[] Steps { get; set; }
}