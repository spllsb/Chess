
using System;
using System.Threading.Tasks;
using Chess.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Chess.WebSite.Views.Shared.Components.PlayerList
{
    public class DiagramViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(string chart_type)
        {
 
            if(chart_type == DiagramTypeEnum.donut.ToString())
            {
                return View("DonutDiagram", chart_type);
            }
            return View("Default", chart_type);
        }


        
        private enum DiagramTypeEnum{
            donut
        }
    }
} 



