
using System.ComponentModel;

namespace DataKioskStacks.DataContract.ContractHelpers
{

    public enum Sex
    {
        [Description("Male")]
        Male = 1,

        [Description("Female")]
        Female
    }

    public enum UserLoginSource
    {
        Mobile = 1,
        Web,
    }

    public enum DeviceOSType
    {
        Android = 1,
        BlackBerry,
        Windows_Phone,
        Windows_Desktop, IOS
    }

    public enum ActionType
    {
        Add = 1,
        Update,
        Approved,
        Issued
    }
    
    public enum RegStatus
    {
        New_Registration = 1,
        Uploaded,
        Edited,
        Deleted
    }

    public enum DataValidationName
    {
        Validate_Surname = 1,
        Validate_Othernames,
        Validate_Date_Of_Birth,
        Validate_Sex,
        Validate_Mobile_Number,
        Validate_BusinessName,
        Can_Update_Surname,
        Can_Update_BusinessName,
        Can_Update_BusinessLocation,
        Can_Update_Tax_Category,
        Can_Update_Business_Type,
        Can_Update_Sex,
        Can_Update_Date_Of_Birth,
        Can_Update_Mobile_Number,
        Can_Update_Othernames,
        Can_Delete_Record,
        Can_Edit_Record
    }

    public enum UploadStatus
    {
        Successful = 1,
        Failed = -1,
        Incompleted = 0,
        
    }

    public enum CustomerTitle
    {
        Mr = 1,
        Miss,
        Mrs,
        Alhaji,
        Chief,
        Pastor,
        Dr,
        Lawyer,
        Prof,

    }
    public enum SpecialAnniversaryType
    {
        None = 0,
        Marriage = 1,
        House_Warming,
        Coronation,
        Others,

    }
    public enum BeneficiaryType
    {
        Customer = 1,
        Supplier,
        Staff
    }

    public enum Status
    {
        Active = 1,
        In_Active = -1,
    }

    public enum DefectComplexity
    {
        Useable = 1,
        Manageable,
        Condemned
    }

    public enum ExpenseType
    {
        Cost_Of_Sales = 1,
        Operating_Expenses,
        Non_Operation_Expenses
    }


    public enum TransactionStatus
    {
        Fresh = 1,
        Approved,
        Rejected,
        Processed,
        Closed,
       
    }

    public enum IdentifierNumberType
    {
        Invoice_Number = 1,
        Receipt_Number,
        Transaction_Code,
        Reference_Number,
        Others

    }

    public enum UnitsOfMeasurement
    {
        Meter,
        Kilogram,
        Litre,
        Unit,
        Yard,
        Mass,
        Time,
        Others
    }

    public enum StoreTransactionType
    {
        Entry = 1,
        Issued,
        Disposed,
        Returned
    }
    public enum RequisitionStatus
    {
        Fresh = 1,
        Pending,
        Approved,
        Denied,
        Issued,
        Close,
      
    }
    
    
    public enum AccountInstrumentType
    {
        Teller,
        POS_Receipt,
        Online_Reference,
        Transfer_Code,
        Mobile_Reference
    }

    
    public enum Channel
    {
        Windows = 1,
        Mobile,
        Web,
       
    }

    
    public enum NotificationType
    {
        All = 0,
        SMS = 1,
        Email,
        Push_Notification,
       
    }
    public enum PreferredPaymentMethod
    {
        Any = 0,
        Bank_Deposit = 1,
        Bank_Transfer,
        Cash_Payment,
        Cheque,
        Mobile_Money,
        Online_Transfer,
        Online_Payment

    }


    public static class APIAuth
    {
        public static string ApiUser = "SchoolManagement";
        public static int ApiVersion = 1;
        public static string ApiKey = "d546120ef587fd98b159415da47d3d7d";
    }


}
