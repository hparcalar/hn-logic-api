using System;
public class ProcessResultModel {
    public int Id { get; set; }
        public int ProcessStepId { get; set; }
        public string StrResult { get; set; }
        public float? NumResult { get; set; }
        public DateTime CreatedDate { get; set; }
        public ProcessStepModel ProcessStep { get; set; }
}