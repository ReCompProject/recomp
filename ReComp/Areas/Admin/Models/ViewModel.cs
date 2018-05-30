using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ReComp.Areas.Admin.Models;
using ReComp.Models;

namespace ReComp.Areas.Admin.Controllers
{
    public class ViewModelAbout 
    {
        public AboutU TheAboutU { get; set; }
        public AboutService TheAboutService { get; set; }

        public IEnumerable<ReComp.Models.AboutService> AAboutService { get; set; }
    }



    
    public class ViewModelProject
    {
        public Project TheProject { get; set; }
        public Project_Photo TheProject_Photo { get; set; }
        public Project_sevices TheProject_Service { get; set; }

        public IEnumerable<ReComp.Models.Project_sevices> PProject_Service { get; set; }
        public FileModel fileModels { get; set; }
        public IEnumerable<ReComp.Models.Project> PProject { get; set; }

        public IList<ReComp.Models.Project_Photo> PProject_Photo { get; set; }
    }
    public class ViewModelStick
    { 
        public Sticker TheSticker { get; set; }
        public Stick_Type TheStick_Type { get; set; }
        public Stick_Photo TheStick_Photo { get; set; } 

        public Stick_sxem TheStick_sxem { get; set; }
        public Room TheRoom { get; set; }
        public Room_Type TheRoom_Type { get; set; }
        public Project TheProject { get; set; }
        public FileModel fileModels { get; set; }
        public FileModel fileModelsxem { get; set; }

        public IEnumerable<Sticker> SSticker { get; set; }
        public IEnumerable<Stick_Photo> SSticker_Photo { get; set; }
        public IEnumerable<Stick_sxem> SSticker_Sxem { get; set; }
        public IEnumerable<Room> RRoom { get; set; }
        public IEnumerable<Room_Type> RRoom_Type { get; set; }
        public IEnumerable<Stick_Type> SStick_Type { get; set; }
        public IEnumerable<ReComp.Models.Project> PProject { get; set; }

    }

    public class ViewModelUsers
    {
        public User_Type TheUserType { get; set; }
        public User TheUser { get; set; }

        public IEnumerable<User> UUser { get; set; }
        public IEnumerable<User_Type> UUserType { get; set; }
    }
}

//uarko
