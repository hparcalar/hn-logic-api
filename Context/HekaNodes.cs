using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace HekaNodes.DataAccess {
    public class NodesContext : DbContext, IDisposable {
        public DbSet<HnApp> HnApps { get; set; }
        public DbSet<HnProcess> HnProcesses { get; set; }
        public DbSet<ProcessStep> ProcessSteps { get; set; }
        public DbSet<ProcessResult> ProcessResults { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<PrintQueue> PrintQueues { get; set; }

        public NodesContext() : base(){}
        public NodesContext(Microsoft.EntityFrameworkCore.DbContextOptions options) : base(options){}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
            => modelBuilder.UseSerialColumns();

        public new void Dispose() {
            base.Dispose();
        }
    }

    public class HnApp {
        public int HnAppId { get; set; }
        public string AppName { get; set; }
        public bool IsActive { get; set; }
    }

    public class HnProcess{
        public int HnProcessId { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public int HnAppId { get; set; }
        public int DelayBefore { get; set; }
        public int DelayAfter { get; set; }
        public bool IsDeviceConnected { get; set; } = false;
        public int ConnectionResetMessageDelay { get; set; } = 1000;
        public string ConnectionResetMessage { get; set; }
        public bool CanRepeat { get; set; } = false;
        public int ProcStatus { get; set; } = 0;
        public bool MustBeStopped { get; set; } = false;
        public string LiveCondition { get; set; }
        public HnApp HnApp { get; set; }
    }

    public class PrintQueue{
        public int PrintQueueId { get; set; }
        public int? ItemId { get; set; }
        public string ItemCode { get; set; }
        public bool? IsPrinted { get; set; }
        public DateTime CreatedDate { get; set; }
    }

    public class Item {
        public int ItemId { get; set; }
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public bool IsActive { get; set; }
    }

    public class ProcessStep {
        public int ProcessStepId { get; set; }
        public string Explanation { get; set; }
        public string Comparison { get; set; }
        public string ResultAction { get; set; }
        public string ElseAction { get; set; }
        public int? DelayBefore { get; set; }
        public int? DelayAfter { get; set; }
        public int HnProcessId { get; set; }
        public string ParallelAction { get; set; }
        public bool IsTestResult { get; set; } = false;
        public int OrderNo { get; set; }
        public bool WaitUntilConditionRealized { get; set; } = false;
        public int ConditionRealizeTimeout { get; set; }
        public int ConditionSatisfiedTime { get; set; } = 0;
        public HnProcess HnProcess { get; set; }
    }

    public class ProcessResult {
        public int Id { get; set; }

        [ForeignKey("ProcessStep")]
        public int ProcessStepId { get; set; }
        public string StrResult { get; set; }
        public float? NumResult { get; set; }
        public bool? IsOk { get; set; }

        [ForeignKey("Item")]
        public int? ItemId { get; set; }
        public virtual Item Item { get; set; }
        public virtual ProcessStep ProcessStep { get; set; }
        public int DurationInSeconds { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}