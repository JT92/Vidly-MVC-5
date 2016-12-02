namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedUsers : DbMigration
    {
        public override void Up()
        {
            Sql(@"
                INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'13b26fd9-252b-4374-b6ac-324d72eef2d2', N'guest@vidly.com', 0, N'AAQVO94JUdl846zPi+UI8fW5gpYSD6Rv/DaGA7BBnj3joquXLP7/6pYIfz+su8lB+A==', N'e463dbcd-de16-4343-80aa-5b5149849a66', NULL, 0, 0, NULL, 1, 0, N'guest@vidly.com')
                INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'8d78c264-148d-46ad-b06e-603411a801af', N'admin@vidly.com', 0, N'AGxUxVtOm6pKNta6jJRnFIfTBBzw5kH69yEZHdgvdzeVk5SHJbKzueHiP/9mO+M4yg==', N'f51c0f84-e67a-4c44-91b3-9ed1249d0674', NULL, 0, 0, NULL, 1, 0, N'admin@vidly.com')
                INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'04a3e682-5743-47d7-920b-03b48233a3be', N'CanManageMovies')
                INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'8d78c264-148d-46ad-b06e-603411a801af', N'04a3e682-5743-47d7-920b-03b48233a3be')
                ");
        }
        
        public override void Down()
        {
        }
    }
}
