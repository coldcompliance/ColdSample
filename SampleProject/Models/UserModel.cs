using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SampleProject.Models {
    public class UserModel {

        [Key]
        public int UserID { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public int CompanyID { get; set; }

        public string Company { get; set; }

    }
}