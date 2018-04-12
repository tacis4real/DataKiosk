namespace DataKioskStacks.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migrationX1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "EnrollKiosk.ClientStation",
                c => new
                    {
                        ClientStationId = c.Long(nullable: false, identity: true),
                        OrganizationId = c.Long(nullable: false),
                        StationName = c.String(nullable: false, maxLength: 150, unicode: false),
                        StationId = c.String(nullable: false, maxLength: 8000, unicode: false),
                        APIAccessKey = c.String(nullable: false, maxLength: 10, unicode: false),
                        StateId = c.Int(nullable: false),
                        LocalAreaId = c.Int(nullable: false),
                        TimeStampRegistered = c.DateTime(nullable: false),
                        RegisteredByUserId = c.Long(nullable: false),
                        Status = c.Int(nullable: false),
                        LocalArea_LocalAreaId = c.Long(),
                    })
                .PrimaryKey(t => t.ClientStationId)
                .ForeignKey("EnrollKiosk.LocalArea", t => t.LocalArea_LocalAreaId)
                .ForeignKey("EnrollKiosk.Organization", t => t.OrganizationId, cascadeDelete: true)
                .Index(t => t.OrganizationId)
                .Index(t => t.StationId, unique: true, name: "IX_Station_Id")
                .Index(t => t.APIAccessKey, unique: true, name: "IX_APIAccessKey_Key")
                .Index(t => t.LocalArea_LocalAreaId);
            
            CreateTable(
                "EnrollKiosk.Beneficiary",
                c => new
                    {
                        BeneficiaryId = c.Long(nullable: false, identity: true),
                        BeneficiaryRemoteId = c.Long(nullable: false),
                        RecordId = c.Int(nullable: false),
                        ClientStationId = c.Long(nullable: false),
                        EnrollerId = c.Long(nullable: false),
                        Surname = c.String(nullable: false, maxLength: 100),
                        FirstName = c.String(nullable: false, maxLength: 200),
                        Othernames = c.String(),
                        DateOfBirth = c.String(nullable: false, maxLength: 10),
                        MobileNumber = c.String(),
                        ResidentialAddress = c.String(nullable: false, maxLength: 200),
                        OfficeAddress = c.String(maxLength: 200),
                        StateId = c.Int(nullable: false),
                        LocalAreaId = c.Int(nullable: false),
                        Sex = c.Int(nullable: false),
                        MaritalStatus = c.Int(nullable: false),
                        OccupationId = c.Int(nullable: false),
                        RemoteImageFileName = c.String(),
                        ImageFileName = c.String(),
                        ImagePath = c.String(),
                        FingerPrintTemplates = c.Binary(),
                        TimeStampRegistered = c.DateTime(nullable: false),
                        Status = c.Int(nullable: false),
                        SurnameHashed = c.Int(nullable: false),
                        FirstNameHashed = c.Int(nullable: false),
                        OtherNameHashed = c.Int(nullable: false),
                        DateOfBirthHashed = c.Int(nullable: false),
                        MobileNoHashed = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.BeneficiaryId)
                .ForeignKey("EnrollKiosk.ClientStation", t => t.ClientStationId, cascadeDelete: true)
                .ForeignKey("EnrollKiosk.Enroller", t => t.EnrollerId, cascadeDelete: true)
                .Index(t => t.ClientStationId)
                .Index(t => t.EnrollerId);
            
            CreateTable(
                "EnrollKiosk.Enroller",
                c => new
                    {
                        EnrollerId = c.Long(nullable: false, identity: true),
                        ClientStationId = c.Long(nullable: false),
                        EnrollerRegId = c.String(nullable: false, maxLength: 8000, unicode: false),
                        Surname = c.String(nullable: false, maxLength: 150, unicode: false),
                        FirstName = c.String(nullable: false, maxLength: 150, unicode: false),
                        OtherNames = c.String(),
                        Sex = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 20),
                        Password = c.String(nullable: false, maxLength: 50),
                        Salt = c.String(),
                        UserCode = c.String(),
                        MobileNumber = c.String(nullable: false, maxLength: 15, unicode: false),
                        Email = c.String(maxLength: 50),
                        Address = c.String(nullable: false, maxLength: 200, unicode: false),
                        DeviceId = c.String(maxLength: 50, unicode: false),
                        DeviceIP = c.String(maxLength: 15, unicode: false),
                        TimeStampRegistered = c.DateTime(nullable: false),
                        RegisteredByUserId = c.Long(nullable: false),
                        Status = c.Int(nullable: false),
                        TimeStampAuthorized = c.DateTime(),
                    })
                .PrimaryKey(t => t.EnrollerId)
                .Index(t => t.EnrollerRegId, unique: true, name: "IX_OperatorReg_Id");
            
            CreateTable(
                "EnrollKiosk.LocalArea",
                c => new
                    {
                        LocalAreaId = c.Long(nullable: false, identity: true),
                        StateId = c.Int(nullable: false),
                        Name = c.String(),
                        Status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.LocalAreaId);
            
            CreateTable(
                "EnrollKiosk.Organization",
                c => new
                    {
                        OrganizationId = c.Long(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 500, unicode: false),
                        Address = c.String(nullable: false, maxLength: 200, unicode: false),
                        Email = c.String(maxLength: 50),
                        PhoneNumber = c.String(),
                        TimeStampRegistered = c.DateTime(nullable: false),
                        Status = c.Int(nullable: false),
                        RegisteredByUserId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.OrganizationId);
            
            CreateTable(
                "EnrollKiosk.User",
                c => new
                    {
                        UserId = c.Long(nullable: false, identity: true),
                        OrganizationId = c.Long(nullable: false),
                        UserTypeId = c.Int(nullable: false),
                        RoleId = c.Int(nullable: false),
                        Surname = c.String(nullable: false, maxLength: 100, unicode: false),
                        FirstName = c.String(nullable: false, maxLength: 200, unicode: false),
                        MobileNumber = c.String(nullable: false, maxLength: 15, unicode: false),
                        Sex = c.Int(nullable: false),
                        Email = c.String(maxLength: 50),
                        Username = c.String(maxLength: 20, unicode: false),
                        UserCode = c.String(nullable: false, maxLength: 200, unicode: false),
                        AccessCode = c.String(nullable: false, maxLength: 200, unicode: false),
                        Password = c.String(nullable: false, maxLength: 200, unicode: false),
                        IsLockedOut = c.Boolean(nullable: false),
                        IsApproved = c.Boolean(nullable: false),
                        IsFirstTimeLogin = c.Boolean(nullable: false),
                        PasswordChangeTimeStamp = c.String(maxLength: 35, unicode: false),
                        LastLoginTimeStamp = c.String(maxLength: 35, unicode: false),
                        LastLockedOutTimeStamp = c.String(maxLength: 35, unicode: false),
                        TimeStampRegistered = c.String(nullable: false, maxLength: 35, unicode: false),
                        FailedPasswordCount = c.Int(nullable: false),
                        IsPasswordChangeRequired = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        IsWebActive = c.Boolean(nullable: false),
                        IsMobileActive = c.Boolean(nullable: false),
                        RegisteredByUserId = c.Int(nullable: false),
                        IsEmailVerified = c.Boolean(nullable: false),
                        IsMobileNumberVerified = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("EnrollKiosk.UserType", t => t.UserTypeId, cascadeDelete: true)
                .ForeignKey("EnrollKiosk.Organization", t => t.OrganizationId, cascadeDelete: true)
                .Index(t => t.OrganizationId)
                .Index(t => t.UserTypeId)
                .Index(t => t.Email, unique: true, name: "UQ_User_Email");
            
            CreateTable(
                "EnrollKiosk.UserDevice",
                c => new
                    {
                        UserDeviceId = c.Long(nullable: false, identity: true),
                        UserId = c.Long(nullable: false),
                        DeviceSerialNumber = c.String(nullable: false, maxLength: 50, unicode: false),
                        DeviceName = c.String(maxLength: 50, unicode: false),
                        NotificationCode = c.String(unicode: false, storeType: "text"),
                        DeviceOSType = c.Int(nullable: false),
                        IsAuthorized = c.Boolean(nullable: false),
                        AuthorizationCode = c.String(maxLength: 10, unicode: false),
                        TimeStampRegistered = c.String(nullable: false, maxLength: 35, unicode: false),
                    })
                .PrimaryKey(t => t.UserDeviceId)
                .ForeignKey("EnrollKiosk.User", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "EnrollKiosk.DeviceAccessAuthorization",
                c => new
                    {
                        DeviceAccessAuthorizationId = c.Long(nullable: false, identity: true),
                        UserLoginActivityId = c.Long(nullable: false),
                        UserId = c.Long(nullable: false),
                        UserDeviceId = c.Long(nullable: false),
                        AuthorizedDate = c.String(nullable: false, maxLength: 10, unicode: false),
                        AuthorizedTime = c.String(nullable: false, maxLength: 15, unicode: false),
                        AuthorizedDeviceSerialNumber = c.String(nullable: false, maxLength: 50, unicode: false),
                        AuthorizationToken = c.String(nullable: false, maxLength: 50, unicode: false),
                        IsExpired = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.DeviceAccessAuthorizationId)
                .ForeignKey("EnrollKiosk.UserDevice", t => t.UserDeviceId, cascadeDelete: true)
                .Index(t => t.UserDeviceId);
            
            CreateTable(
                "EnrollKiosk.UserLoginActivity",
                c => new
                    {
                        UserLoginActivityId = c.Long(nullable: false, identity: true),
                        UserId = c.Long(nullable: false),
                        IsLoggedIn = c.Boolean(nullable: false),
                        LoginSource = c.Int(nullable: false),
                        LoginAddress = c.String(maxLength: 50, unicode: false),
                        LoginToken = c.String(maxLength: 50, unicode: false),
                        LoginTimeStamp = c.String(nullable: false, maxLength: 35, unicode: false),
                    })
                .PrimaryKey(t => t.UserLoginActivityId)
                .ForeignKey("EnrollKiosk.User", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "EnrollKiosk.UserType",
                c => new
                    {
                        UserTypeId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.UserTypeId);
            
            CreateTable(
                "EnrollKiosk.Role",
                c => new
                    {
                        RoleId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 250, unicode: false),
                        Description = c.String(maxLength: 200, unicode: false),
                        Status = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.RoleId);
            
            CreateTable(
                "EnrollKiosk.UserRole",
                c => new
                    {
                        UserRoleId = c.Int(nullable: false, identity: true),
                        UserId = c.Long(nullable: false),
                        RoleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.UserRoleId)
                .ForeignKey("EnrollKiosk.User", t => t.UserId, cascadeDelete: true)
                .ForeignKey("EnrollKiosk.Role", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "EnrollKiosk.SerialNumberKeeper",
                c => new
                    {
                        SerialNumberKeeperId = c.Long(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.SerialNumberKeeperId);
            
            CreateTable(
                "EnrollKiosk.State",
                c => new
                    {
                        StateId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.StateId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("EnrollKiosk.UserRole", "RoleId", "EnrollKiosk.Role");
            DropForeignKey("EnrollKiosk.UserRole", "UserId", "EnrollKiosk.User");
            DropForeignKey("EnrollKiosk.User", "OrganizationId", "EnrollKiosk.Organization");
            DropForeignKey("EnrollKiosk.User", "UserTypeId", "EnrollKiosk.UserType");
            DropForeignKey("EnrollKiosk.UserLoginActivity", "UserId", "EnrollKiosk.User");
            DropForeignKey("EnrollKiosk.UserDevice", "UserId", "EnrollKiosk.User");
            DropForeignKey("EnrollKiosk.DeviceAccessAuthorization", "UserDeviceId", "EnrollKiosk.UserDevice");
            DropForeignKey("EnrollKiosk.ClientStation", "OrganizationId", "EnrollKiosk.Organization");
            DropForeignKey("EnrollKiosk.ClientStation", "LocalArea_LocalAreaId", "EnrollKiosk.LocalArea");
            DropForeignKey("EnrollKiosk.Beneficiary", "EnrollerId", "EnrollKiosk.Enroller");
            DropForeignKey("EnrollKiosk.Beneficiary", "ClientStationId", "EnrollKiosk.ClientStation");
            DropIndex("EnrollKiosk.UserRole", new[] { "RoleId" });
            DropIndex("EnrollKiosk.UserRole", new[] { "UserId" });
            DropIndex("EnrollKiosk.UserLoginActivity", new[] { "UserId" });
            DropIndex("EnrollKiosk.DeviceAccessAuthorization", new[] { "UserDeviceId" });
            DropIndex("EnrollKiosk.UserDevice", new[] { "UserId" });
            DropIndex("EnrollKiosk.User", "UQ_User_Email");
            DropIndex("EnrollKiosk.User", new[] { "UserTypeId" });
            DropIndex("EnrollKiosk.User", new[] { "OrganizationId" });
            DropIndex("EnrollKiosk.Enroller", "IX_OperatorReg_Id");
            DropIndex("EnrollKiosk.Beneficiary", new[] { "EnrollerId" });
            DropIndex("EnrollKiosk.Beneficiary", new[] { "ClientStationId" });
            DropIndex("EnrollKiosk.ClientStation", new[] { "LocalArea_LocalAreaId" });
            DropIndex("EnrollKiosk.ClientStation", "IX_APIAccessKey_Key");
            DropIndex("EnrollKiosk.ClientStation", "IX_Station_Id");
            DropIndex("EnrollKiosk.ClientStation", new[] { "OrganizationId" });
            DropTable("EnrollKiosk.State");
            DropTable("EnrollKiosk.SerialNumberKeeper");
            DropTable("EnrollKiosk.UserRole");
            DropTable("EnrollKiosk.Role");
            DropTable("EnrollKiosk.UserType");
            DropTable("EnrollKiosk.UserLoginActivity");
            DropTable("EnrollKiosk.DeviceAccessAuthorization");
            DropTable("EnrollKiosk.UserDevice");
            DropTable("EnrollKiosk.User");
            DropTable("EnrollKiosk.Organization");
            DropTable("EnrollKiosk.LocalArea");
            DropTable("EnrollKiosk.Enroller");
            DropTable("EnrollKiosk.Beneficiary");
            DropTable("EnrollKiosk.ClientStation");
        }
    }
}
