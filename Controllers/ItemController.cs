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
    public class ItemController : ControllerBase
    {
        NodesContext _context;

        public ItemController(NodesContext context){
            _context = context;
        }

        [HttpGet]
        public IEnumerable<ItemModel> Get()
        {
            ItemModel[] data = new ItemModel[0];
            try
            {
                data = _context.Items.Select(d => new ItemModel{
                        ItemId = d.ItemId,
                        IsActive = d.IsActive,
                        ItemCode = d.ItemCode,
                        ItemName = d.ItemName,
                    }).OrderBy(d => d.ItemCode).ToArray();
            }
            catch
            {
                
            }
            
            return data;
        }

        [HttpGet]
        [Route("{id}")]
        public ItemModel Get(int id)
        {
            ItemModel data = new ItemModel();
            try
            {
                data = _context.Items.Where(d => d.ItemId == id).Select(d => new ItemModel{
                       ItemId = d.ItemId,
                        IsActive = d.IsActive,
                        ItemCode = d.ItemCode,
                        ItemName = d.ItemName,
                    }).FirstOrDefault();
            }
            catch
            {
                
            }
            
            return data;
        }

        [HttpPost]
        public BusinessResult Post(ItemModel model){
            BusinessResult result = new BusinessResult();

            try
            {
                var dbObj = _context.Items.FirstOrDefault(d => d.ItemId == model.ItemId);
                if (dbObj == null){
                    dbObj = new Item();
                    _context.Items.Add(dbObj);
                }

               dbObj.ItemId = model.ItemId;
               dbObj.IsActive = model.IsActive;
               dbObj.ItemCode = model.ItemCode;
               dbObj.ItemName = model.ItemName;

                _context.SaveChanges();
                result.Result=true;
                result.RecordId = dbObj.ItemId;
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
                var dbObj = _context.Items.FirstOrDefault(d => d.ItemId == id);
                if (dbObj == null)
                    throw new Exception("Silinmesi istenen kayıt bulunamadı.");

                if (_context.ProcessResults.Any(d => d.ItemId == id)) {
                    throw new Exception("Test kayıtları mevcut olan bir malzeme silinemez.");
                }

                _context.Items.Remove(dbObj);

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
