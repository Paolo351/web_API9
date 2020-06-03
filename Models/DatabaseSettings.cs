﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace web_API9.Models
{
    public class DatabaseSettings : IBDO_DatabaseSettings
    {

        public string CollectionName_depl { get; set; }
        public string CollectionName_pro { get; set; }
        public string CollectionName_user { get; set; }
        public string CollectionName_db { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
