using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFood.EnumDefine;

public enum StatusEnum
{
    Deleted = -1,
    Active = 1,
    New = 2,
    InActive = 3
}

public enum ExternalLoginProviderEnum
{
    //Facebook = 1,
    Google = 2,
    Apple = 4,
    LocalId = 8,
    Microsoft = 16,
    Zalo = 32,
}

public enum PageTypeEnum
{
    Home = 1,
    NewsCategory = 2,
    NewsDetail = 3,
    LandingPage = 4,
    SpecialEvent = 5,
    ArticlePreview = 6,
    Is404 = 7,
    IsOldVersion = 8,
    NewsCategoryCustom = 9,
    Tag = 10,
    EducationStudentScoresDetail = 11,
    Is301 = 12,
    Is301Rule = 13,
    Premium = 14,
    PremiumSpecial = 15,
    WebsiteTemplate = 100,
}

public enum EmailAttachmentStorageLocation
{
    Blob,
    FileReference,
    Path
}

public enum MailBodyFormat
{
    Text,
    Html
}

[Flags]
public enum PageSettings : long
{
    IsResponsive = 1,
    KeyCacheValidQuerystring = 2,
    EditSettingEnable = 4
}

public enum ComponentConditionTypeEnum
{
    Category = 1,

    //Author = 2,
    //News = 5,
    //Series = 6,
    Tag = 7,
    Event = 8,

    //Type = 9,
    DisplayType = 10,

    //Source = 11,
    //PublishDate = 12,
    //Sticker = 13,
    //Icon = 14,
    //Province = 15,
    //SubEvent = 16,
    //View = 17,
    //CategoryMain = 18,
    //CategorySub = 19,
    Menu = 21,
    Zone = 22,
    //Wiki = 23,
}


public enum ShardingTypeEnum
{
    Account = 1,
    Dealer = 2
}

public enum HttpProtocolEnum
{
    Https = 1,
    Http = 2,
}

public enum StatusAudioEnum
{
    Active = 1,
    InActive = 2
}

public enum WebsiteConfigTypeEnum
{
    Group = 1,
    Item = 2
}
public enum ZoneObjectTypeEnum
{
    Default = 0,
    EventSpecial = 1,
    Category = 2,
    Video = 3,
    Fee = 4,
    HomePage = 5,
    Event = 6,
    Series = 7,
    Province = 8,
    SpecialPosition = 9
}

public enum ZonePositionActionTypeEnum
{
    Change = 1,
    Remove = 2,
}
public enum ActiveStatusEnum
{
    New = 0,
    Approved = 1,
    Rejected = 2,
    Changed = 3,
    Cancel = -1,
}

public enum ZonePositionDisplayTypeEnum
{
    Active = 1,
    Timer = 2,
    Pin = 3
}

public enum ZonePositionType
{
    Article = 1,
    Other = 0,
    Event = 2,
    Series = 3,
    Product = 4,
    Manufacturer = 5,
    Vendor = 6,
    Attribute = 7
}

public enum MobilePageEnum
{
    HomePage = 1,
    Category = 2,
    VideoHomePage = 3,
    VideoCategory = 4,
    PremiumHomePage = 5
}

[Flags]
public enum ZoneOptionEnum
{
    VideoAuto = 1,
    EventAuto = 2,
    SeriesAuto = 4,
    PinSupport = 8,
}
public enum WebsiteDomainTypeEnum
{
    Primary = 1,
    Api = 2,
    Account = 3,
    Comment = 4,
    CdnUpload = 5,
    Embed = 6,
    CssAndJsFile = 7,
    ImageAndVideoFile = 8
}

public enum WebsiteConfigFieldTypeEnum
{
    Text = 1,
    TextArea = 2,
    Image = 3,
    CheckBox = 4,
    Menu = 5
}

public enum WebsiteSettingTypeEnum
{
    SocialTag = 1,
    Ga = 2,
    GaId = 3,
    MaxPageSize = 4,
    CdnDomain = 5,
    CdnDomainSystem = 6,
    CmsDomain = 7,
    IsPms = 8,
    IsAccountSystem = 9,
    CustomerAccountSystemId = 10,
    EmployeeAccountSystemId = 11,
    DomainsAccept = 12,
    ArticleConnectionString = 100,
    CdnDomainEmbed = 101,
    CdnDomainEmbedPublic = 102,
    CdnDomainVideoPublic = 103,
    CdnDomainVideoPrivate = 104,
    CdnDomainPublic = 105,
    CdnDomainPublicNotCopy = 106,
    ImageSizeLimit = 110,
    FileMaxWith = 111,
    FileMinSize = 112,
    FileMinWidth = 113
}

