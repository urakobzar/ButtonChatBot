using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SoftPortalBot.Model.DataBaseContext;
using SoftPortalBot.Model.DataBaseTable;

namespace SoftPortalBot.Model.StartingData
{
    public static class StartingData
    {
        private static readonly Context Context = new();

        /// <summary>
        /// Внесение в базу данных пользователей.
        /// </summary>
        private static void AddUsers()
        {
            try
            {
                List<User> user = new()
                {
                    new User("testname1@mail.ru",
                        "TestSurname1", "TestName1", "TestPatronymic1"),
                    new User("testname2@mail.ru",
                        "TestSurname2", "TestName2", "TestPatronymic2"),
                    new User("testname3@mail.ru",
                        "TestSurname3", "TestName3", "TestPatronymic3"),
                    new User("testname4@mail.ru",
                        "TestSurname4", "TestName4", "TestPatronymic4"),
                    new User("testname5@mail.ru",
                        "TestSurname5", "TestName5", "TestPatronymic5"),
                };

                foreach (var a in user)
                {
                    try
                    {
                        if (Context.Users.First(m =>
                                m.Name.Equals(a.Name)) == null)
                        {
                            Context.Users.Add(a);
                        }
                    }
                    catch (InvalidOperationException)
                    {
                        Context.Users.Add(a);
                    }
                }

                Context.SaveChanges();
            }
            catch
            {

            }
        }

        /// <summary>
        /// Внесение в базу данных ролей.
        /// </summary>
        private static void AddRoles()
        {
            try
            {
                List<Role> roles = new()
                {
                    new Role("Администратор"),
                    new Role("Аналитик"),
                    new Role("Пользователь")
                };

                foreach (var a in roles)
                {
                    try
                    {
                        if (Context.Roles.First(m =>
                                m.Name.Equals(a.Name)) == null)
                        {
                            Context.Roles.Add(a);
                        }
                    }
                    catch (InvalidOperationException)
                    {
                        Context.Roles.Add(a);
                    }
                }

                Context.SaveChanges();
            }
            catch
            {

            }
        }

        /// <summary>
        /// Внесение в базу данных связи между ролью и пользователем.
        /// </summary>
        private static void AddRoleUsers()
        {
            try
            {
                List<RoleUser> roleUser = new()
                {
                    new RoleUser(Context.Users.First(m
                            => m.Login.Equals("testname1@mail.ru")).Id,
                        Context.Roles.First(m => m.Name.Equals("Пользователь")).Id),
                    new RoleUser(Context.Users.First(m
                            => m.Login.Equals("testname2@mail.ru")).Id,
                        Context.Roles.First(m => m.Name.Equals("Аналитик")).Id),
                    new RoleUser(Context.Users.First(m
                            => m.Login.Equals("testname3@mail.ru")).Id,
                        Context.Roles.First(m => m.Name.Equals("Администратор")).Id),
                };
                foreach (var a in roleUser)
                {
                    try
                    {
                        if (Context.RoleUsers.First(m =>
                                m.UserId.Equals(a.UserId)) == null)
                        {
                            Context.RoleUsers.Add(a);
                        }
                    }
                    catch (InvalidOperationException)
                    {
                        Context.RoleUsers.Add(a);
                    }
                }

                Context.SaveChanges();
            }
            catch
            {

            }
        }

        /// <summary>
        /// Внесение в базу данных ответственных групп.
        /// </summary>
        private static void AddResponsibleGroups()
        {
            try
            {
                List<ResponsibleGroup> responsibleGroup = new()
                {
                    new ResponsibleGroup("Администрирование"),
                    new ResponsibleGroup("3D-моделирование"),
                    new ResponsibleGroup("TestGroup")
                };
                foreach (var a in responsibleGroup)
                {
                    try
                    {
                        if (Context.ResponsibleGroups.First(m =>
                                m.Name.Equals(a.Name)) == null)
                        {
                            Context.ResponsibleGroups.Add(a);
                        }
                    }
                    catch (InvalidOperationException)
                    {
                        Context.ResponsibleGroups.Add(a);
                    }
                }

                Context.SaveChanges();
            }
            catch
            {

            }
        }

