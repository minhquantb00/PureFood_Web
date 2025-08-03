using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFood.Config;

public enum ConfigSettingEnum
{
    AppName,
    AppVersion,
    HttpType,
    HttpPort,
    HttpPort2,
    Https,
    LogEventLevel,
    LogToEsUrl,
    AuthenticationType,
    CookieAuthenName,
    CookieDomain,
    [Display(Name = "JwtTokens:Key")] JwtTokensKey,
    CORS,
    RedisPersistenceDbId,
    DataProtectionRedisKey,
    DataProtectionRedisDbId,
    IsDevEnvironment,
    RabbitMqExChange,
    RabbitMqRoutingRoot,
    RabbitMqExChangeNotify,
    RabbitMqExChangeTrigger,
    RabbitMqPrefetchCount,
    RabbitMqHost,
    VirtualHost,
    RabbitMqUserName,
    RabbitMqPassword,
    EventHandlerFilterQueues,
    RabbitMqRouting,
    RabbitMqQueues,
    RabbitMqExChangeTriggerListen,
    RabbitMqPoolSize,
    RabbitMqExChangeFERequest,
    RabbitMqRoutingFERequest,
    WorkerGroup,

    [Display(Name = "ConnectionStrings:DbConnectionString")]
    DbConnectionString,

    [Display(Name = "ConnectionStrings:DbOldConnectionString")]
    DbOldConnectionString,

    [Display(Name = "ConnectionStrings:DBEntityChangeLoggingConnectionString")]
    DBEntityChangeLoggingConnectionString,
    RedisPersistenceHostIps,
    RedisPassword,
    RedisHostIps,
    RedisCacheDbId,
    RedisPoolSize,
    StartWorker,
    EventDatabaseConnectionString,
    EventDatabaseName,
    EventCollectionName,
    RabbitMqExChangeNotifyListen,
    AdminUserIds,
    LoginExpiresTime,
    PathUrl,
    CdnDomain,
    CdnDomainKey,
    CdnDomainVideo,
    CdnDomainVideoKey,
    CacheEnable,
    EnableUpdateCacheFromWorker,
    IsLoadCache,
    EsUrl,
    AccountManagerUrl,
    SystemManagerUrl,
    ProductManagerUrl,
    LocationManagerUrl,
    NotificationManagerUrl,
    WarehouseManagerUrl,
    SurveyManagerUrl,
    TicketManagerUrl,
    RepairManagerUrl,
    RepairHistoryManagerUrl,
    CustomerManagerUrl,
    TMSSOldSystemUrl,
    TMSSOldSaleSystemUrl,
    LocaleStringResourceManagerUrl,
    TestDriveManagerUrl,
    UtilityManagerUrl,
    OrderManagerUrl,
    NotificationProcessScheduleMessage,
    EmailSendRange,
    [Display(Name = "SmtpServer")] SmtpServer,
    [Display(Name = "SmtpPort")] SmtpPort,
    [Display(Name = "SmtpEnableSsl")] SmtpEnableSsl,
    [Display(Name = "SmtpAccount")] SmtpAccount,
    [Display(Name = "SmtpPassword")] SmtpPassword,
    [Display(Name = "AwsAccessKeyId")] AwsAccessKeyId,
    [Display(Name = "AwsSecretAccessKey")] AwsSecretAccessKey,

    [Display(Name = "AwsAccessKeyIdByMarketing")]
    AwsAccessKeyIdByMarketing,

    [Display(Name = "AwsSecretAccessKeyByMarketing")]
    AwsSecretAccessKeyByMarketing,

    [Display(Name = "NotificationDatabaseSettings:ConnectionString")]
    NotificationDatabaseConnectionString,

    [Display(Name = "NotificationDatabaseSettings:DatabaseName")]
    NotificationDatabaseName,

    [Display(Name = "NotificationDatabaseSettings:EmailCollectionName")]
    EmailCollectionName,

    [Display(Name = "NotificationDatabaseSettings:SMSCollectionName")]
    SMSCollectionName,

    [Display(Name = "NotificationDatabaseSettings:NotificationName")]
    NotificationName,

    [Display(Name = "NotificationDatabaseSettings:NotificationMessageName")]
    NotificationMessageName,

    [Display(Name = "NotificationDatabaseSettings:NotificationSettingName")]
    NotificationSettingName,

