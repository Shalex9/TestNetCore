using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestNetCore.Data.TableData;

namespace TestNetCore.Models.EF
{
    public class FilterViewModel
    {
        public FilterViewModel(List<CompanySort> companies, int? company, string name)
        {
            // устанавливаем начальный элемент, который позволит выбрать всех
            companies.Insert(0, new CompanySort { Name = "Все", Id = 0 });
            Companies = new SelectList(companies, "Id", "Name", company);
            SelectedCompany = company;
            SelectedName = name;
        }
        
        public SelectList Companies { get; private set; } // список компаний
        public int? SelectedCompany { get; private set; }   // выбранная компания
        public string SelectedName { get; private set; }    // введенное имя
    }
}