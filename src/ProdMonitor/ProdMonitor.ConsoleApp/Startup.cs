using ProdMonitor.Domain.Interfaces.Services;
using ProdMonitor.Domain.Models.Enums;
using ProdMonitor.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProdMonitor.Domain.Exceptions;
using System.Xml.Linq;
using ProdMonitor.DataAccess.Models.Enums;
using System.Globalization;
using Microsoft.Extensions.DependencyInjection;
using ProdMonitor.DataAccess.Models;
using ProdMonitor.Application.Services;

namespace ProdMonitor.ConsoleApp
{
    public class Startup
    {
        private readonly IAssemblyLineService _assemblyLineService;
        private readonly IAuthenticationService _authenticationService;
        private readonly IDetailOrderService _detailOrderService;
        private readonly IDetailService _detailService;
        private readonly IServiceReportService _serviceReportService;
        private readonly IServiceRequestService _serviceRequestService;
        private readonly ITractorService _tractorService;
        private readonly IUserService _userService;
        private User? _currentUser;


        public Startup(IServiceReportService serviceReportService,
                   IServiceRequestService serviceRequestService,
                   IUserService userService,
                   IAssemblyLineService assemblyLineService,
                   ITractorService tractorService,
                   IDetailService detailService,
                   IDetailOrderService orderService,
                   IAuthenticationService authenticationService)
        {
            _serviceReportService = serviceReportService;
            _serviceRequestService = serviceRequestService;
            _userService = userService;
            _assemblyLineService = assemblyLineService;
            _tractorService = tractorService;
            _detailService = detailService;
            _detailOrderService = orderService;
            _authenticationService = authenticationService;
        }

