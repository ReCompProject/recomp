using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ReComp.Areas.Admin.Models
{
    public class FileModel
    {
        public HttpPostedFileBase[] files { get; set; }
    }
}