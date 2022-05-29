using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using UC001_Beispielprojekt_Firmenpflege.Models;
using PagedList;
using UC001_Beispielprojekt_Firmenpflege.Logic;

namespace UC001_Beispielprojekt_Firmenpflege.Controllers
{
    public class HomeController : Controller
    {
        CompanyEntities ci = new CompanyEntities();
        SearchModel search = new SearchModel();

        public async Task<ActionResult> Index(string sortOrder, int? companyNumber, string companyName, string industry, string city, string holding, int? page, string delete)
        {
            IEnumerable<CompanyModel> cml = await ci.Company_db.ToListAsync();
            List<int> numberQuery= cml.Select(n => n.CompanyNo).Distinct().ToList();
            List<string> industryQuery = cml.Select(i => i.Industry).Distinct().ToList();
            
            search = SaveLogic.GetSearch();

            if (delete == "delete")
            {
                search = new SearchModel();
                SaveLogic.SaveSearch(search);
            }

            int pageSize = 5;
            int pageNumber = (page ?? 1);

            ViewBag.NumberSortParam = String.IsNullOrEmpty(sortOrder) ? "number_desc" : "";
            ViewBag.CurrentSort = sortOrder;

            if ((companyNumber == null && search.CompanyNo == null) && (String.IsNullOrEmpty(companyName) && String.IsNullOrEmpty(search.CompanyName)) && (String.IsNullOrEmpty(industry) && String.IsNullOrEmpty(search.Industry)) && (String.IsNullOrEmpty(city) && String.IsNullOrEmpty(search.City)) && (String.IsNullOrEmpty(holding) && String.IsNullOrEmpty(search.Holding)))
            {
                page = 1;
                switch (sortOrder)
                {
                    case "number_desc":
                        cml = cml.OrderByDescending(n => n.CompanyNo);
                        break;

                    default:

                        cml = cml.OrderBy(n => n.CompanyNo);
                        break;
                }
                var companyNumberIndustryVM = new CompanyNumberIndustryViewModel
                {
                    Companies = cml.ToPagedList(pageNumber, pageSize),
                    Number = new SelectList(numberQuery),
                    IndustrySelect = new SelectList(industryQuery)
                };

                List<CompanyNumberIndustryViewModel> viewModelList = new List<CompanyNumberIndustryViewModel>();

                viewModelList.Add(companyNumberIndustryVM);

                var companyCount = cml.Count();

                var pagedList = new StaticPagedList<CompanyNumberIndustryViewModel>(viewModelList, pageNumber, pageSize, companyCount);

                return View(pagedList);
            }

            else
            {
                if(companyNumber != null || !String.IsNullOrEmpty(companyName) || !String.IsNullOrEmpty(industry) || !String.IsNullOrEmpty(city) || !String.IsNullOrEmpty(holding))
                {
                    search = new SearchModel
                    {
                        CompanyNo = companyNumber,
                        CompanyName = companyName,
                        Industry = industry,
                        City = city,
                        Holding = holding
                    };

                    SaveLogic.SaveSearch(search);
                }

                if (search.CompanyNo != null)
                {
                    cml = cml.Where(cn => cn.CompanyNo == search.CompanyNo);
                }

                if (!String.IsNullOrEmpty(search.CompanyName))
                {
                    companyName = search.CompanyName.ToLower();
                    cml = cml.Where(cn => cn.CompanyName.ToLower().Contains(companyName));
                }

                if (!String.IsNullOrEmpty(search.Industry))
                {
                    industry = search.Industry.ToLower();
                    cml = cml.Where(i => i.Industry.ToLower().Contains(industry));
                }

                if (!String.IsNullOrEmpty(search.City))
                {
                    city = search.City.ToLower();
                    cml = cml.Where(c => c.City.ToLower().Contains(city));
                }

                if (!String.IsNullOrEmpty(search.Holding))
                {
                    holding = search.Holding.ToLower();
                    cml = cml.Where(h => h.Holding.ToLower().Contains(holding));
                }

                switch (sortOrder)
                {
                    case "number_desc":
                        cml = cml.OrderByDescending(n => n.CompanyNo);
                        break;

                    default:
                        cml = cml.OrderBy(n => n.CompanyNo);
                        break;
                }

                var companyNumberIndustryVM = new CompanyNumberIndustryViewModel
                {
                    //Companies = cml.ToPagedList(pageNumber, pageSize),
                    Companies = cml,
                    Number = new SelectList(numberQuery),
                    IndustrySelect = new SelectList(industryQuery)
                };

                List<CompanyNumberIndustryViewModel> viewModelList = new List<CompanyNumberIndustryViewModel>();

                viewModelList.Add(companyNumberIndustryVM);

                var companyCount = cml.Count();

                var pagedList = new StaticPagedList<CompanyNumberIndustryViewModel>(viewModelList, pageNumber, pageSize, companyCount);

                return View(pagedList);
            }
        }
    }
}