        public async Task Run()
        {
            while (true)
            {
                if (_currentUser == null)
                {
                    Console.WriteLine("Select an option:");
                    Console.WriteLine("1. Login");
                    Console.WriteLine("2. Register");
                    Console.WriteLine("0. Exit");

                    var choice = Console.ReadLine();

                    switch (choice)
                    {
                        case "1":
                            await Login();
                            break;
                        case "2":
                            await Register();
                            break;
                        case "0":
                            return;
                        default:
                            Console.WriteLine("Invalid option. Please try again.");
                            break;
                    }
                }

                else if (_currentUser.Role == RoleType.Verification)
                {
                    Console.WriteLine("Your acc on verification now, wait, please");
                    Console.WriteLine("0. Exit");

                    var choice = Console.ReadLine();
                    switch (choice)
                    {
                        case "0":
                            Logout();
                            break;
                        default:
                            Console.WriteLine("Invalid option. Please try again.");
                            break;
                    }
                }

                else if (_currentUser.Role == RoleType.Admin)
                {
                    Console.WriteLine("Your role now is admin");
                    Console.WriteLine("Select an option:");
                    Console.WriteLine("1.  Изменить права пользователя");
                    Console.WriteLine("2.  Посмотреть список пользователей");
                    Console.WriteLine("3.  Добавить трактор");
                    Console.WriteLine("4.  Добавить производственную линию");
                    Console.WriteLine("5.  Добавить деталь");
                    Console.WriteLine("6.  Посмотреть список деталей");
                    Console.WriteLine("7.  Посмотреть информацию о тракторе");
                    Console.WriteLine("8.  Посмотреть список тракторов");
                    Console.WriteLine("9.  Посмотреть информацию о производственной линии");
                    Console.WriteLine("10. Посмотреть список производственных линий");
                    Console.WriteLine("11. Посмотреть журнал обслуживания");
                    Console.WriteLine("0.  Выход");

                    var choice = Console.ReadLine();

                    switch (choice)
                    {
                        case "1":
                            await ChangeUserRole();
                            break;
                        case "2":
                            await GetUsers();
                            break;
                        case "3":
                            await AddTractor();
                            break;
                        case "4":
                            await AddLine();
                            break;
                        case "5":
                            await AddDetail();
                            break;
                        case "6":
                            await GetDetails();
                            break;
                        case "7":
                            await GetTractor();
                            break;
                        case "8":
                            await GetTractors();
                            break;
                        case "9":
                            await GetLine();
                            break;
                        case "10":
                            await GetLines();
                            break;
                        case "11":
                            await GetReports();
                            break;
                        case "0":
                            Logout();
                            break;
                        default:
                            Console.WriteLine("Invalid option. Please try again.");
                            break;
                    }
                }

                else if (_currentUser.Role == RoleType.Operator)
                {
                    Console.WriteLine("Your role now is operator");
                    Console.WriteLine("Select an option:");
                    Console.WriteLine("1.  Посмотреть список тракторов");
                    Console.WriteLine("2.  Посмотреть информацию о тракторе");
                    Console.WriteLine("3.  Посмотреть список производственных линий");
                    Console.WriteLine("4.  Посмотреть информацию о производственной линии");
                    Console.WriteLine("5.  Посмотреть журнал обслуживания");
                    Console.WriteLine("6.  Создать заявку на обслуживание");
                    Console.WriteLine("0.  Выход");

                    var choice = Console.ReadLine();

                    switch (choice)
                    {
                        case "1":
                            await GetTractors();
                            break;
                        case "2":
                            await GetTractor();
                            break;
                        case "3":
                            await GetLines();
                            break;
                        case "4":
                            await GetLine();
                            break;
                        case "5":
                            await GetReports();
                            break;
                        case "6":
                            await CreateServiceRequest();
                            break;
                        case "0":
                            Logout();
                            break;
                        default:
                            Console.WriteLine("Invalid option. Please try again.");
                            break;
                    }
                }

                else if (_currentUser.Role == RoleType.Specialist)
                {
                    Console.WriteLine("Your role now is specialist");
                    Console.WriteLine("Select an option:");
                    Console.WriteLine("1.  Посмотреть список тракторов");
                    Console.WriteLine("2.  Посмотреть информацию о тракторе");
                    Console.WriteLine("3.  Посмотреть список производственных линий");
                    Console.WriteLine("4.  Посмотреть информацию о производственной линии");
                    Console.WriteLine("5.  Посмотреть журнал обслуживания");
                    Console.WriteLine("6.  Создать отчет об обслуживании");
                    Console.WriteLine("7.  Закрыть отчет об обслуживании");
                    Console.WriteLine("8.  Посмотреть список запчастей");
                    Console.WriteLine("9.  Создать заказ деталей");
                    Console.WriteLine("0.  Выход");

                    var choice = Console.ReadLine();

                    switch (choice)
                    {
                        case "1":
                            await GetTractors();
                            break;
                        case "2":
                            await GetTractor();
                            break;
                        case "3":
                            await GetLines();
                            break;
                        case "4":
                            await GetLine();
                            break;
                        case "5":
                            await GetReports();
                            break;
                        case "6":
                            await CreateServiceReport();
                            break;
                        case "7":
                            await CloseServiceReport();
                            break;
                        case "8":
                            await GetDetails();
                            break;
                        case "9":
                            await CreateOrder();
                            break;
                        case "0":
                            Logout();
                            break;
                        default:
                            Console.WriteLine("Invalid option. Please try again.");
                            break;
                    }
                }

            }
        }

        private void Logout()
        {
            _currentUser = null;
        }

        private async Task CreateOrder()
        {
            try
            {
                var orderDetails = new List<OrderDetailData>();
                var userId = _currentUser.Id;

                while (true)
                {
                    Console.WriteLine("Введите идентификатор детали (или '-' для завершения ввода деталей):");
                    var detailIdInput = Console.ReadLine();

                    if (detailIdInput == "-")
                    {
                        break;
                    }

                    if (!Guid.TryParse(detailIdInput, out Guid detailId))
                    {
                        Console.WriteLine("Некорректный идентификатор детали.");
                        continue;
                    }

                    Console.WriteLine("Введите количество деталей:");
                    var amountInput = Console.ReadLine();

                    if (!int.TryParse(amountInput, out int amount) || amount <= 0)
                    {
                        Console.WriteLine("Некорректное количество деталей.");
                        continue;
                    }

                    orderDetails.Add(new OrderDetailData(detailId, amount));
                }

                if (!orderDetails.Any())
                {
                    Console.WriteLine("Заказ должен содержать хотя бы одну деталь.");
                    return;
                }

                var order = new DetailOrderCreate(userId, orderDetails);
                var createdOrder = await _detailOrderService.CreateDetailOrderAsync(order);

                Console.WriteLine("Заказ успешно создан:");
                Console.WriteLine($"Идентификатор заказа: {createdOrder.Id}");
                Console.WriteLine($"Общая стоимость заказа: {createdOrder.TotalPrice}");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Ошибка при создании заказа: {ex.Message}");
            }
            catch (DetailNotFoundException ex)
            {
                Console.WriteLine($"Ошибка при создании заказа: {ex.Message}");
            }
            catch (DetailOrderException ex)
            {
                Console.WriteLine($"Ошибка при создании заказа: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при создании заказа: {ex.Message}");
            }
        }