        /// <summary>
        /// Внесение в базу данных связи между пользователем и ответственной группой.
        /// </summary>
        private static void AddUserResponsibleGroups()
        {
            try
            {
                List<UserResponsibleGroup> roleUser = new()
                {
                    new UserResponsibleGroup(Context.Users.First(m
                        => m.Login.Equals("testname1@mail.ru")).Id,
                    Context.ResponsibleGroups.First(m
                        => m.Name.Equals("3D-моделирование")).Id),
                    new UserResponsibleGroup(Context.Users.First(m
                        => m.Login.Equals("testname2@mail.ru")).Id,
                        Context.ResponsibleGroups.First(m
                        => m.Name.Equals("Администрирование")).Id),
                    new UserResponsibleGroup(Context.Users.First(m
                        => m.Login.Equals("testname3@mail.ru")).Id,
                        Context.ResponsibleGroups.First(m
                        => m.Name.Equals("Администрирование")).Id),
                    new UserResponsibleGroup(Context.Users.First(m
                            => m.Login.Equals("testname4@mail.ru")).Id,
                        Context.ResponsibleGroups.First(m
                            => m.Name.Equals("TestGroup")).Id),
                    new UserResponsibleGroup(Context.Users.First(m
                            => m.Login.Equals("testname5@mail.ru")).Id,
                        Context.ResponsibleGroups.First(m
                            => m.Name.Equals("TestGroup")).Id),
                };
                foreach (var a in roleUser)
                {
                    try
                    {
                        if (Context.UserResponsibleGroup.First(m =>
                                m.UserId.Equals(a.UserId)) == null)
                        {
                            Context.UserResponsibleGroup.Add(a);
                        }
                    }
                    catch (InvalidOperationException)
                    {
                        Context.UserResponsibleGroup.Add(a);
                    }
                }

                Context.SaveChanges();
            }
            catch
            {

            }
        }

        /// <summary>
        /// Внесение в БД категории приложения.
        /// </summary>
        private static void AddAppTypes()
        {
            try
            {
                List<AppType> appTypes = new()
                {
                    new AppType("Macros", "Макрос"),
                    new AppType("Addin", "Надстройка"),
                    new AppType("Console", "Консольное приложение"),
                    new AppType("Plugin", "Плагин"),
                    new AppType("Desktop", "Десктопное приложение"),
                    new AppType("Web", "Веб-приложение"),
                    new AppType("Service", "Веб-сервис")
                };

                foreach (var a in appTypes)
                {
                    try
                    {
                        if (Context.AppTypes.First(m => m.Name.Equals(a.Name)) == null)
                        {
                            Context.AppTypes.Add(a);
                        }
                    }
                    catch (InvalidOperationException)
                    {
                        Context.AppTypes.Add(a);
                    }

                }

                Context.SaveChanges();
            }
            catch
            {

            }
        }

        /// <summary>
        /// Внесение в БД приложения.
        /// </summary>
        private static void AddApplications()
        {
            // Внесение в БД приложения
            try
            {
                List<Application> apps = new()
                {
                    new Application("App1", Context.AppTypes.First(m
                        =>m.Name.Equals("Web")).Id),
                    new Application("App2", Context.AppTypes.First(m
                        => m.Name.Equals("Web")).Id),
                    new Application("App3", Context.AppTypes.First(m
                        => m.Name.Equals("Web")).Id),
                    new Application("App4", Context.AppTypes.First(m
                        => m.Name.Equals("Desktop")).Id),
                    new Application("App5", Context.AppTypes.First(m
                        => m.Name.Equals("Web")).Id)
                };
                foreach (var a in apps)
                {
                    try
                    {
                        if (Context.Applications.First(m => m.Name.Equals(a.Name)) == null)
                        {
                            Context.Applications.Add(a);
                        }
                    }
                    catch (InvalidOperationException)
                    {
                        Context.Applications.Add(a);
                    }
                }

                Context.SaveChanges();
            }
            catch
            {

            }
        }

