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
    public class PrintController : ControllerBase
    {
        NodesContext _context;

        public PrintController(NodesContext context){
            _context = context;
        }

        [HttpGet]
        public IEnumerable<PrintQueueModel> Get()
        {
            PrintQueueModel[] data = new PrintQueueModel[0];
            try
            {
                data = _context.PrintQueues.Where(d => (d.IsPrinted ?? false) == false)
                    .Select(d => new PrintQueueModel{
                        CreatedDate = d.CreatedDate,
                        IsPrinted = d.IsPrinted,
                        ItemCode = d.ItemCode,
                        ItemId = d.ItemId,
                        PrintQueueId = d.PrintQueueId,
                    }).OrderBy(d => d.ItemCode).ToArray();
            }
            catch
            {
                
            }
            
            return data;
        }

        [HttpGet]
        [Route("{id}")]
        public PrintQueueModel Get(int id)
        {
            PrintQueueModel data = new PrintQueueModel();
            try
            {
                data = _context.PrintQueues.Where(d => d.PrintQueueId == id).Select(d => new PrintQueueModel{
                        CreatedDate = d.CreatedDate,
                        IsPrinted = d.IsPrinted,
                        ItemCode = d.ItemCode,
                        ItemId = d.ItemId,
                        PrintQueueId = d.PrintQueueId,
                    }).FirstOrDefault();
            }
            catch
            {
                
            }
            
            return data;
        }

        [HttpGet]
        [Route("SerialNo/{itemId}")]
        public int SerialNo(int itemId){
            try
            {
                var today = DateTime.Now.Date;
                var todayItemCount = _context.PrintQueues.Where(d => d.ItemId == itemId && d.CreatedDate == today
                    && d.IsPrinted == true).Count();
                return todayItemCount;
            }
            catch (System.Exception)
            {
                
            }

            return 0;
        }

        [HttpPost]
        public BusinessResult Post(PrintQueueModel model){
            BusinessResult result = new BusinessResult();

            try
            {
                var dbObj = _context.PrintQueues.FirstOrDefault(d => d.PrintQueueId == model.PrintQueueId);
                if (dbObj == null){
                    dbObj = new PrintQueue();
                    _context.PrintQueues.Add(dbObj);
                }

               dbObj.CreatedDate = model.CreatedDate;
               dbObj.IsPrinted = model.IsPrinted;
               dbObj.ItemCode = model.ItemCode;
               dbObj.ItemId = model.ItemId;

                _context.SaveChanges();
                result.Result=true;
                result.RecordId = dbObj.PrintQueueId;
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
                var dbObj = _context.PrintQueues.FirstOrDefault(d => d.PrintQueueId == id);
                if (dbObj == null)
                    throw new Exception("Silinmesi istenen kayıt bulunamadı.");

                _context.PrintQueues.Remove(dbObj);

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