public enum LocaleModuleEnum
{
    [Display(Name = "General Module")] GeneralModule = 0,
    [Display(Name = "FileManager Module")] FileManagerModule = 1,
    [Display(Name = "Reception Module")] ReceptionModule = 2,
    [Display(Name = "News Module")] NewsModule = 3,
    [Display(Name = "Product Module")] ProductModule = 4,
    [Display(Name = "Repair Module")] RepairModule = 5,
}

public enum MenuPosition
{
    CMSMenu = 1,
    MenuHeader = 2,
    MegaMenu = 3,
    MenuFooter = 4,
    MenuUser = 5,
    MenuHeaderSport = 6,
    MenuHeaderVideo = 7,
    MenuLeftVideo = 8,
    HashTagTopHeader = 9,
    MenuHeaderVnnEn = 10,
    MenuFooterVnnEn = 11,
    MenuHeaderDHD = 12,
    MenuInfonet = 13,
    MenuICTNews = 14,
    PMSMenu = 15,
    MenuXeHeader = 16,
    MenuXeFooter = 17,
    MenuGNNHeader = 18,
    MenuGNNFooter = 19,
    Menu997NewsHeader = 20,
    Menu997NewsFooter = 21,
    CMSMobile = 22,
    Test = 23,
    MegaMenuRps = 24,
    PMSBDSMenu = 25,
    MenuBDSHeader = 26,
    MenuBDSFooter = 27,
    CMSDataMenu = 30,
    TrackingCMSMenu = 31,
    TrackingHeatmapUserMenu = 32,
    MenuHeader2Sao = 33,
    MenuMega2Sao = 34,
    MenuFooter2Sao = 35,
    MenuHashTagTopHeader2Sao = 36,
    GoodLink = 37,
    CustomerAppInfo = 38, // Menu thông tin
    CustomerAppUtility = 39, // Menu tiện ích
    CustomerAppWelcome = 40, // Menu welcome sau khi user đăng nhập
}
public enum MenuTypeEnum
{
    Link = 1,
    Category = 2,
    Brand = 3,
    OutSystem = 4,
    Iframe = 5,
    CustomerApp = 6
}

public enum PageDeviceTypeEnum
{
    Desktop = 1,
    Mobile = 2,
    Tablet = 3,
    AMP = 4
}

public enum ComponentTypeEnum
{
    Default = 0,
    Html = 1,
    NewsList = 2,
    NewsDetail = 3,
    Article = 100,
    Layout = 5,
    Pseudonym = 6,
    PseudonymDetail = 7,
    WikiProfiles = 8,
    CategoryCustom = 9,
    Category = 10,
    NewsEvents = 11,
    ArticleSearch = 12,
    SeriesList = 13,
    TagsDetail = 14,
    SeriesDetail = 15,
    EventDetail = 19,
    WikiArticleDetail = 20,
    PartialView = 103,
    CKEditorImages = 1000,
    WikiDiseaseDetail = 21,
    EventPage = 1001,
    Menus = 23,
    EventByZone = 24,
    ArticleByCategoryGroups = 25,
    ArticlesByProvince = 26,
    Sitemap = 27,
    ArticlesByZone = 28,
    Wikis = 29,
    WikiProfilesTopHome = 30,
    ArticlesByWiki = 31,
    EducationScores = 32,
    EducationStudentScores = 33,
    EducationScoreReport = 34,
    EducationMajorGroup = 35,
    EducationMajor = 36,
    University = 37,
    UniversityDetail = 38,
    UniversityScore = 39,
    UniversityGroup = 40,
    LayoutByArticle = 41,
    MappingConfig = 101,
    SpecialPosition = 10000,
    SpecialPositionEvent = 10001
}

public enum ConfigTypeEnum
{
    Other = 0,
    TemplateRenderer = 1
}

public enum NewsAdminSortFieldEnum
{
    Default = 0,
    CreateDate = 1,
    PublishDate = 2,
    PageView = 3,
    Position = 4,
    Comment = 5,
    Event = 6,
    Series = 7,
    UpdatedDate = 8,
    PageView12Hour = 9,
    PageView3Hour = 10,
    PageView6Hour = 11,
    PageView9Hour = 12,
    Wiki = 13,
}

