using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using HekaNodes.DataAccess;
using Microsoft.AspNetCore.Cors;

namespace hn_logic_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [EnableCors()]
    public class AppsController : ControllerBase
    {
        NodesContext _context;

        public AppsController(NodesContext context){
            _context = context;
        }

        [HttpGet]
        public IEnumerable<HnAppModel> Get()
        {
            HnAppModel[] data = new HnAppModel[0];
            try
            {
                data = _context.HnApps.Select(d => new HnAppModel{
                        HnAppId = d.HnAppId,
                        AppName = d.AppName,
                        IsActive = d.IsActive,
                    }).ToArray();
            }
            catch
            {
                
            }
            
            return data;
        }

        [HttpGet]
        [Route("{id}/process")]
        public IEnumerable<HnProcessModel> Process(int id)
        {
            HnProcessModel[] data = new HnProcessModel[0];
            try
            {
                data = _context.HnProcesses.Where(d => d.HnAppId == id)
                .Select(d => new HnProcessModel{
                        HnAppId = d.HnAppId,
                        HnProcessId = d.HnProcessId,
                        IsActive = d.IsActive,
                        CanRepeat = d.CanRepeat,
                        DelayAfter =d.DelayAfter,
                        DelayBefore = d.DelayBefore,
                        IsDeviceConnected = d.IsDeviceConnected,
                        ConnectionResetMessage = d.ConnectionResetMessage,
                        ConnectionResetMessageDelay = d.ConnectionResetMessageDelay,
                        LiveCondition = d.LiveCondition,
                        MustBeStopped = d.MustBeStopped,
                        Name = d.Name,
                    }).OrderBy(d => d.HnProcessId).ToArray();

                foreach (var item in data)
                {
                    item.Steps = _context.ProcessSteps.Where(d => d.HnProcessId == item.HnProcessId)
                        .Select(d => new ProcessStepModel {
                            ProcessStepId = d.ProcessStepId,
                            Explanation = d.Explanation,
                            DelayBefore = d.DelayBefore,
                            DelayAfter = d.DelayAfter,
                            OrderNo = d.OrderNo,
                            Comparison = d.Comparison,
                            ParallelAction = d.ParallelAction,
                            ResultAction = d.ResultAction,
                            ElseAction = d.ElseAction,
                            HnProcessId = d.HnProcessId,
                            IsTestResult = d.IsTestResult,
                            WaitUntilConditionRealized = d.WaitUntilConditionRealized,
                            ConditionRealizeTimeout = d.ConditionRealizeTimeout,
                            ConditionSatisfiedTime = d.ConditionSatisfiedTime,
                        }).ToArray();
                }
            }
            catch
            {
                
            }
            
            return data;
        }

        [HttpGet]
        [Route("{id}")]
        public HnAppModel Get(int id)
        {
            HnAppModel data = new HnAppModel();
            try
            {
                data = _context.HnApps.Where(d => d.HnAppId == id).Select(d => new HnAppModel{
                        HnAppId = d.HnAppId,
                        AppName = d.AppName,
                        IsActive = d.IsActive,
                    }).FirstOrDefault();
            }
            catch
            {
                
            }
            
            return data;
        }

        [HttpPost]
        public BusinessResult Post(HnAppModel model){
            BusinessResult result = new BusinessResult();

            try
            {
                var dbObj = _context.HnApps.FirstOrDefault(d => d.HnAppId == model.HnAppId);
                if (dbObj == null){
                    dbObj = new HnApp();
                    _context.HnApps.Add(dbObj);
                }

                dbObj.AppName = model.AppName;
                dbObj.IsActive = model.IsActive;

                _context.SaveChanges();
                result.Result=true;
                result.RecordId = dbObj.HnAppId;
            }
            catch (System.Exception ex)
            {
                result.Result=false;
                result.ErrorMessage = ex.Message;
            }

            return result;
        }
    }
}