    [Display(Name = "NotificationDatabaseSettings:NotificationSettingManager")]
    NotificationSettingManager,

    [Display(Name = "NotificationDatabaseSettings:NotificationUserSetting")]
    NotificationUserSetting,

    [Display(Name = "NotificationDatabaseSettings:SyncEvent")]
    SyncEvent,
    SMSSendRange,
    [Display(Name = "SMSGateWayUrl")] SMSGateWayUrl,
    [Display(Name = "SMSGateWayUser")] SMSGateWayUser,
    [Display(Name = "SMSGateWayPassword")] SMSGateWayPassword,
    [Display(Name = "SMSGateWayCPCode")] SMSGateWayCPCode,

    [Display(Name = "SMSGateWayAuthorizationKey")]
    SMSGateWayAuthorizationKey,

    [Display(Name = "SMSGateWayBrandName")]
    SMSGateWayBrandName,
    [Display(Name = "ZaloAppID")] ZaloAppID,
    [Display(Name = "ZaloSecretKey")] ZaloSecretKey,

    [Display(Name = "ZaloOAAccessTokenUrl")]
    ZaloOAAccessTokenUrl,
    [Display(Name = "ZaloOAAccessToken")] ZaloOAAccessToken,
    [Display(Name = "ZaloOARefreshToken")] ZaloOARefreshToken,

    [Display(Name = "ZaloZNSTemplateSendUrl")]
    ZaloZNSTemplateSendUrl,

    [Display(Name = "ZaloZNSRatingGetUrl")]
    ZaloZNSRatingGetUrl,

    [Display(Name = "ZaloZNSMessageStatusUrl")]
    ZaloZNSMessageStatusUrl,

    [Display(Name = "OCRRecognitionToken")]
    OCRRecognitionToken,

    [Display(Name = "OCRIDCardRecognitionUrl")]
    OCRIDCardRecognitionUrl,

    [Display(Name = "OCRVehicleInspectionRecognitionUrl")]
    OCRVehicleInspectionRecognitionUrl,

    [Display(Name = "OCRVehicleRegistrationRecognitionUrl")]
    OCRVehicleRegistrationRecognitionUrl,

    EnvironmentName,
    FileManagerUrl,

    [Display(Name = "GoogleRecaptchaDomain")]
    GoogleRecaptchaDomain,
    [Display(Name = "ChartDomain")] ChartDomain,
    [Display(Name = "StoreId")] StoreId,
    AdminWebsiteIdConfig,
    [Display(Name = "Domain")] Domain,

    [Display(Name = "CdnDomainPublicNotCopy")]
    CdnDomainPublicNotCopy,
    [Display(Name = "CdnDomainPublic")] CdnDomainPublic,
    [Display(Name = "CdnArticleCopy")] CdnArticleCopy,

    [Display(Name = "CdnArticleCopyPublic")]
    CdnArticleCopyPublic,
    [Display(Name = "CdnDomainEmbed")] CdnDomainEmbed,
    [Display(Name = "CdnDomainKeyEmbed")] CdnDomainKeyEmbed,

    [Display(Name = "ArticleDatabaseSettings:ConnectionString")]
    ArticleDatabaseConnectionString,

    [Display(Name = "ArticleDatabaseSettings:DatabaseName")]
    ArticleDatabaseName,

    [Display(Name = "ArticleDatabaseSettings:HistoryCollectionName")]
    ArticleCollectionName,
    LocalUploadTokenKey,
    [Display(Name = "CdnUrlLocalUpload")] CdnUrlLocalUpload,
    [Display(Name = "CdnUrlImageCopy")] CdnUrlImageCopy,

