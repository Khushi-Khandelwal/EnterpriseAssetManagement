﻿using Microsoft.AspNetCore.Mvc.Rendering;

namespace AssetManagement.Models
{
    public class MyViewModel
    {
        public string SelectedValue { get; set; }
        public IEnumerable<SelectListItem> Values { get; set; }

        //... some other properties that your view might need
    }
}
