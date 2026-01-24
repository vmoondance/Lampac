using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared;
using System;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace Lampac.Controllers
{
    public class PlayerInnerController : BaseController
    {
        [HttpGet]
        [Route("player-inner/{*uri}")]
        public void PlayerInner(string uri)
        {
            if (string.IsNullOrEmpty(AppInit.conf.playerInner))
                return;

            // убираем мусор в ссылке
            uri = Regex.Replace(uri, "[^a-z0-9_:\\-\\/\\.\\=\\?\\&\\%\\@]+", "", RegexOptions.IgnoreCase);
            uri = uri + HttpContext.Request.QueryString.Value;

            if (!Uri.TryCreate(uri, UriKind.Absolute, out var stream) || 
                (stream.Scheme != Uri.UriSchemeHttp && stream.Scheme != Uri.UriSchemeHttps))
                return;

            Process.Start(new ProcessStartInfo()
            {
                FileName = AppInit.conf.playerInner,
                Arguments = stream.AbsoluteUri
            });
        }
    }
}