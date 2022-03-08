﻿using FoodForWeek.DAL.Identity.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace FoodForWeek.DAL.Identity
{
    public class AppIdentityContext : IdentityDbContext<AppUser,AppRole,int>
    {
        public AppIdentityContext(DbContextOptions<AppIdentityContext> options) : base(options)
        {

        }
    }
}
