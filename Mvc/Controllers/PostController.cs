using Mvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace Mvc.Controllers
{
    public class PostController : Controller
    {
        // GET: Post
        public ActionResult Index(string searchtext = "", int page = 1, int pageSize = 5)
        {
            IEnumerable<Post> posts;
            IEnumerable<Partner> Partners;
            IEnumerable<Interest> Interests;
            IEnumerable<Post> sorted_posts;

            HttpResponseMessage response = GlobalVariablecs.WebApiClient.GetAsync("Posts").Result;
            posts = response.Content.ReadAsAsync<IEnumerable<Post>>().Result;

            HttpResponseMessage response_partner = GlobalVariablecs.WebApiClient.GetAsync("Partners").Result;
            Partners = response_partner.Content.ReadAsAsync<IEnumerable<Partner>>().Result;

            HttpResponseMessage response_interest = GlobalVariablecs.WebApiClient.GetAsync("Interests").Result;
            Interests = response_interest.Content.ReadAsAsync<IEnumerable<Interest>>().Result;

            var total = posts.Count();
            var skip = pageSize * (page - 1);
            var is_valid_page = skip < total;
            if (!is_valid_page) 
                return View(); 
            if(string.IsNullOrEmpty(searchtext))
            {
                 sorted_posts = posts
                                    .Skip(skip)
                                    .Take(pageSize)
                                    .ToArray();
            }
            else
            {
                 sorted_posts = posts.Where(item => item.post_desc.Contains(searchtext))
                                   .Skip(skip)
                                   .Take(pageSize)
                                   .ToArray();
            }

            float page_count = total / pageSize;
            //var fraction_result = page_count - Math.Truncate(page_count);
            //if(fraction_result > 0)
            //{
            //    ViewBag.page_count = (int)page_count + 1;
            //}
            //else
            //{
            //    ViewBag.page_count = (int)page_count;
            //}

            ViewBag.page_count = (int)page_count;
            ViewBag.totalcount = total;
            foreach (var item in sorted_posts)
            {
                item.user_name = Partners.Where(p => p.id == item.user_id).SingleOrDefault().name;   //Select(p=>p.name).ToString();
                item.entered_date = item.entered_on.ToString("MM/dd/yyyy");
                if (item.parent_id == item.id)
                {
                    var count = posts.Where(i => i.parent_id == item.id && i.id != i.parent_id).Count();
                    item.comment_count = count.ToString() + " Comments";
                }

                else
                {
                    item.comment_count_like = Interests.Where(i => i.post_id == item.id && i.has_interest == 1).Count().ToString();
                    item.comment_count_dis_like = Interests.Where(i => i.post_id == item.id && i.has_interest == 0).Count().ToString();
                }
            }


            //return View(sorted_posts);
            return PartialView(sorted_posts);
        }
    }
}