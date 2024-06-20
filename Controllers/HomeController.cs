using Lazy.Captcha.Core;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using verificationcode.Models;

namespace verificationcode.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICaptcha _captcha;
        public HomeController(ILogger<HomeController> logger,ICaptcha captcha)
        {
            _logger = logger;
            _captcha = captcha;
        }
        /// <summary>
        /// 生成验证码
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Captcha(string id)
        {
            var info = _captcha.Generate(id);
            // 有多处验证码且过期时间不一样，可传第二个参数覆盖默认配置。
            //var info = _captcha.Generate(id,120);
            var stream = new MemoryStream(info.Bytes);
            return File(stream, "image/gif");
        }
        /// <summary>
        /// 验证验证码是否输入正确
        /// </summary>
        [HttpGet]
        public bool Validate(string id, string code)
        {
            return _captcha.Validate(id, code);
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
