using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using HekaNodes.DataAccess;
using Microsoft.AspNetCore.Cors;
using System.IO;
using ClosedXML;
using ClosedXML.Excel;

namespace hn_logic_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [EnableCors()]
    public class ResultsController : ControllerBase
    {
        NodesContext _context;

        public ResultsController(NodesContext context){
            _context = context;
        }

        [HttpGet]
        public IEnumerable<ProcessResultModel> Get()
        {
            ProcessResultModel[] data = new ProcessResultModel[0];
            try
            {
                DateTime dtStart = DateTime.Now.Date.AddMonths(-1);
                DateTime dtEnd = DateTime.Now;

                data = _context.ProcessResults.Where(d => d.CreatedDate >= dtStart && d.CreatedDate <= dtEnd)
                    .Select(d => new ProcessResultModel{
                        CreatedDate = d.CreatedDate,
                        Id = d.Id,
                        NumResult = d.NumResult,
                        ProcessStepId = d.ProcessStepId,
                        StrResult = d.StrResult,
                        DurationInSeconds = d.DurationInSeconds,
                        IsTestResult = d.ProcessStep.IsTestResult,
                        IsOk = d.IsOk,
                    }).ToArray();
            }
            catch
            {
                
            }
            
            return data;
        }

        [HttpGet]
        [Route("App/{appId}")]
        public IEnumerable<ProcessResultModel> GetApp(int appId){
            ProcessResultModel[] data = new ProcessResultModel[0];
            try
            {
                DateTime dtStart = DateTime.Now.Date.AddMonths(-1);
                DateTime dtEnd = DateTime.Now;

                data = _context.ProcessResults.Where(d => d.CreatedDate >= dtStart && d.CreatedDate <= dtEnd
                        && d.ProcessStep.HnProcess.HnAppId == appId)
                    .Select(d => new ProcessResultModel{
                        CreatedDate = d.CreatedDate,
                        Id = d.Id,
                        NumResult = d.NumResult,
                        ProcessStepId = d.ProcessStepId,
                        StrResult = d.StrResult,
                        DurationInSeconds = d.DurationInSeconds,
                        IsTestResult = d.ProcessStep.IsTestResult,
                        IsOk = d.IsOk,
                    }).ToArray();
            }
            catch
            {
                
            }
            
            return data;
        }

        [HttpGet]
        [Route("Process/{processId}")]
        public IEnumerable<ProcessResultModel> GetProc(int processId){
            ProcessResultModel[] data = new ProcessResultModel[0];
            try
            {
                DateTime dtStart = DateTime.Now.Date.AddMonths(-1);
                DateTime dtEnd = DateTime.Now;

                data = _context.ProcessResults.Where(d => d.CreatedDate >= dtStart && d.CreatedDate <= dtEnd
                        && d.ProcessStep.HnProcess.HnProcessId == processId)
                    .Select(d => new ProcessResultModel{
                        CreatedDate = d.CreatedDate,
                        Id = d.Id,
                        NumResult = d.NumResult,
                        ProcessStepId = d.ProcessStepId,
                        StrResult = d.StrResult,
                        DurationInSeconds = d.DurationInSeconds,
                        IsTestResult = d.ProcessStep.IsTestResult,
                        IsOk = d.IsOk,
                    }).ToArray();
            }
            catch
            {
                
            }
            
            return data;
        }

        [HttpGet]
        [Route("DataSheet/Process/{processId}")]
        public IActionResult AllDataOfProcess(int processId){
            try
            {
                  #region PREPARE DATA
            ProcessReportModel[] data = new ProcessReportModel[0];
            try
            {
                data = _context.ProcessResults.Where(d => d.ProcessStep.HnProcess.HnProcessId == processId)
                    .Select(d => new ProcessReportModel{
                        CreatedDate = d.CreatedDate,
                        StepName = d.ProcessStep != null ? d.ProcessStep.Explanation : "",
                        Duration = d.DurationInSeconds,
                        ItemNo = d.Item != null ? d.Item.ItemCode : "",
                        ItemName = d.Item != null ? d.Item.ItemName : "",
                        IsOk = d.IsOk ?? false,
                    }).ToArray();
            }
            catch
            {
                
            }
            #endregion

            #region PREPARE EXCEL FILE
            byte[] excelFile = new byte[0];

            using (var workbook = new XLWorkbook()) {
                var worksheet = workbook.Worksheets.Add("Test Report");

                worksheet.Cell(1,1).Value = "Tarih";
                worksheet.Cell(1,2).Value = "Test Adımı";
                worksheet.Cell(1,3).Value = "Malzeme No";
                worksheet.Cell(1,4).Value = "Malzeme Adı";
                worksheet.Cell(1,5).Value = "Sonuç";
                worksheet.Cell(1,6).Value = "Test Süresi(sn)";

                worksheet.Cell(2,1).InsertData(data);

                worksheet.Columns().AdjustToContents();

                var titlesStyle = workbook.Style;
                titlesStyle.Font.Bold = true;
                titlesStyle.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                worksheet.Row(1).Style = titlesStyle;

                using (MemoryStream memoryStream = new MemoryStream()) {
                    workbook.SaveAs(memoryStream);
                    excelFile = memoryStream.ToArray();
                }

                return Ok(excelFile);
            }

            #endregion
            
            }
            catch (System.Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet]
        [Route("LastResult/{processId}")]
        public ProcessResultModel GetLastResult(int processId){
            ProcessResultModel data = new ProcessResultModel();
            try
            {
                data = _context.ProcessResults.Where(d => d.ProcessStep.HnProcessId == processId)
                .OrderByDescending(d => d.Id)
                .Select(d => new ProcessResultModel{
                        Id = d.Id,
                        CreatedDate = d.CreatedDate,
                        NumResult = d.NumResult,
                        StrResult = d.StrResult,
                        ProcessStepId = d.ProcessStepId,
                        DurationInSeconds = d.DurationInSeconds,
                        IsOk = d.IsOk,
                    }).FirstOrDefault();
            }
            catch
            {
                
            }
            
            return data;
        }

        [HttpGet]
        [Route("{id}")]
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
                        IsOk = d.IsOk,
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
                dbObj.DurationInSeconds = model.DurationInSeconds;
                dbObj.IsOk = model.IsOk;

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
