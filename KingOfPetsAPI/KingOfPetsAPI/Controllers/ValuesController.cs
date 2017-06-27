using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using KingOfPetsAPI.Model;
using System;
using KingOfPetsAPI.Util;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace KingOfPetsAPI.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
      
        // GET api/values?id=lastId&limit=LIMIT
        [HttpGet]
        public IActionResult EmailPagination([FromQuery]string id, [FromQuery]string limit)
        {
            if(limit == null)
            {
                return BadRequest();
            }
            if (id == null)
            {
                using (var db = new EmailModel()) {
                    var limitInt = Int32.Parse(limit);
                    var emails = db.Emails.FromSql("SELECT *  FROM Emails ORDER BY ID ASC LIMIT {0}",limitInt).ToList();
                    return Ok(emails);
                }
            }
            else
            {
                using (var db = new EmailModel())
                {
                    var email = db.Emails.Find(Int32.Parse(id));
                    if (email == null)
                    {
                        return NotFound();
                    }
                    else
                    {
                        var limitInt = Int32.Parse(limit);
                        var idInt = Int32.Parse(id);
                        var emails = db.Emails.FromSql("SELECT *  FROM Emails WHERE ID>{0} ORDER BY ID ASC LIMIT {1}", idInt,limitInt).ToList();
                        return Ok(emails);
                    }
                }
            }
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody]Email emailJson)
        {
            if (emailJson.email.Equals(null)) { return BadRequest(emailJson); }

            if (!EmailSender.sendEmail("chiriac.cosmin97@gmail.com", emailJson.email)) { return BadRequest(emailJson); }

            using (var db = new EmailModel())
            {
                db.Emails.Add(new Email { email = emailJson.email });    
                var count = db.SaveChanges();
                return Ok(emailJson);
            }
        }

    }
}
