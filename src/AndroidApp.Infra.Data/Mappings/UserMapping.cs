using System;
using AndroidApp.Infra.Data.Extensions;
using AndroidApp.Domain.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AndroidApp.Infra.Data.Mappings
{
    internal class UserMapping : DbEntityConfiguration<User>
    {
        public override void Map(EntityTypeBuilder<User> builder)
        {
            throw new NotImplementedException();
        }
    }
}