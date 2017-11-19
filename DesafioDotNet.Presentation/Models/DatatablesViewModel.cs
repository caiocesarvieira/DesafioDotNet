using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DesafioDotNet.Presentation.Models
{
    public class DatatablesViewModel
    {
        public string Id { get; set; }

        public DatatablesViewModel(string id)
        {
            Id = id;
        }
    }
}