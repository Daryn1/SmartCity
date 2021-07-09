using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.Configuration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.SpaServices;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SmartCity.Common.Enums;
using SmartCity.Data;
using SmartCity.Data.Entities;
using SmartCity.Data.Entities.Bus;
using SmartCity.Data.Entities.Medicine;
using SmartCity.Data.Entities.Police;
using SmartCity.Data.Entities.UserAccount;
using SmartCity.Data.Interfaces;
using SmartCity.Data.Repositories;
using SmartCity.Services.Interfaces;
using SmartCity.Services.Services;
using SmartCity.Web.Hubs;
using SmartCity.Web.Infrastructure;
using SmartCity.Web.Models.Account;
using SmartCity.Web.Models.Bus;
using SmartCity.Web.Models.Certificates;
using SmartCity.Web.Models.Friends;
using SmartCity.Web.Models.Friendships;
using SmartCity.Web.Models.HDDoctor;
using SmartCity.Web.Models.HDManager;
using SmartCity.Web.Models.HDUser;
using SmartCity.Web.Models.HealthDepartment;
using SmartCity.Web.Models.Messenger;
using SmartCity.Web.Models.Police;
using SmartCity.Web.Models.Police.Violation;
using SmartCity.Web.Models.Roles;
using SmartCity.Web.Models.Transactions;
using SmartCity.Web.Models.Users;
using SmartCity.Web.Models.UserTasks;
using ICertificateService = SmartCity.Web.Infrastructure.ICertificateService;
using IConfiguration = Microsoft.Extensions.Configuration.IConfiguration;

namespace SmartCity.Web
{
    public class Startup
    {
        public const string AuthMethod = "CookieAuth";
        public const string PoliceAuthMethod = "PoliceAuth";
        public const string MedicineAuth = "CookieMedicineAuth";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRouting();

            services.AddDbContext<SmartCityDbContext>(option => option.UseSqlServer(Configuration.GetConnectionString("SmartCity")));

            services.AddAuthentication(AuthMethod)
                .AddCookie(AuthMethod, config =>
                {
                    config.Cookie.Name = "User.Auth";
                    config.LoginPath = "/Account/Login";
                    config.AccessDeniedPath = "/Account/AccessDenied";
                })
                .AddCookie(PoliceAuthMethod, config =>
                {
                    config.Cookie.Name = "PUser";
                    config.LoginPath = "/Police/Login";
                });

            services.AddAuthentication(AuthMethod)
                .AddCookie(MedicineAuth, config =>
                {
                    config.Cookie.Name = "Med.Auth";
                    config.LoginPath = "/HealthDepartment/Login";
                    config.AccessDeniedPath = "/HealthDepartment/AccessDenied";
                });

            services.AddTransient<IAuthorizationHandler, RestrictAccessToBlockedUsersHandler>(s =>
                new RestrictAccessToBlockedUsersHandler(s.GetService<ICitizenUserRepository>()));

            services.AddTransient<IAuthorizationHandler, RestrictAccessToDeadUsersHandler>(s =>
                new RestrictAccessToDeadUsersHandler(s.GetService<ICitizenUserRepository>()));

            services.AddAuthorization(options =>
            {
                options.AddPolicy("Admins", policy =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.RequireRole("Admin");
                    policy.Requirements.Add(new RestrictAccessToBlockedUsersRequirement());
                    policy.Requirements.Add(new RestrictAccessToDeadUsersRequirement());
                });
            });

            RegistrationMapper(services);

            //RegistrationRepository(services);

            // Add repositories
            services.AddScoped<ICitizenUserRepository, CitizenUserRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IAdressRepository, AdressRepository>();
            services.AddScoped<ICertificateRepository, CertificateRepository>();
            services.AddScoped<IFriendshipRepository, FriendshipRepository>();
            services.AddScoped<IMessageRepository, MessageRepository>();
            services.AddScoped<ITransactionRepository, TransactionRepository>();
            services.AddScoped<IUserTaskRepository, UserTaskRepository>();
            services.AddScoped<IBusRepository, BusRepository>();
            services.AddScoped<IBusRouteRepository, BusRouteRepository>();
            services.AddScoped<IMedicalInsuranceRepository, MedicalInsuranceRepository>();
            services.AddScoped<IMedicineCertificateRepository, MedicineCertificateRepository>();
            services.AddScoped<IPolicemanRepository, PolicemanRepository>();
            services.AddScoped<IReceptionOfPatientsRepository, ReceptionOfPatientsRepository>();
            services.AddScoped<IRecordFormRepository, RecordFormRepository>();
            services.AddScoped<IViolationRepository, ViolationRepository>();

            // Add services
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IFriendshipService, FriendshipService>();
            services.AddScoped<ICertificateService, CertificateService>();
            services.AddScoped<IMessengerService, MessengerService>();
            services.AddScoped<ITaskService, TaskService>();
            services.AddScoped<ITransactionService, TransactionService>();