public enum NewsAdminSortTypeEnum
{
    [Display(Name = "Tăng dần")] Asc = 1,
    [Display(Name = "Giảm dần")] Desc = 2
}
public enum SystemOptionEnum
{
    VMSV1 = 1,
    VMSV2 = 2,
    VMSV3 = 3,
    PMSV1 = 4,
    PMSV2 = 5,
    PMSV3 = 6,
    TRACKING = 7,
    NewsManager = 8,
    NewsOnlineManager = 9,
}


public enum AccountStatusEnum
{
    Active = 1,
    InActive = 2,
    Deleted = 3
}

public enum GenderEnum
{
    Male = 1,
    Female = 2,
    Other = 3
}

[Flags]
public enum OtpTypeEnum
{
    OTPByEmail = 1,
    OTPBySMS = 2,
    OTPByApp = 4,
    SmartOTP = 8,
    OTPByZalo = 16,
}

public enum KeyCacheTypeEnum
{
    Component = 1,
    NewsCategory = 2,
    News = 3,
    Event = 4,

    // Parameter = 5,
    MenuPosition = 6,
    NickName = 7,
    Series = 8,
    Tag = 9,
    Wiki = 10,
    Zone = 11,
    Fixture = 12,
    League = 13,
    Standing = 14,
    Season = 15,
    Player = 16,
    Team = 17,
    PlayerStatistic = 18,
    Sitemap = 19,
    Rss = 20,
    Chart = 21,
    Video = 22,
    Website = 23,

    // RequestUrl = 23,
    // Page = 24,
    ProductCategory = 25,
    ProductManufacturer = 26,
    ProductAttributeValueCostSetting = 27,
    ProductAttributeCostSetting = 28,
    AttributeCategoryMappingsLoad = 29,

    ProductAttributeValueHasOption = 30,
    ProductAttributeHasOption = 31,
}

public enum ErrorCodeEnum
{
    NoErrorCode = 0,
    Success = 1,
    Fail = 2,
    ErrorCommentLimit = 3,
    ErrorCommentTime = 4,
    InternalExceptions = 500,
    Unauthorized = 401,
    NullRequestExceptions = 501,
    NotExistExceptions = 503,
    UserNullException = 504,
    IdNullException = 505,
    CurrentWebsiteNullException = 506,
    CurrentCompanyNullException = 507,
    PermissionDeny = 403,
    AntiXss = 502,
    InternalExceptionsNotDefine = 508,
    InternalExceptionsInService = 509,
    AccountPasswordIsConfigured = 510,
    OTPSendLimit = 511,
    OTPVerifyLimit = 512,
    OTPInvalid = 513,
}

public enum AccountTypeEnum
{
    CustomerApp = 1,
    OldSystem = 2,
    Local = 3,
    OldSale = 4,
    OldService = 5
}

public enum LoginTypeEnum
{
    Web = 1,
    CMSApp = 2,
    CustomerApp = 3,
}

[Flags]
public enum AuthenApplicationOptionEnum
{
    ExternalTokenValidate = 1,
    CheckUserExist = 2
}
public enum DeviceCryptographyTypeEnum
{
    TokenLogin = 1
}

[Flags]
public enum ForgotPasswordStatusEnum
{
    New = 1,
    Send = 2,
    Expired = 4,
    Used = 8,
    VerifySuccess = 16
}

public enum AddressTypeEnum
{
    ShippingAddress = 1,
    BillingAddress = 2,
    HomeAddress = 3,
    BusinessAddress = 4,
    ContractAddress = 5,
    RecipientAddress = 6,
    OtherAddress = 7,
    DefaultAddress = 9,
    VehicleDeliveryAddress = 10,
    VehiclePickupAddress = 11,
    InsuranceAddress = 12,
}

public enum AccountSettingEnum
{
    Language = 1,
    Partner = 2,
    DateFormat = 3,
    NumberFormat = 4,
    Dealer = 5,
    FormDisplaySetting = 6,
    TrackingApplication = 7,
}

public enum FileTypeEnum
{
    Undefined = 0,
    Image = 1,
    Video = 2,
    Document = 3,
    Other = 4
}
