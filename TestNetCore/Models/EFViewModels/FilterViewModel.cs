using Microsoft.AspNetCore.Mvc.Rendering;
//using TestNetCore.Models.PersonalCabinetViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestNetCore.Models.EFViewModels
{
    public class FilterViewModel
    {
        //private List<Company> list;
        //private int? company;
        //private string name;

        //public FilterViewModel(List<CompanySort> companies, int? company, string name)
        public FilterViewModel(List<CompanySort> companies, int? company, string name)
        {
            // устанавливаем начальный элемент, который позволит выбрать всех
            companies.Insert(0, new CompanySort { Name = "Все", Id = 0 });
            Companies = new SelectList(companies, "Id", "Name", company);
            SelectedCompany = company;
            SelectedName = name;
        }

        //public FilterViewModel(List<Company> list, int? company, string name)
        //{
        //    this.list = list;
        //    this.company = company;
        //    this.name = name;
        //}

        public SelectList Companies { get; private set; } // список компаний
        public int? SelectedCompany { get; private set; }   // выбранная компания
        public string SelectedName { get; private set; }    // введенное имя
    }
}
