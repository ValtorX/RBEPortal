using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RBEPortal.Models;
using RBEPortalServer.Schema;
using System.Web.Security;

namespace RBEPortal.Controllers {
    public class HomeController : Controller {
        public ActionResult Index() {
            ViewBag.Message = "Resource-Based Economy Portal";

            return View();
        }

        public ActionResult About() {
            return View();
        }

        public ActionResult NewSearch() {
            return View("Index");
        }

        public ActionResult Search(MainModel model) {
            using (var session = new RBEPortalServer.RBEPortalContext()) {
                var userId = session.RBEPortalData.Users.Where(o => o.LoweredUserName == "valtor").Select(o => o.UserId).SingleOrDefault();
                if (userId == Guid.Empty) {
                    Membership.CreateUser("Valtor", "123456", "valtor.public@gmail.com");
                    userId = session.RBEPortalData.Users.Where(o => o.LoweredUserName == "valtor").Select(o => o.UserId).Single();
                }

                if (!session.RBEPortalData.Resources.Any()) {
                    session.RBEPortalData.Resources.Add(new Resource {
                        ResourceId = Guid.NewGuid(),
                        Name = "Rice",
                        Description = "Rice bio and home grown.",
                        Status = "active",
                        CreationDate = DateTime.Now,
                        ModifiedDate = DateTime.Now,
                        ModifiedBy = userId,
                    });
                    session.RBEPortalData.Resources.Add(new Resource {
                        ResourceId = Guid.NewGuid(),
                        Name = "Corn",
                        Description = "Corn bio and home grown.",
                        Status = "active",
                        CreationDate = DateTime.Now,
                        ModifiedDate = DateTime.Now,
                        ModifiedBy = userId,
                    });
                    session.RBEPortalData.Resources.Add(new Resource {
                        ResourceId = Guid.NewGuid(),
                        Name = "Amaranth",
                        Description = "Amaranth bio and home grown.",
                        Status = "active",
                        CreationDate = DateTime.Now,
                        ModifiedDate = DateTime.Now,
                        ModifiedBy = userId,
                    });
                    session.RBEPortalData.Resources.Add(new Resource {
                        ResourceId = Guid.NewGuid(),
                        Name = "Barley",
                        Description = "Barley bio and home grown.",
                        Status = "active",
                        CreationDate = DateTime.Now,
                        ModifiedDate = DateTime.Now,
                        ModifiedBy = userId,
                    });
                    session.RBEPortalData.Resources.Add(new Resource {
                        ResourceId = Guid.NewGuid(),
                        Name = "Milk",
                        Description = "Milk bio and full fat.",
                        Status = "active",
                        CreationDate = DateTime.Now,
                        ModifiedDate = DateTime.Now,
                        ModifiedBy = userId,
                    });
                    session.RBEPortalData.Resources.Add(new Resource {
                        ResourceId = Guid.NewGuid(),
                        Name = "Shampoo",
                        Description = "<p><span style='font-size:medium;'>Good Shampoo</span></p><p>For <strong>all</strong> <em>Hair</em> types</p><p></p>",
                        Status = "active",
                        CreationDate = DateTime.Now,
                        ModifiedDate = DateTime.Now,
                        ModifiedBy = userId,
                    });
                    
                    session.RBEPortalData.SaveChanges();
                }

                if (model != null && !model.ResourceName.IsNullOrEmpty()) {
                    model.Resources = session.RBEPortalData.Resources
                        .Where(o => (o.Name.Contains(model.ResourceName) || o.Description.Contains(model.ResourceName)) &&
                                    o.Status == "active")
                        .OrderBy(o => o.Name)
                        .Include(o => o.User)
                        .ToList();
                    if (model.Resources.Count < 1)
                        model.Resources = null;
                } else {
                    model.Resources = session.RBEPortalData.Resources
                        .Where(o => o.Status == "active")
                        .OrderBy(o => o.Name)
                        .Include(o => o.User)
                        .ToList();
                }
            }

            return View("Search", model);
        }

        public ActionResult CreateResource(string resourceName) {
            if (!User.Identity.IsAuthenticated)
                return View("NotLogged");

            var model = new CreateResourceModel();

            model.Name = resourceName;

            return View("CreateResource", model);
        }

        public ActionResult SaveNewResource(CreateResourceModel model) {
            if (!User.Identity.IsAuthenticated)
                return View("NotLogged");

            model.Description = HttpUtility.HtmlDecode(model.Description);

            Resource res;
            using (var session = new RBEPortalServer.RBEPortalContext()) {
                var userId = session.RBEPortalData.Users.Where(o => o.LoweredUserName == User.Identity.Name.ToLower()).Select(o => o.UserId).Single();

                res = new Resource {
                    ResourceId = Guid.NewGuid(),
                    Name = model.Name,
                    Description = model.Description,
                    Status = "active",
                    CreationDate = DateTime.Now,
                    ModifiedDate = DateTime.Now,
                    ModifiedBy = userId,
                };
                session.RBEPortalData.Resources.Add(res);
                session.RBEPortalData.SaveChanges();
            }

            return DisplayResource(res.ResourceId);
        }

        public ActionResult DisplayResource(Guid resourceId) {
            var model = new DisplayResourceModel();

            using (var session = new RBEPortalServer.RBEPortalContext()) {
                model.Resource = session.RBEPortalData.Resources.Single(o => o.ResourceId == resourceId);
            }

            return View("DisplayResource", model);
        }

        public ActionResult EditResource(Guid resourceId) {
            var model = new EditResourceModel();
            model.Id = resourceId;

            using (var session = new RBEPortalServer.RBEPortalContext()) {
                var resource = session.RBEPortalData.Resources.Single(o => o.ResourceId == resourceId);
                model.Name = resource.Name;
                model.Description = resource.Description;
            }

            return View("EditResource", model);
        }

        public ActionResult SaveResource(EditResourceModel model) {
            if (!User.Identity.IsAuthenticated)
                return View("NotLogged");

            model.Description = HttpUtility.HtmlDecode(model.Description);

            using (var session = new RBEPortalServer.RBEPortalContext()) {
                var userId = session.RBEPortalData.Users.Where(o => o.LoweredUserName == User.Identity.Name.ToLower()).Select(o => o.UserId).Single();
                var resource = session.RBEPortalData.Resources.Single(o => o.ResourceId == model.Id);

                resource.Name = model.Name;
                resource.Description = model.Description;
                resource.ModifiedDate = DateTime.Now;
                resource.ModifiedBy = userId;

                //session.RBEPortalData.Resources.Add(res);
                session.RBEPortalData.SaveChanges();
            }

            return DisplayResource(model.Id);
        }

        public ActionResult DeleteResource(Guid resourceId) {
            if (!User.Identity.IsAuthenticated)
                return View("NotLogged");

            using (var session = new RBEPortalServer.RBEPortalContext()) {
                var userId = session.RBEPortalData.Users.Where(o => o.LoweredUserName == User.Identity.Name.ToLower()).Select(o => o.UserId).Single();
                var resource = session.RBEPortalData.Resources.Single(o => o.ResourceId == resourceId);

                resource.Status = "deleted";
                resource.ModifiedDate = DateTime.Now;
                resource.ModifiedBy = userId;

                session.RBEPortalData.SaveChanges();
            }

            return NewSearch();
        }
    }
}
