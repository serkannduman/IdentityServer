﻿using IdentityServer.API2.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IdentityServer.API2.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class PicturesController : ControllerBase
{
    [Authorize]
    [HttpGet]
    public IActionResult GetPictures()
    {
        var pictures = new List<Picture>()
        {
            new Picture() { Id = 1, Name = "Doğa resmi", Url = "dogaresmi.jpg" },
            new Picture() { Id = 2, Name = "fil resmi", Url = "dogaresmi.jpg" },
            new Picture() { Id = 3, Name = "aslan resmi", Url = "dogaresmi.jpg" },
            new Picture() { Id = 4, Name = "fare resmi", Url = "dogaresmi.jpg" },
            new Picture() { Id = 5, Name = "kedi resmi", Url = "dogaresmi.jpg" },
            new Picture() { Id = 6, Name = "köpek resmi", Url = "dogaresmi.jpg" },
        };
        return Ok(pictures);
    }
}
