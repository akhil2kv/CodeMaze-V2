using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Configuration
{
    public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.HasData
                (
                 new Employee
                 {
                    Id = new Guid("75c4c053-49b6-4f3f-9a8c-8f6d5a24f19a"),
                    Name = "Sam Raiden",
                    Age = 26,
                    Position = "Software developer",
                    CompanyId = new Guid("c9d4c053-49b6-4f3f-9a8c-8f6d5a24f19a")
                },
                 new Employee
                 {
                        Id = new Guid("2dc4c053-49b6-4f3f-9a8c-8f6d5a24f19a"),
                        Name = "Jana McLeaf",
                        Age = 30,
                        Position = "Software developer",
                        CompanyId = new Guid("c9d4c053-49b6-4f3f-9a8c-8f6d5a24f19a")
                    },
                    new Employee
                    {
                        Id = new Guid("3ac4c053-49b6-4f3f-9a8c-8f6d5a24f19a"),
                        Name = "Kane Miller",
                        Age = 35,
                        Position = "Administrator",
                        CompanyId = new Guid("3d490a70-94ce-4d15-9494-5248280c2ce4")
                    }
                );

        }
    }
}
