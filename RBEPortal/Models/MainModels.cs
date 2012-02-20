using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Web.Mvc;
using System.Web.Security;
using RBEPortalServer.Schema;
using System.Web;

namespace RBEPortal.Models {

    public class MainModel {
        [DataType(DataType.Text)]
        [Display(Name = "Search resources")]
        public string ResourceName { get; set; }

        public List<Resource> Resources { get; set; }
    }

    public class CreateResourceModel {
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Description")]
        public string Description { get; set; }
    }

    public class DisplayResourceModel {
        public Resource Resource { get; set; }

        public List<Resource> Requests { get; set; }

        public List<Resource> Shared { get; set; }

        public List<Resource> Sponsors { get; set; }
    }
}