            services.AddHttpContextAccessor();

            services.AddControllersWithViews().AddJsonOptions(opt =>
            {
                opt.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
                opt.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });

            services.AddScoped<CertificateService>();
            services.AddSignalR();

            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });
        }

        private void RegistrationMapper(IServiceCollection services)
        {
            var configurationExpression = new MapperConfigurationExpression();

            configurationExpression.CreateMap<CitizenUser, MyProfileViewModel>().ForMember(dest => dest.FriendRequests,
                opt => opt.MapFrom(src =>
                    src.ReceivedFriendRequests.Where(friendship => friendship.FriendshipStatus == FriendshipStatus.Pending)));

            configurationExpression.CreateMap<CitizenUser, FriendViewModel>();
            configurationExpression.CreateMap<Friendship, FriendRequestViewModel>();
            configurationExpression.CreateMap<CitizenUser, ProfileViewModel>();

            configurationExpression.CreateMap<Friendship, FriendshipViewModel>()
                .ForMember(dest => dest.RequesterLogin, opt => opt.MapFrom(src => src.Requester.Login))
                .ForMember(dest => dest.RequestedLogin, opt => opt.MapFrom(src => src.Requested.Login));

            configurationExpression.CreateMap<FriendshipViewModel, Friendship>();

            configurationExpression.CreateMap<CitizenUser, FoundUserViewModel>();

            configurationExpression.CreateMap<UserTask, UserTaskViewModel>()
                .ForMember(dest => dest.OwnerLogin, opt => opt.MapFrom(src => src.Owner.Login));

            configurationExpression.CreateMap<UserTaskViewModel, UserTask>();

            configurationExpression.CreateMap<Certificate, CertificateViewModel>()
                .ForMember(dest => dest.OwnerLogin, opt => opt.MapFrom(src => src.Owner.Login));

            configurationExpression.CreateMap<CertificateViewModel, Certificate>();

            configurationExpression.CreateMap<Role, RoleViewModel>()
                .ForMember(dest => dest.UserLogins, opt => opt.MapFrom(src => src.Users.Select(t => t.Login)));

            configurationExpression.CreateMap<RoleViewModel, Role>();

            configurationExpression.CreateMap<Transaction, TransactionViewModel>()
                .ForMember(dest => dest.SenderLogin, opt => opt.MapFrom(src => src.Sender.Login))
                .ForMember(dest => dest.RecipientLogin, opt => opt.MapFrom(src => src.Recipient.Login));

            configurationExpression.CreateMap<TransactionViewModel, Transaction>();

            configurationExpression.CreateMap<Message, MessageViewModel>()
                .ForMember(dest => dest.SenderLogin, opt => opt.MapFrom(src => src.Sender.Login))
                .ForMember(dest => dest.RecipientLogin, opt => opt.MapFrom(src => src.Recipient.Login))
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Date.ToString("HH:mm, dd MMM")));

            configurationExpression.CreateMap<TransactionViewModel, Transaction>();

            configurationExpression.CreateMap<CitizenUser, RegistrationViewModel>();
            configurationExpression.CreateMap<RegistrationViewModel, CitizenUser>();

            configurationExpression.CreateMap<Adress, AdressViewModel>()
                .ForMember(dest => dest.OwnerLogin, opt => opt.MapFrom(src => src.Owner.Login));

            configurationExpression.CreateMap<AdressViewModel, Adress>();

            configurationExpression.CreateMap<HealthDepartment, HealthDepartmentViewModel>();
            configurationExpression.CreateMap<HealthDepartmentViewModel, HealthDepartment>();

            configurationExpression.CreateMap<Bus, BusViewModel>();
            configurationExpression.CreateMap<BusViewModel, Bus>();

            configurationExpression.CreateMap<BusRoute, CreateBusRouteViewModel>();
            configurationExpression.CreateMap<CreateBusRouteViewModel, BusRoute>();

            configurationExpression.CreateMap<BusWorker, ManageBusWorkerViewModel>();
            configurationExpression.CreateMap<ManageBusWorkerViewModel, BusWorker>();

            configurationExpression.CreateMap<BusOrder, BusOrderViewModel>();
            configurationExpression.CreateMap<BusOrderViewModel, BusOrder>();

            configurationExpression.CreateMap<BusRouteTime, BusRouteTimeViewModel>();
            configurationExpression.CreateMap<BusRouteTimeViewModel, BusRouteTime>();

            configurationExpression.CreateMap<Bus, BusOrderViewModel>();
            configurationExpression.CreateMap<BusOrderViewModel, Bus>();

            configurationExpression.CreateMap<RecordForm, RecordFormViewModel>();
            configurationExpression.CreateMap<RecordFormViewModel, RecordForm>();

            configurationExpression.CreateMap<RecordForm, ListRecordFormViewModel>();
            configurationExpression.CreateMap<ListRecordFormViewModel, RecordForm>();


            configurationExpression.CreateMap<Policeman, PolicemanViewModel>()
                .ForMember(dest => dest.ProfileVM, opt => opt.MapFrom(p => p.User));

            configurationExpression.CreateMap<Violation, ViolationItemViewModel>()
                .ForMember(dest => dest.BlamedUserName, opt => opt.MapFrom(v => v.BlamedUser.FirstName + " " + v.BlamedUser.LastName))
                .ForMember(dest => dest.PolicemanName, opt => opt.MapFrom(v => v.ViewingPoliceman.User.FirstName + " " + v.ViewingPoliceman.User.LastName));

            configurationExpression.CreateMap<ViolationDeclarationViewModel, Violation>().ReverseMap();

            configurationExpression.CreateMap<CitizenUser, FoundUsersViewModel>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(u => u.FirstName + " " + u.LastName));

            configurationExpression.CreateMap<CitizenUser, UserVerificationViewModel>();

            configurationExpression.CreateMap<Violation, CriminalItemViewModel>()
                .ForMember(dest => dest.BlamingUserName, opt => opt.MapFrom(v => v.BlamingUser.FirstName + " " + v.BlamingUser.LastName))
                .ForMember(dest => dest.BlamedUserName, opt => opt.MapFrom(v => v.BlamedUser.FirstName + " " + v.BlamedUser.LastName))
                .ForMember(dest => dest.ViewingPolicemanName, opt => opt.MapFrom(v => v.ViewingPoliceman.User.FirstName + " " + v.ViewingPoliceman.User.LastName))
                .ForMember(dest => dest.PolicemanLogin, opt => opt.MapFrom(v => v.ViewingPoliceman.User.Login))
                .ForMember(dest => dest.BlamedUserAvatar, opt => opt.MapFrom(v => v.BlamedUser.AvatarUrl));

            configurationExpression.CreateMap<MedicalInsurance, MedicalInsuranceViewModel>();
            configurationExpression.CreateMap<MedicalInsuranceViewModel, MedicalInsurance>();

            configurationExpression.CreateMap<CitizenUser, ForDHLoginViewModel>();
            configurationExpression.CreateMap<ForDHLoginViewModel, CitizenUser>();

            configurationExpression.CreateMap<MedicalInsurance, UserPageViewModel>();
            configurationExpression.CreateMap<UserPageViewModel, MedicalInsurance>();

            configurationExpression.CreateMap<CitizenUser, UserPageViewModel>();
            configurationExpression.CreateMap<UserPageViewModel, CitizenUser>();

            configurationExpression.CreateMap<CitizenUser, DoctorPageViewModel>();
            configurationExpression.CreateMap<DoctorPageViewModel, CitizenUser>();

            configurationExpression.CreateMap<MedicineCertificate, MedicineCertificateViewModel>();
            configurationExpression.CreateMap<MedicineCertificateViewModel, MedicineCertificate>();

            configurationExpression.CreateMap<ReceptionOfPatients, ReceptionOfPatientsViewModel>();
            configurationExpression.CreateMap<ReceptionOfPatientsViewModel, ReceptionOfPatients>();

            configurationExpression.CreateMap<ReceptionOfPatients, UserPageViewModel>();
            configurationExpression.CreateMap<UserPageViewModel, ReceptionOfPatients>();

            var mapperConfiguration = new MapperConfiguration(configurationExpression);
            var mapper = new Mapper(mapperConfiguration);
            services.AddScoped<IMapper>(s => mapper);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseDeveloperExceptionPage();
            // if (env.IsDevelopment())
            // {
            //     
            // }
            // else
            // {
            //     app.UseExceptionHandler("/Home/Error");
            //     // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            //     app.UseHsts();
            // }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            var cultureInfo = new CultureInfo("en-US");
            CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
            CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

            app.UseRouting();

            //  то ты?
            app.UseAuthentication();

            //  уда у теб€ есть доступ?
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapHub<ChatHub>("/chathub");
            });

            /*app.Map("/shop", app1 =>
            {
                app1.UseSpa(spa =>
                {
                    // To learn more about options for serving an Angular SPA from ASP.NET Core,
                    // see https://go.microsoft.com/fwlink/?linkid=864501

                    spa.Options.SourcePath = "ClientApp";
                    spa.Options.StartupTimeout = TimeSpan.FromSeconds(180);

                    if (env.IsDevelopment())
                    {
                        spa.UseAngularCliServer(npmScript: "start");
                    }
                });
            });*/

            app.UseSpa(spa =>
            {
                // To learn more about options for serving an Angular SPA from ASP.NET Core,
                // see https://go.microsoft.com/fwlink/?linkid=864501

                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseAngularCliServer(npmScript: "start");
                }
            });
        }
    }
}
