using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCApp.Controllers
{
    [SessionAuth]
    public abstract class BaseSecuredController:Controller
    {

    }
}