        private async Task CreateServiceRequest()
        {
            Console.WriteLine("Введите id линии: ");
            var lineId = Guid.Parse(Console.ReadLine());

            var userId = _currentUser.Id;

            Console.WriteLine("Введите тип заявки (Inspection/Repair): ");
            var typeInput = Console.ReadLine();
            var requestType = Enum.Parse<RequestType>(typeInput);

            Console.WriteLine("Введите описание заявки: ");
            var description = Console.ReadLine();

            var newRequest = new ServiceRequestCreate(lineId, userId, requestType, description);

            try
            {
                var request = await _serviceRequestService.CreateServiceRequestAsync(newRequest);
                Console.WriteLine($"Заявка с id {request.Id} успешно добавлена.");
            }
            catch (RequestServiceException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        private async Task GetReports()
        {
            try
            {
                var filter = GetReportsFilter();
                var reports = await _serviceReportService.GetAllServiceReportsAsync(filter);

                if (reports.Count == 0)
                {
                    Console.WriteLine("Отчетов об обслуживании пока нет");
                    return;
                }

                Console.WriteLine("==============================================================================");
                foreach (var report in reports)
                {
                    Console.WriteLine($"ID: {report.Id}");
                    Console.WriteLine($"lineID: {report.LineId}");
                    Console.WriteLine($"userID: {report.UserId}");
                    Console.WriteLine($"requestID: {report.RequestId}");
                    Console.WriteLine($"openDate: {report.OpenDate}");
                    Console.WriteLine($"closeDate: {report.CloseDate}");
                    Console.WriteLine($"price: {report.Price}");
                    Console.WriteLine($"description: {report.Description}");
                    Console.WriteLine("--------------------------------------------------------------");
                }

            }
            catch (ReportServiceException ex)
            {
                Console.WriteLine(ex.Message);
            }
            
        }

        private ServiceReportFilter GetReportsFilter()
        {
            Console.WriteLine("Пожалуйста, заполните необходимые поля фильтра (если хотите пропустить поле, введите \"-\")");
            
            Console.WriteLine("Введите id линии: ");
            var lineIdInput = Console.ReadLine();
            Guid? lineId = lineIdInput == "-" ? null : Guid.Parse(lineIdInput);
            
            Console.WriteLine("Введите id пользователя: ");
            var userIdInput = Console.ReadLine();
            Guid? userId = userIdInput == "-" ? null : Guid.Parse(userIdInput);

            Console.WriteLine("Введите id запроса: ");
            var requestIdInput = Console.ReadLine();
            Guid? requestId = requestIdInput == "-" ? null : Guid.Parse(requestIdInput);

            Console.WriteLine("Нужно ли сортировать по дате (да/нет)");
            var sortByDateInput = Console.ReadLine();
            bool? sortByDate = sortByDateInput == "-" ? null : sortByDateInput.Equals("да", StringComparison.OrdinalIgnoreCase);

            Console.WriteLine("Пропустить N записей (по умолчанию 0): ");
            var skipInput = Console.ReadLine();
            int skip = skipInput == "-" || string.IsNullOrWhiteSpace(skipInput) ? 0 : int.Parse(skipInput);

            Console.WriteLine("Максимальное количество записей (по умолчанию максимальное значение): ");
            var limitInput = Console.ReadLine();
            int limit = limitInput == "-" || string.IsNullOrWhiteSpace(limitInput) ? int.MaxValue : int.Parse(limitInput);

            var filter = new ServiceReportFilter(lineId, userId, requestId, sortByDate, skip, limit);
            return filter;
        }


        private async Task GetLines()
        {
            try
            {
                var filter = GetLinesFilter();
                var lines = await _assemblyLineService.GetAllAssemblyLinesAsync(filter);

                if (lines.Count == 0)
                {
                    Console.WriteLine("Производственных линий пока нет");
                    return;
                }

                Console.WriteLine("==============================================================================");
                foreach (var line in lines)
                {
                    Console.WriteLine($"ID: {line.Id}");
                    Console.WriteLine($"Name: {line.Name}");
                    Console.WriteLine($"Status: {line.Status}");
                    Console.WriteLine("--------------------------------------------------------------");
                }
            }
            catch (AssemblyLineServiceException ex)
            {
                Console.WriteLine(ex.Message);
            }
            

        }

        private AssemblyLineFilter GetLinesFilter()
        {
            Console.WriteLine("Пожалуйста, заполните необходимые поля фильтра (если хотите пропустить поле, введите \"-\")");

            Console.WriteLine("Введите статус линии: ");
            var statusInput = Console.ReadLine();
            LineStatusType? status = null;

            if (statusInput != "-")
            {
                if (Enum.TryParse<LineStatusType>(statusInput, out var parsedStatus))
                {
                    status = parsedStatus;
                }
                else
                {
                    Console.WriteLine("Некорректный ввод статуса линии. Попробуйте снова.");
                    return GetLinesFilter();
                }
            }

            Console.WriteLine("Пропустить N записей (по умолчанию 0): ");
            var skipInput = Console.ReadLine();
            int skip = skipInput == "-" || string.IsNullOrWhiteSpace(skipInput) ? 0 : int.Parse(skipInput);

            Console.WriteLine("Максимальное количество записей (по умолчанию максимальное значение): ");
            var limitInput = Console.ReadLine();
            int limit = limitInput == "-" || string.IsNullOrWhiteSpace(limitInput) ? int.MaxValue : int.Parse(limitInput);

            var filter = new AssemblyLineFilter(status, skip, limit);
            return filter;
        }

        private async Task GetLine()
        {
            Console.WriteLine("Введите идентификатор линии: ");
            var lineIdInput = Console.ReadLine();

            if (Guid.TryParse(lineIdInput, out Guid lineId))
            {
                try
                {
                    var line = await _assemblyLineService.GetAssemblyLineByIdAsync(lineId);

                    if (line != null)
                    {
                        Console.WriteLine($"ID: {line.Id},\n Линия: {line.Name},\n Статус: {line.Status},\n Время простоя: {line.DownTime} часов");
                    }
                    else
                    {
                        Console.WriteLine("Линия не найдена.");
                    }
                }
                catch (LineNotFoundException ex)
                {
                    Console.WriteLine($"Ошибка при получении информации о линии: {ex.Message}");
                }
                catch (AssemblyLineServiceException ex)
                {
                    Console.WriteLine($"Ошибка при получении информации о линии: {ex.Message}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка при получении информации о линии: {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine("Некорректный идентификатор линии. Попробуйте снова.");
            }
        }

        private async Task GetTractors()
        {
            try
            {
                var filter = GetTractorsFilter();
                var tractors = await _tractorService.GetAllTractorsAsync(filter);

                if (tractors.Count == 0)
                {
                    Console.WriteLine("Тракторов пока нет");
                    return;
                }

                Console.WriteLine("==============================================================================");
                foreach (var tractor in tractors)
                {
                    Console.WriteLine($"ID: {tractor.Id}");
                    Console.WriteLine($"Model: {tractor.Model}");
                    Console.WriteLine($"Release year: {tractor.ReleaseYear}");
                    Console.WriteLine($"Engine type: {tractor.EngineType}");
                    Console.WriteLine($"EcologicalStandart: {tractor.EcologicalStandart}");
                }
            }
            catch (TractorServiceException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private TractorFilter GetTractorsFilter()
        {
            Console.WriteLine("Пожалуйста, заполните необходимые поля фильтра (если хотите пропустить поле, введите \"-\")");


            Console.WriteLine("Введите год выпуска трактора: ");
            var releaseYearInput = Console.ReadLine();
            int? releaseYear = releaseYearInput == "-" ? null : int.Parse(releaseYearInput);

            Console.WriteLine("Введите тип двигателя: ");
            var engineType = Console.ReadLine();
            engineType = engineType == "-" ? null : engineType;

            Console.WriteLine("Введите экологический стандарт: ");
            var ecologicalStandart = Console.ReadLine();
            ecologicalStandart = ecologicalStandart == "-" ? null : ecologicalStandart;

            Console.WriteLine("Пропустить N записей (по умолчанию 0): ");
            var skipInput = Console.ReadLine();
            int skip = skipInput == "-" || string.IsNullOrWhiteSpace(skipInput) ? 0 : int.Parse(skipInput);

            Console.WriteLine("Максимальное количество записей (по умолчанию максимальное значение): ");
            var limitInput = Console.ReadLine();
            int limit = limitInput == "-" || string.IsNullOrWhiteSpace(limitInput) ? int.MaxValue : int.Parse(limitInput);

            var filter = new TractorFilter(releaseYear, engineType, ecologicalStandart, skip, limit);
            return filter;
        }

        private async Task GetTractor()
        {
            Console.WriteLine("Введите идентификатор трактора: ");
            var tractorIdInput = Console.ReadLine();

            if (Guid.TryParse(tractorIdInput, out Guid tractorId))
            {
                try
                {
                    var tractor = await _tractorService.GetTractorByIdAsync(tractorId);
                    if (tractor != null)
                    {
                        Console.WriteLine($"ID: {tractor.Id}");
                        Console.WriteLine($"Model: {tractor.Model}");
                        Console.WriteLine($"Release year: {tractor.ReleaseYear}");
                        Console.WriteLine($"Engine type: {tractor.EngineType}");
                        Console.WriteLine($"EcologicalStandart: {tractor.EcologicalStandart}");
                    }
                    else
                    {
                        Console.WriteLine("Трактор не найден.");
                    }
                }
                catch (TractorNotFoundException ex)
                {
                    Console.WriteLine($"Ошибка при получении информации о тракторе: {ex.Message}");
                }
                catch (TractorServiceException ex)
                {
                    Console.WriteLine($"Ошибка при получении информации о тракторе: {ex.Message}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка при получении информации о тракторе: {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine("Не корректный uuid");
            }
        }

        private async Task GetDetails()
        {
            try
            {
                var filter = GetDetailsFilter();
                var details = await _detailService.GetAllDetailsAsync(filter);

                if (details.Count == 0)
                {
                    Console.WriteLine("Деталей пока нет");
                    return;
                }

                Console.WriteLine("==============================================================================");
                foreach (var detail in details)
                {
                    Console.WriteLine($"ID: {detail.Id}");
                    Console.WriteLine($"Name: {detail.Name}");
                    Console.WriteLine($"Country: {detail.Country}");
                    Console.WriteLine($"Amount: {detail.Amount}");
                    Console.WriteLine($"Price: {detail.Price}");
                    Console.WriteLine("--------------------------------------------------------------");
                }
            }
            catch (DetailServiceException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private DetailFilter GetDetailsFilter()
        {
            Console.WriteLine("Пожалуйста, заполните необходимые поля фильтра (если хотите пропустить поле, введите \"-\")");

            Console.WriteLine("Введите страну производства: ");
            var country = Console.ReadLine();
            country = country == "-" ? null : country;

            Console.WriteLine("Пропустить N записей (по умолчанию 0): ");
            var skipInput = Console.ReadLine();
            int skip = skipInput == "-" || string.IsNullOrWhiteSpace(skipInput) ? 0 : int.Parse(skipInput);

            Console.WriteLine("Максимальное количество записей (по умолчанию максимальное значение): ");
            var limitInput = Console.ReadLine();
            int limit = limitInput == "-" || string.IsNullOrWhiteSpace(limitInput) ? int.MaxValue : int.Parse(limitInput);

            var filter = new DetailFilter(country, skip, limit);
            return filter;
        }

        private async Task AddDetail()
        {

            Console.WriteLine("Введите название детали:");
            var name = Console.ReadLine();

            Console.WriteLine("Введите страну изготовителя: ");
            var country = Console.ReadLine();

            Console.WriteLine("Введите количество: ");
            var amount = int.Parse(Console.ReadLine());

            Console.WriteLine("Введите цену детали: ");
            var price = float.Parse(Console.ReadLine());

            Console.WriteLine("Введиет длину: ");
            var length = int.Parse(Console.ReadLine());

            Console.WriteLine("Введите высоту: ");
            var height = int.Parse(Console.ReadLine());

            Console.WriteLine("Введите ширину: ");
            var width = int.Parse(Console.ReadLine());

            var newDetail = new DetailCreate(name, country, amount, price, length, height, width);

            try
            {
                var detail = await _detailService.CreateDetailAsync(newDetail);
                Console.WriteLine($"Деталь с id {detail.Id} успешно добавлена.");
            }
            catch (DetailServiceException ex)
            {
                Console.WriteLine($"Ошибка при добавлении детали: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при добавлении детали: {ex.Message}");
            }
        }

        private async Task AddLine()
        {
            Console.WriteLine("Введите имя: ");
            var name = Console.ReadLine();

            Console.WriteLine("Введите длину: ");
            var length = float.Parse(Console.ReadLine());

            Console.WriteLine("Введите высоту: ");
            var height = float.Parse(Console.ReadLine());

            Console.WriteLine("Введите ширину: ");
            var width = float.Parse(Console.ReadLine());

            Console.WriteLine("Введите статус линии: ");
            var statusInput = Console.ReadLine();
            var status = Enum.Parse<LineStatusType>(statusInput);

            Console.WriteLine("Введите время простоя: ");
            var downTime = int.Parse(Console.ReadLine());

            Console.WriteLine("Введите количество техосмотров в год: ");
            var inspectionsPerYear = int.Parse(Console.ReadLine());

            Console.WriteLine("Введите дату последнего техосмотра: ");
            var lastInspection = DateOnly.Parse(Console.ReadLine());

            Console.WriteLine("Введите дату следующего техосмотра: ");
            var nextInspection = DateOnly.Parse(Console.ReadLine());

            Console.WriteLine("Введите процент брака: ");
            var defectRate = float.Parse(Console.ReadLine());

            var newLine = new AssemblyLineCreate(name, length, height, width, status, downTime, inspectionsPerYear, lastInspection, nextInspection, defectRate);

            try
            {
                var createdLine =  await _assemblyLineService.CreateAssemblyLineAsync(newLine);
                Console.WriteLine($"Линия с id {createdLine.Id} успешно добавлена.");
            }
            catch (AssemblyLineServiceException ex)
            {
                Console.WriteLine($"Ошибка при добавлении детали: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при добавлении детали: {ex.Message}");
            }
        }

        private async Task AddTractor()
        {
            Console.WriteLine("Введите название модели: ");
            var model = Console.ReadLine();

            Console.WriteLine("Введите год выпуска: ");
            var releaseYear = int.Parse(Console.ReadLine());

            Console.WriteLine("Введите тип двигателя: ");
            var engineType = Console.ReadLine();

            Console.WriteLine("Введите мощнось двигателя: ");
            var enginePower = Console.ReadLine();

            Console.WriteLine("Введите диаметр передних колес: ");
            var frontTierSize = int.Parse(Console.ReadLine());

            Console.WriteLine("Введите диаметр задних колес: ");
            var backTierSize = int.Parse(Console.ReadLine());

            Console.WriteLine("Введите количество колес колес: ");
            var wheelsAmount = int.Parse(Console.ReadLine());

            Console.WriteLine("Введите объем бензобака: ");
            var tankCapacity = int.Parse(Console.ReadLine());

            Console.WriteLine("Введите экологический стандарт: ");
            var ecologicalStandart = Console.ReadLine();

            Console.WriteLine("Введите длину: ");
            var length = float.Parse(Console.ReadLine());

            Console.WriteLine("Введите ширину: ");
            var width = float.Parse(Console.ReadLine());

            Console.WriteLine("Введите высоту по кабине: ");
            var cabinHeight = float.Parse(Console.ReadLine());

            var newTractor = new TractorCreate(model, releaseYear, engineType, enginePower, frontTierSize, backTierSize, wheelsAmount, tankCapacity, ecologicalStandart, length, width, cabinHeight);

            try
            {
                var createdTractor = await _tractorService.CreateTractorAsync(newTractor);
                Console.WriteLine($"Трактор с id {createdTractor.Id} успешно добавлена.");
            }
            catch (TractorServiceException ex)
            {
                Console.WriteLine($"Ошибка при добавлении детали: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при добавлении детали: {ex.Message}");
            }
        }

        private async Task GetUsers()
        {
            try
            {
                var filter = GetUsersFilter();
                var users = await _userService.GetAllUsersAsync(filter);

                if (users.Count == 0)
                {
                    Console.WriteLine("Пользователей пока нет");
                    return;
                }

                Console.WriteLine("==============================================================================");
                foreach (var user in users)
                {
                    Console.WriteLine($"ID: {user.Id}");
                    Console.WriteLine($"Name: {user.Name}");
                    Console.WriteLine($"Surname: {user.Surname}");
                    Console.WriteLine($"Patronymic: {user.Patronymic}");
                    Console.WriteLine($"Department: {user.Department}");
                    Console.WriteLine($"Email: {user.Email}");
                    Console.WriteLine($"BirthDay: {user.BirthDay}");
                    Console.WriteLine($"Sex: {user.Sex}");
                    Console.WriteLine($"Role: {user.Role}");
                    Console.WriteLine("--------------------------------------------------------------");
                }
            }
            catch (UserServiceException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            
        }

        private UserFilter GetUsersFilter()
        {
            Console.WriteLine("Пожалуйста, заполните необходимые поля фильтра (если хотите пропустить поле, введите \"-\")");

            // Ввод департамента
            Console.WriteLine("Введите название департамента: ");
            var department = Console.ReadLine();
            department = department == "-" ? null : department;

            // Ввод даты рождения
            Console.WriteLine("Введите дату рождения (в формате ГГГГ-ММ-ДД): ");
            var birthDayInput = Console.ReadLine();
            DateOnly? birthDay = birthDayInput == "-" ? null : DateOnly.Parse(birthDayInput);

            // Ввод пола
            Console.WriteLine("Введите пол (Male/Female): ");
            var sexInput = Console.ReadLine();
            SexType? sex = sexInput == "-" ? null : Enum.Parse<SexType>(sexInput, true);

            // Ввод роли
            Console.WriteLine("Введите роль (Admin/User/Manager и т.д.): ");
            var roleInput = Console.ReadLine();
            RoleType? role = roleInput == "-" ? null : Enum.Parse<RoleType>(roleInput, true);

            // Ввод количества записей для пропуска
            Console.WriteLine("Пропустить N записей: ");
            var skipInput = Console.ReadLine();
            int skip = skipInput == "-" ? 0 : int.Parse(skipInput);

            // Ввод лимита количества записей
            Console.WriteLine("Максимальное количество записей: ");
            var limitInput = Console.ReadLine();
            int limit = limitInput == "-" ? int.MaxValue : int.Parse(limitInput);

            // Создание и возврат объекта фильтра
            var filter = new UserFilter(department, birthDay, sex, role, skip, limit);
            return filter;
        }

        private async Task ChangeUserRole()
        {
            Console.WriteLine("Введите идентификатор пользователя:");
            var userIdInput = Console.ReadLine();

            if (Guid.TryParse(userIdInput, out Guid userId))
            {
                Console.WriteLine("Введите новую роль пользователя (Admin, Operator, Specialist, Verification):");
                var roleInput = Console.ReadLine();

                if (Enum.TryParse<RoleType>(roleInput, true, out RoleType role))
                {
                    try
                    {
                        var userUpdateRole = new UserUpdateRole(role);
                        await _userService.UpdateUserRole(userId, userUpdateRole);
                        Console.WriteLine("Роль пользователя успешно изменена.");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Ошибка при изменении роли пользователя: {ex.Message}");
                    }
                }
                else
                {
                    Console.WriteLine("Некорректная роль пользователя.");
                }
            }
            else
            {
                Console.WriteLine("Некорректный идентификатор пользователя.");
            }
        }

        private async Task Login()
        {
            const int maxAttempts = 3;
            int attempts = 0;

            while (attempts < maxAttempts)
            {
                try
                {
                    Console.WriteLine("Enter email:");
                    var email = Console.ReadLine();

                    Console.WriteLine("Enter password:");
                    var password = Console.ReadLine();

                    var authModel = new LoginModel
                    (
                        email: email,
                        password: password
                    );

                    _currentUser = await _authenticationService.LoginAsync(authModel);

                    await _authenticationService.SendTwoFactorCode(_currentUser);

                    Console.WriteLine("We sent you an email with a two factor code.");
                    Console.WriteLine("Enter two factor code:");
                    var code = Console.ReadLine();

                    if (await _authenticationService.VerifyTwoFactorCodeAsync(_currentUser.Id, code))
                    {
                        Console.WriteLine($"Welcome, {_currentUser.Name}!");
                        return;
                    }
                    else
                    {
                        Console.WriteLine("Invalid two factor code.");
                    }
                    
                }
                catch (UserNotFoundException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch (WrongPasswordException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch (LoginException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch (WrongTwoFactorCodeException ex)
                {
                    Console.WriteLine(ex.Message);
                }

                attempts++;
                if (attempts < maxAttempts)
                {
                    Console.WriteLine($"You have {maxAttempts - attempts} attempt(s) remaining.");
                }
                else
                {
                    Console.WriteLine("Too many failed attempts. Exiting...");
                    Environment.Exit(0);
                }
            }
        }

        private async Task Register()
        {
            try
            {
                Console.WriteLine("Enter email:");
                var email = Console.ReadLine();

                Console.WriteLine("Enter password: ");
                var password = Console.ReadLine();

                Console.WriteLine("Enter name: ");
                var name = Console.ReadLine();

                Console.WriteLine("Enter surname: ");
                var surname = Console.ReadLine();

                Console.WriteLine("Enter patronymic: ");
                var patronymic = Console.ReadLine();

                var sex = ReadSexTypeFromConsole();

                Console.WriteLine("Enter your department: ");
                var departmetn = Console.ReadLine();

                var birthDay = ReadBirthDateFromConsole();

                var regModel = new RegisterModel(email,
                    password,
                    name,
                    surname,
                    patronymic,
                    sex,
                    departmetn,
                    birthDay);

                var registerUser = await _authenticationService.RegisterAsync(regModel);
            }
            catch (UserAlreadyExistException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (RegisterException ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        private static DateOnly ReadBirthDateFromConsole()
        {
            while (true)
            {
                Console.WriteLine("Enter your date of birth (e.g., MM/dd/yyyy): ");
                string input = Console.ReadLine();

                if (DateOnly.TryParse(input, out DateOnly birthDate))
                {
                    return birthDate;
                }

                Console.WriteLine("Invalid date format. Please try again.");
            }
        }

        private static SexType ReadSexTypeFromConsole()
        {
            while (true)
            {
                Console.WriteLine("Enter sex (Male, Female): ");
                string input = Console.ReadLine();

                if (Enum.TryParse<SexType>(input, true, out var sexType) && Enum.IsDefined(typeof(SexType), sexType))
                {
                    return sexType;
                }

                Console.WriteLine("Invalid input. Please try again.");
            }
        }

        private async Task CreateServiceReport()
        {
            try
            {
                Console.WriteLine("Введите идентификатор запроса:");
                var requestIdInput = Console.ReadLine();

                if (!Guid.TryParse(requestIdInput, out Guid requestId))
                {
                    Console.WriteLine("Некорректный идентификатор запроса.");
                    return;
                }

                var report = new ServiceReportCreate(_currentUser.Id, requestId);

                var createdReport = await _serviceReportService.CreateServiceReportAsync(report);

                Console.WriteLine("Отчёт успешно создан:");
                Console.WriteLine($"Идентификатор отчёта: {createdReport.Id}");
                Console.WriteLine($"Дата создания: {createdReport.OpenDate}");
                Console.WriteLine($"Идентификатор запроса: {createdReport.RequestId}");
                Console.WriteLine($"Идентификатор пользователя: {createdReport.UserId}");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Ошибка при создании отчёта: {ex.Message}");
            }
            catch (ReportServiceException ex)
            {
                Console.WriteLine($"Ошибка при создании отчёта: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при создании отчёта: {ex.Message}");
            }
        }

        private async Task CloseServiceReport()
        {
            try
            {
                Console.WriteLine("Введите идентификатор отчёта:");
                var reportIdInput = Console.ReadLine();

                if (!Guid.TryParse(reportIdInput, out Guid reportId))
                {
                    Console.WriteLine("Некорректный идентификатор отчёта.");
                    return;
                }

                Console.WriteLine("Введите стоимость выполненных работ:");
                var priceInput = Console.ReadLine();
                if (!float.TryParse(priceInput, out float price))
                {
                    Console.WriteLine("Некорректная стоимость.");
                    return;
                }

                Console.WriteLine("Введите описание выполненных работ:");
                var description = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(description))
                {
                    Console.WriteLine("Описание не может быть пустым.");
                    return;
                }

                var reportClose = new ServiceReportClose(price, description);
                var closedReport = await _serviceReportService.CloseServiceReportAsync(reportId, reportClose);

                Console.WriteLine("Отчёт успешно закрыт:");
                Console.WriteLine($"Идентификатор отчёта: {closedReport.Id}");
                Console.WriteLine($"Дата закрытия: {closedReport.CloseDate}");
                Console.WriteLine($"Стоимость работ: {closedReport.Price}");
                Console.WriteLine($"Описание: {closedReport.Description}");
            }
            catch (ReportServiceException ex)
            {
                Console.WriteLine($"Ошибка при закрытии отчёта: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при закрытии отчёта: {ex.Message}");
            }
        }
    }
}

