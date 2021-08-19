public class ProcessStepModel {
    public int ProcessStepId { get; set; }
        public string Explanation { get; set; }
        public string Comparison { get; set; }
        public int OrderNo { get; set; }
        public int? DelayBefore { get; set; }
        public int? DelayAfter { get; set; }
        public bool IsTestResult { get; set; } = false;
        public string ResultAction { get; set; }
        public int HnProcessId { get; set; }
        public bool WaitUntilConditionRealized { get; set; } = false;
        public int ConditionRealizeTimeout { get; set; }
}