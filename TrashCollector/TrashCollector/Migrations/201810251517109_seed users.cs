namespace TrashCollector.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class seedusers : DbMigration
    {
        public override void Up()
        {
            Sql(@"
INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'1968484d-9dab-46d2-aff9-492ac1771dce', N'guest@vidly.com', 0, N'AE2wD4Wyfbi6GL8Ca5QHPJNi4DT5a4DdG4vmh1+EVC1lCWsoJsMYlpPUwNMfLBtw8A==', N'1ca4ed1b-d614-4469-b923-d53d68211b88', NULL, 0, 0, NULL, 1, 0, N'guest@vidly.com')
INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'9891a176-d091-4750-955c-1033c4c1ab74', N'customer@vidly.com', 0, N'AA5dvLY4HTPxZidOELOi1b0RsoJeS9sZOpuLfaHzjF8trqZW5NGb8nkjMzAberXKRQ==', N'04732bdc-161f-40c5-89dc-79da01cf5d67', NULL, 0, 0, NULL, 1, 0, N'customer@vidly.com')
INSERT INTO[dbo].[AspNetRoles]([Id], [Name]) VALUES(N'542995ae-3236-4598-b4fc-8d8d89fa049a', N'Customer')
INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'9891a176-d091-4750-955c-1033c4c1ab74', N'542995ae-3236-4598-b4fc-8d8d89fa049a')
");



    }

    public override void Down()
        {
        }
    }
}
