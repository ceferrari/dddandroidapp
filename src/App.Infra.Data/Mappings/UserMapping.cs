using System;
using App.Domain.Models;
using App.Infra.Data.Extensions;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Infra.Data.Mappings
{
    internal class UserMapping : DbEntityConfiguration<User>
    {
        public override void Map(EntityTypeBuilder<User> entity)
        {
            throw new NotImplementedException();
        }
    }
}
