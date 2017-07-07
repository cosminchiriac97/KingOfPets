using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Microsoft.AspNetCore.Cors;


namespace KingOfPetsAPI.Controllers
{

    [Route("api/values")]
    public class ValuesController : Controller
    {
      
        // GET api/values?id=lastId&limit=LIMIT
        [HttpGet]
        public IActionResult EmailPagination([FromQuery]string pageOffset, [FromQuery]string limit)
        {
            if(limit == null)
            {
                return BadRequest();
            }
            if (pageOffset == null)
            {
                using (var db = new Model.EmailModel()) {
                    var limitInt = Int32.Parse(limit);
                    var emails = db.Emails.FromSql("SELECT *  FROM Emails ORDER BY ID ASC LIMIT {0}",limitInt).ToList();
                    return Ok(emails);
                }
            }
            else
            {
                using (var db = new Model.EmailModel())
                {
                    var email = db.Emails.Find(Int32.Parse(pageOffset));
                    if (email == null)
                    {
                        return NotFound();
                    }
                    else
                    {
                        var limitInt = Int32.Parse(limit);
                        var pageOffesetInt = Int32.Parse(pageOffset);
                        pageOffesetInt = (pageOffesetInt-1) * limitInt;
                        var emails = db.Emails.FromSql("select * from Emails ORDER BY CREATED_AT ASC LIMIT {0} OFFSET {1}",limitInt, pageOffesetInt).ToList();
                        return Ok(emails);
                    }
                }
            }
        }
        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody]Model.Email emailJson)
        {
           
            if (emailJson.email.Equals(null)) { return BadRequest("Please enter a valid email!"); }
            using (var db = new Model.EmailModel())
            {
                List<Model.Email> emails = (List<Model.Email>)db.Emails.FromSql("select * from Emails where email is {0}", emailJson.email).ToList();
                if (emails.Count != 0) { return BadRequest("You have already subscribed!"); }
            }
   
            if (!Util.EmailSender.sendEmail("chiriac.cosmin97@gmail.com", emailJson.email)) { return BadRequest("Please enter a valid email!"); }

            using (var db = new Model.EmailModel())
            {
                db.Emails.Add(new Model.Email { email = emailJson.email });    
                db.SaveChanges();
                return Ok("Thanks for subscribed us!");
            }
        }
        // POST api/values/email
        [Route("email")]
        [HttpPost]
        public IActionResult PostToAllMemebers([FromBody]Model.EmailContent email)
        {
            Util.EmailSender.SendEmail(email);
            return Ok(email);
        }

    }
}
