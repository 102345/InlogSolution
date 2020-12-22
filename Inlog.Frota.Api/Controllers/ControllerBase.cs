using Inlog.Frota.Api.ViewModels;
using System.Diagnostics;
using System.Web.Http;

namespace Inlog.Frota.Api.Controllers
{
    public class ControllerBase : ApiController
    {
        private Stopwatch _stopWatch;
        private JsonViewModel _jsonResult;

        public Stopwatch StopWatch
        {
            get
            {
                return _stopWatch;
            }
            set
            {
                _stopWatch = value;
            }
        }
        public JsonViewModel JsonResult
        {
            get
            {
                return _jsonResult;
            }
            set
            {
                _jsonResult = value;
            }
        }

        public ControllerBase()
        {
            _stopWatch = new Stopwatch();
            _jsonResult = new JsonViewModel();
        }
    }
}
