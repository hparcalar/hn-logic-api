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
    public class ResultsController : ControllerBase
    {
        NodesContext _context;

        public ResultsController(NodesContext context){
            _context = context;
        }

        [HttpGet]
        [Route("get")]
        public IEnumerable<ProcessResultModel> Get()
        {
            ProcessResultModel[] data = new ProcessResultModel[0];
            try
            {
                data = _context.ProcessResults.Select(d => new ProcessResultModel{
                        CreatedDate = d.CreatedDate,
                        Id = d.Id,
                        NumResult = d.NumResult,
                        ProcessStepId = d.ProcessStepId,
                        StrResult = d.StrResult,
                    }).ToArray();
            }
            catch
            {
                
            }
            
            return data;
        }

        [HttpGet]
        [Route("get/{id}")]
        public ProcessResultModel Get(int id)
        {
            ProcessResultModel data = new ProcessResultModel();
            try
            {
                data = _context.ProcessResults.Where(d => d.Id == id).Select(d => new ProcessResultModel{
                        Id = d.Id,
                        CreatedDate = d.CreatedDate,
                        NumResult = d.NumResult,
                        StrResult = d.StrResult,
                        ProcessStepId = d.ProcessStepId,
                    }).FirstOrDefault();
            }
            catch
            {
                
            }
            
            return data;
        }

        [HttpPost]
        public BusinessResult Post(ProcessResultModel model){
            BusinessResult result = new BusinessResult();

            try
            {
                var dbObj = _context.ProcessResults.FirstOrDefault(d => d.Id == model.Id);
                if (dbObj == null){
                    dbObj = new ProcessResult();
                    _context.ProcessResults.Add(dbObj);
                }

                dbObj.CreatedDate = DateTime.Now;
                dbObj.NumResult = model.NumResult;
                dbObj.StrResult = model.StrResult;
                dbObj.ProcessStepId = model.ProcessStepId;

                _context.SaveChanges();
                result.Result=true;
                result.RecordId = dbObj.Id;
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
                var dbObj = _context.ProcessResults.FirstOrDefault(d => d.Id == id);
                if (dbObj == null)
                    throw new Exception("Silinmesi istenen kayıt bulunamadı.");

                _context.ProcessResults.Remove(dbObj);
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
