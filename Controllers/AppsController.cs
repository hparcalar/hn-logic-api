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
    public class AppsController : ControllerBase
    {
        NodesContext _context;

        public AppsController(NodesContext context){
            _context = context;
        }

        [HttpGet]
        [Route("get")]
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
        [Route("get/{id}")]
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
