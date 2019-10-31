using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RateAppLogin.Models
{
    public class Ratings
    {

            public int Id { get; set; }
            public int RateID { get; set; }
            public string RateTxt { get; set; }
            public IEnumerable<SelectListItem> RateSelectedValue { get; set; }
        
    }

}