        /// <summary>
        /// Внесение в БД приложения.
        /// </summary>
        private static void AddApplicationResponsibleGroups()
        {
            // Внесение в БД приложения
            try
            {
                List<ApplicationResponsibleGroup> apps = new()
                {
                    new ApplicationResponsibleGroup(Context.Applications.First(m
                        => m.Name.Equals("App1")).Id, Context.ResponsibleGroups.First(m
                        => m.Name.Equals("Администрирование")).Id),
                    new ApplicationResponsibleGroup(Context.Applications.First(m
                        => m.Name.Equals("App2")).Id, Context.ResponsibleGroups.First(m
                        => m.Name.Equals("Администрирование")).Id),
                    new ApplicationResponsibleGroup(Context.Applications.First(m
                        => m.Name.Equals("App3")).Id, Context.ResponsibleGroups.First(m
                        => m.Name.Equals("Администрирование")).Id),
                    new ApplicationResponsibleGroup(Context.Applications.First(m
                        => m.Name.Equals("App4")).Id, Context.ResponsibleGroups.First(m
                        => m.Name.Equals("3D-моделирование")).Id),
                    new ApplicationResponsibleGroup(Context.Applications.First(m
                        => m.Name.Equals("App5")).Id, Context.ResponsibleGroups.First(m
                        => m.Name.Equals("TestGroup")).Id)
                };
                foreach (var a in apps)
                {
                    try
                    {
                        if (Context.ApplicationResponsibleGroups.First(m
                                => m.ApplicationId.Equals(a.ApplicationId)) == null)
                        {
                            Context.ApplicationResponsibleGroups.Add(a);
                        }
                    }
                    catch (InvalidOperationException)
                    {
                        Context.ApplicationResponsibleGroups.Add(a);
                    }
                }

                Context.SaveChanges();
            }
            catch
            {

            }
        }


        /// <summary>
        /// Внесение в БД ответа базы знаний.
        /// </summary>
        private static void AddKnowledgeBaseResponses()
        {
            // Внесение в БД приложения
            try
            {
                List<ProblemResponse> responses = new()
                {
                    new ProblemResponse(Context.Applications.First(m
                            => m.Name.Equals("App1")).Id, "Установка", "Установка ПО App1",
                    "Устанавливать ничего не надо, нужно всего лишь зайти на сайт."),
                    new ProblemResponse(Context.Applications.First(m
                            => m.Name.Equals("App1")).Id, "Описание и инструкция", "Описание и инструкция для ПО App1",
                    "App1 - программное обеспечение разработанное компанией \"COMPANY\"."),
                    new ProblemResponse(Context.Applications.First(m
                            => m.Name.Equals("App1")).Id, "Ошибки при установке", "Ошибки при установке ПО App1",
                    "Установки нет, поэтому и ошибок никаких быть не может.")
                };
                foreach (var a in responses)
                {
                    try
                    {
                        if (Context.ProblemResponses.First(m => m.Name.Equals(a.Name)) == null)
                        {
                            Context.ProblemResponses.Add(a);
                        }
                    }
                    catch (InvalidOperationException)
                    {
                        Context.ProblemResponses.Add(a);
                    }
                }

                Context.SaveChanges();
            }
            catch
            {

            }
        }


        /// <summary>
        /// Внесение в БД статуса заявки.
        /// </summary>
        private static void AddRequestStatus()
        {
            // Внесение в БД приложения
            try
            {
                List<RequestStatus> requestStatus = new()
                {
                    new RequestStatus("На рассмотрении"),
                    new RequestStatus("В работе"),
                    new RequestStatus("Выполнена"),
                    new RequestStatus("Дан ответ")
                };
                foreach (var a in requestStatus)
                {
                    try
                    {
                        if (Context.RequestStatus.First(m => m.Name.Equals(a.Name)) == null)
                        {
                            Context.RequestStatus.Add(a);
                        }
                    }
                    catch (InvalidOperationException)
                    {
                        Context.RequestStatus.Add(a);
                    }
                }

                Context.SaveChanges();
            }
            catch
            {

            }
        }

        /// <summary>
        /// Внесение в БД причины заявки.
        /// </summary>
        private static void AddRequestReason()
        {
            // Внесение в БД приложения
            try
            {
                List<RequestReason> requestStatus = new()
                {
                    new RequestReason("Установка"),
                    new RequestReason("Необходима консультация"),
                    new RequestReason("Ошибки ПО"),
                    new RequestReason("Другое")
                };
                foreach (var a in requestStatus)
                {
                    try
                    {
                        if (Context.RequestReasons.First(m => m.Name.Equals(a.Name)) == null)
                        {
                            Context.RequestReasons.Add(a);
                        }
                    }
                    catch (InvalidOperationException)
                    {
                        Context.RequestReasons.Add(a);
                    }
                }

                Context.SaveChanges();
            }
            catch
            {

            }
        }

        /// <summary>
        /// Добавить все данные данные.
        /// </summary>
        public static void AddAllData()
        {
            AddUsers();
            AddRoles();
            AddRoleUsers();
            AddResponsibleGroups();
            AddUserResponsibleGroups();
            AddAppTypes();
            AddApplications();
            AddKnowledgeBaseResponses();
            AddApplicationResponsibleGroups();
            AddRequestStatus();
            AddRequestReason();
        }
    }
}
