using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using HekaNodes.DataAccess;

namespace hn_logic_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProcessController : ControllerBase
    {
        NodesContext _context;

        public ProcessController(NodesContext context){
            _context = context;
        }

        [HttpGet]
        [Route("get")]
        public IEnumerable<HnProcessModel> Get()
        {
            HnProcessModel[] data = new HnProcessModel[0];
            try
            {
                data = _context.HnProcesses.Select(d => new HnProcessModel{
                        HnAppId = d.HnAppId,
                        HnProcessId = d.HnProcessId,
                        IsActive = d.IsActive,
                        Name = d.Name,
                    }).ToArray();
            }
            catch
            {
                
            }
            
            return data;
        }

        [HttpGet]
        [Route("get/{id}")]
        public HnProcessModel Get(int id)
        {
            HnProcessModel data = new HnProcessModel();
            try
            {
                data = _context.HnProcesses.Where(d => d.HnProcessId == id).Select(d => new HnProcessModel{
                        HnAppId = d.HnAppId,
                        HnProcessId = d.HnProcessId,
                        IsActive = d.IsActive,
                        Name = d.Name,
                    }).FirstOrDefault();

                if (data != null){
                    data.Steps = _context.ProcessSteps.Where(d => d.HnProcessId == id)
                        .Select(d => new ProcessStepModel {
                            ProcessStepId = d.ProcessStepId,
                            Explanation = d.Explanation,
                            Comparison = d.Comparison,
                            ResultAction = d.ResultAction,
                            HnProcessId = d.HnProcessId,
                        }).ToArray();
                }
            }
            catch
            {
                
            }
            
            return data;
        }

        [HttpPost]
        public BusinessResult Post(HnProcessModel model){
            BusinessResult result = new BusinessResult();

            try
            {
                var dbObj = _context.HnProcesses.FirstOrDefault(d => d.HnProcessId == model.HnProcessId);
                if (dbObj == null){
                    dbObj = new HnProcess();
                    _context.HnProcesses.Add(dbObj);
                }

                dbObj.HnAppId = model.HnAppId;
                dbObj.IsActive = model.IsActive;
                dbObj.Name = model.Name;

                #region SAVE STEPS
                var currentSteps = _context.ProcessSteps.Where(d => d.HnProcessId == model.HnProcessId).ToArray();
                var deletedRecords = currentSteps.Where(d => !model.Steps.Select(m => m.ProcessStepId).ToArray().Contains(d.ProcessStepId))
                    .ToArray();
                foreach (var item in deletedRecords)
                {
                    _context.ProcessSteps.Remove(item);
                }

                foreach (var item in model.Steps)
                {
                    var dbStep = _context.ProcessSteps.FirstOrDefault(d => d.ProcessStepId == item.ProcessStepId);
                    if (dbStep == null){
                        dbStep = new ProcessStep();
                        _context.ProcessSteps.Add(dbStep);
                    }

                    dbStep.Explanation = item.Explanation;
                    dbStep.Comparison = item.Comparison;
                    dbStep.ResultAction = item.ResultAction;
                    dbStep.HnProcess = dbObj;
                }
                #endregion

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

        [HttpDelete]
        public BusinessResult Delete(int id){
            BusinessResult result = new BusinessResult();

            try
            {
                var dbObj = _context.HnProcesses.FirstOrDefault(d => d.HnProcessId == id);
                if (dbObj == null)
                    throw new Exception("Silinmesi istenen kayıt bulunamadı.");

                var steps = _context.ProcessSteps.Where(d => d.HnProcessId == id).ToArray();
                foreach (var item in steps)
                {
                    _context.ProcessSteps.Remove(item);

                    var logs = _context.ProcessResults.Where(d => d.ProcessStepId == item.ProcessStepId).ToArray();
                    foreach (var logItem in logs)
                    {
                        _context.ProcessResults.Remove(logItem);
                    }
                }

                _context.HnProcesses.Remove(dbObj);

                _context.SaveChanges();
                result.Result=true;
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
