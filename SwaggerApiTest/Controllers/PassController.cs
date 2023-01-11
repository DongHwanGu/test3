using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Text.RegularExpressions;

namespace SwaggerApiTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PassController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get([FromQuery] string pass)
        {
            // [1] 영문 대문자
            var result = IsCheckPassword(pass);

            return Ok(new { result = result });
        }

        private ResultCheckPassword IsCheckPassword(string pass)
        {
            int cnt = 0;
            ResultCheckPassword result = new ResultCheckPassword();

            Regex upperRegex = new Regex(@"[A-Z]");
            Boolean isMatchUpper = upperRegex.IsMatch(pass);
            if (isMatchUpper) cnt++;

            Regex lowerRegex = new Regex(@"[a-z]");
            Boolean isMatchLower = lowerRegex.IsMatch(pass);
            if (isMatchLower) cnt++;

            Regex numRegex = new Regex(@"[0-9]");
            Boolean isMatchNum = numRegex.IsMatch(pass);
            if (isMatchNum) cnt++;

            Regex specialRegex = new Regex(@"[~!@\#$%^&*\()\=+|\\/:;?""<>']");
            Boolean isMatchSpecial = specialRegex.IsMatch(pass);
            if (isMatchSpecial) cnt++;

            // 조건 3개 이상 충족 시 8자리
            if (cnt > 2)
            {
                if (pass.Length >= 8)
                {
                    result.Code = 1;
                    result.Message = "성공";
                }
                else
                {
                    result.Code = -1;
                    result.Message = "8자리 충족 안됨";
                }
            }
            // 조건 2개 이상 충족 시 10자리
            else if (cnt > 1)
            {
                if (pass.Length >= 10)
                {
                    result.Code = 1;
                    result.Message = "성공";
                }
                else
                {
                    result.Code = -1;
                    result.Message = "10자리 충족 안됨";
                }
            }
            else
            {
                List<string> arrResult = new List<string>();

                // 충족 안뎀
                if (!isMatchUpper)
                {
                    arrResult.Add("영문 대문자 적용 안됨");
                }
                if (!isMatchLower)
                {
                    arrResult.Add("영문 소문자 적용 안됨");
                }
                if (!isMatchNum)
                {
                    arrResult.Add("숫자 적용 안됨");
                }
                if (!isMatchSpecial)
                {
                    arrResult.Add("특수문자 적용 안됨");
                }

                result.Code = -1;
                result.Message = string.Join(", ", arrResult);
            }

            return result;
        }
    }

    public class ResultCheckPassword
    {
        public int Code { get; set; }
        public string Message { get; set; }
    }
}
