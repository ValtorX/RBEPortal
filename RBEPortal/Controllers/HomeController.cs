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
                        .Include(o => o.ModifiedByUser)
                        .ToList();
                    if (model.Resources.Count < 1)
                        model.Resources = null;
                } else {
                    model.Resources = session.RBEPortalData.Resources
                        .Where(o => o.Status == "active")
                        .OrderBy(o => o.Name)
                        .Include(o => o.ModifiedByUser)
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
                model.Resource = session.RBEPortalData.Resources
                    .Include(o => o.ModifiedByUser)
                    .Single(o => o.ResourceId == resourceId);

                model.Resource.Requests = session.RBEPortalData.Requests
                    .Where(o => o.ResourceId == resourceId && o.Status == "active")
                    .OrderBy(o => o.Location)
                    .Include(o => o.User)
                    .ToList();

                model.Resource.Shares = session.RBEPortalData.Shares
                    .Where(o => o.ResourceId == resourceId && o.Status == "active")
                    .OrderBy(o => o.Location)
                    .Include(o => o.User)
                    .ToList();
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

        public ActionResult NewRequest(Guid resourceId) {
            var model = new RequestModel();

            using (var session = new RBEPortalServer.RBEPortalContext()) {
                var resource = session.RBEPortalData.Resources.Single(o => o.ResourceId == resourceId);

                model.Request = new Request {
                    ResourceId = resourceId,
                    Resource = resource,
                };
            }

            return View("CreateRequest", model);
        }

        public ActionResult SaveRequest(RequestModel model) {
            if (!User.Identity.IsAuthenticated)
                return View("NotLogged");

            model.Request.Location = HttpUtility.HtmlDecode(model.Request.Location);

            using (var session = new RBEPortalServer.RBEPortalContext()) {
                var userId = session.RBEPortalData.Users.Where(o => o.LoweredUserName == User.Identity.Name.ToLower()).Select(o => o.UserId).Single();

                Request request;
                if (model.Request.RequestId == Guid.Empty)
                    request = null;
                else
                    request = session.RBEPortalData.Requests.SingleOrDefault(o => o.RequestId == model.Request.RequestId);

                if (request == null) {
                    request = new Request {
                        RequestId = Guid.NewGuid(),
                        ResourceId = model.Request.ResourceId,
                        UserId = userId,
                        Status = "active",
                        CreationDate = DateTime.Now,
                    };
                    session.RBEPortalData.Requests.Add(request);
                }

                request.Amount = model.Request.Amount;
                request.UoM = model.Request.UoM;
                request.Location = model.Request.Location;
                request.ModifiedDate = DateTime.Now;
                request.ModifiedBy = userId;

                session.RBEPortalData.SaveChanges();
            }

            return DisplayResource(model.Request.ResourceId);
        }

        public ActionResult DeleteRequest(Guid requestId) {
            if (!User.Identity.IsAuthenticated)
                return View("NotLogged");

            Request request;
            using (var session = new RBEPortalServer.RBEPortalContext()) {
                var userId = session.RBEPortalData.Users.Where(o => o.LoweredUserName == User.Identity.Name.ToLower()).Select(o => o.UserId).Single();
                request = session.RBEPortalData.Requests.Single(o => o.RequestId == requestId);

                request.Status = "deleted";
                request.ModifiedDate = DateTime.Now;
                request.ModifiedBy = userId;

                session.RBEPortalData.SaveChanges();
            }

            return DisplayResource(request.ResourceId);
        }

        public ActionResult NewShare(Guid resourceId) {
            var model = new ShareModel();

            using (var session = new RBEPortalServer.RBEPortalContext()) {
                var resource = session.RBEPortalData.Resources.Single(o => o.ResourceId == resourceId);

                model.Share = new Share {
                    ResourceId = resourceId,
                    Resource = resource,
                };
            }

            return View("CreateShare", model);
        }

        public ActionResult SaveShare(ShareModel model) {
            if (!User.Identity.IsAuthenticated)
                return View("NotLogged");

            model.Share.Location = HttpUtility.HtmlDecode(model.Share.Location);

            using (var session = new RBEPortalServer.RBEPortalContext()) {
                var userId = session.RBEPortalData.Users.Where(o => o.LoweredUserName == User.Identity.Name.ToLower()).Select(o => o.UserId).Single();

                Share share;
                if (model.Share.ShareId == Guid.Empty)
                    share = null;
                else
                    share = session.RBEPortalData.Shares.SingleOrDefault(o => o.ShareId == model.Share.ShareId);

                if (share == null) {
                    share = new Share {
                        ShareId = Guid.NewGuid(),
                        ResourceId = model.Share.ResourceId,
                        UserId = userId,
                        Status = "active",
                        CreationDate = DateTime.Now,
                    };
                    session.RBEPortalData.Shares.Add(share);
                }

                share.Amount = model.Share.Amount;
                share.UoM = model.Share.UoM;
                share.Location = model.Share.Location;
                share.ModifiedDate = DateTime.Now;
                share.ModifiedBy = userId;

                session.RBEPortalData.SaveChanges();
            }

            return DisplayResource(model.Share.ResourceId);
        }

        public ActionResult DeleteShare(Guid shareId) {
            if (!User.Identity.IsAuthenticated)
                return View("NotLogged");

            Share share;
            using (var session = new RBEPortalServer.RBEPortalContext()) {
                var userId = session.RBEPortalData.Users.Where(o => o.LoweredUserName == User.Identity.Name.ToLower()).Select(o => o.UserId).Single();
                share = session.RBEPortalData.Shares.Single(o => o.ShareId == shareId);

                share.Status = "deleted";
                share.ModifiedDate = DateTime.Now;
                share.ModifiedBy = userId;

                session.RBEPortalData.SaveChanges();
            }

            return DisplayResource(share.ResourceId);
        }
    }
}
