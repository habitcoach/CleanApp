using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clean.Infra.Data.Context
{
    public class RestaurantAuthDBContext:IdentityDbContext
    {
        public RestaurantAuthDBContext(DbContextOptions<RestaurantAuthDBContext> options):base(options)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {

            base.OnModelCreating(builder);
            var readerRoleId = "a77666a4-55b3-4df2-96f7-0dfdbc3252a9";
            var writerRoleId = "bb5128f8-b4f2-4451-98b5-9f95ac7a4434";

            var roles = new List<IdentityRole> {

                new IdentityRole{
                    Id = readerRoleId,
                    ConcurrencyStamp= readerRoleId,
                    Name= "Reader",
                    NormalizedName= "Reader".ToUpper()


                },
                new IdentityRole{
                    Id = writerRoleId,
                    ConcurrencyStamp= writerRoleId,
                    Name= "Writer",
                    NormalizedName= "Writer".ToUpper()
                },
            };

            builder.Entity<IdentityRole>().HasData(roles);
        }
    }
}