    OldDomainImages,
    DownloadImageProxy,
    ArticleEditLink,
    EmbedDomain,
    ImageThumpHashKey,
    DomainIframeResponsives,
    CdnDomainPublicValidToken,
    EnableLazyLoad,
    [Display(Name = "NoImageUrl")] NoImageUrl,
    ExtensionGrammarNotAllow,
    NewsManagerUrl,
    ActionKeyPrefix,
    ArticleDealerId,
    DealerId,
    CarFamilyAttributeId,
    TermsArticleId,
    CarCategoryId,
    GoogleRecaptchaSecretKey,
    GoogleRecaptchaEnable,
    CmsMaxImageFileSize,
    CmsMaxVideoFileSize,
    CmsMaxDocumentFileSize,
    CmsMaxOtherFileSize,
    MaxAudioFileSize,
    UserMaxImageFileSize,
    UserMaxVideoFileSize,
    UserMaxDocumentFileSize,
    UserMaxOtherFileSize,
    MaxFileSizeReadByte,
    AllowedImageExtension,
    AllowedVideoExtension,
    AllowedAudioExtension,
    AllowedDocumentExtension,
    AllowedOtherExtension,
    AllowedFileInvoiceExtension,
    AccountManagerAutomaticKeyGeneration,
    GoogleTokenInfoUrl,

    GoogleAppId,
    GoogleAppSecret,
    GoogleLoginUrl,
    GoogleReturnUrl,
    GoogleTokenUrl,
    GoogleGetUserInfoUrl,

    MicrosoftAppId,
    MicrosoftAppSecret,
    MicrosoftLoginUrl,
    MicrosoftReturnUrl,
    MicrosoftTokenUrl,
    MicrosoftGetUserInfoUrl,

    AppleClientId,
    AppleKeyId,
    AppleTeamId,
    ApplePrivateKey,
    AppleLoginUrl,
    AppleReturnUrl,
    AppleTokenUrl,
    AppleGetUserInfoUrl,

    ZaloAppId,
    ZaloAppSecret,
    EmailManagerUrl,
    ZaloLoginUrl,
    ZaloReturnUrl,
    ZaloTokenUrl,
    ZaloGetUserInfoUrl,
    ZaloOAUserDetailUrl,
    ZaloDefaultOAId,
    ZaloDefaultOTPTemplateId,

    MicrosoftTokenInfoUrl,
    ZaloTokenInfoUrl,
    TrackingProduct,
    ProductDealerId,
    EnableLogByCurrentUser,
    ProvinceIdDefault,
    IsFileUploadCheckCaptcha,
    UploadRootPath,
    UploadPath,
    UploadPublishPath,
    FileUploadUrl,
    MaxFileNumberUploadByUser,
    MaxSizeUploadByUser,
    IsMasterData,
    DealerTMVId,
    PathOldSale,
    OtoCategoryId,
    ServiceCategoryId,
    SparePartsCategoryId,// Phụ tùng
    SuppliesCategoryId,// Vật tư
    AccessoriesCategoryId,// Phụ kiện
    OilCategoryId,// Dầu
    ChemicalsCategoryId,// Hóa chất
    ToolsCategoryId,// Dụng cụ
    TiresCategoryId,// Lốp
    YearOfCarObjectId,
    VehicleOriginObjectId,
    DefaultMyCarImage,
    ImportedCarAttributeValueId,
    RefreshTokenExpiresTime,
    UseSwagger,
    SwaggerUsername,
    SwaggerPassword,

    [Display(Name = "LocaleStringResourceDatabaseSettings:ConnectionString")]
    LocaleStringResourceDatabaseConnectionString,

    [Display(Name = "LocaleStringResourceDatabaseSettings:DatabaseName")]
    LocaleStringResourceDatabaseName,
    AccountLogoutUrl,
    ExceptionUrl,
    BypassEmbedToken,
    IsWeb,
    ZaloVerifier,
    TMSSOldSystemQueueName,
    TMSSOldSaleQueueName,
    RabbitMqExChangeEntityChangeEvent,
    RabbitMqRoutingEntityChangeEvent,
    ETestApiUrl,
    ETestApiKey,
    ETestSsiSurveyId,
    ETestCsiSurveyId,
    ETestUserName,
    ProductLoad,

    [Display(Name = "SurveyMongoDatabaseSettings:ConnectionString")]
    SurveyMongoDatabaseConnectionString,

    [Display(Name = "SurveyMongoDatabaseSettings:DatabaseName")]
    SurveyMongoDatabaseName,

    [Display(Name = "SurveyMongoDatabaseSettings:ETestLogCollectionName")]
    ETestMongoLogCollectionName,
    TranslateEnable,
    LanguageDefaultId,
    ApplicationGroup,
    WebsiteIdDefault,
    CMSPageRenderUrl,
    NewsPreviewUrl,
    TicketSyncTmssUserId,
    TicketSyncTmssSourceId,
    ReminderRun
}
