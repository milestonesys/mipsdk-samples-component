using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SurveillanceCloudSample.SharedObjects;
using SurveillanceCloudSampleService.Helpers;
using SurveillanceCloudSampleService.Objects;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using VideoOS.Platform;
using VideoOS.Platform.ConfigurationItems;

namespace SurveillanceCloudSampleService.Controllers
{
    public class ValuesController : ApiController
    {
        //Dictionary of codes and corresponding number of cameras. You can change the code so it will take the codes from external file or database.
        //Also you can make codes one time only, so you have different code for each user.
        private Dictionary<string, int> codes = new Dictionary<string, int> { { "1cam", 1 }, { "2cams", 2 }, { "3cams", 3 }, { "4cams", 4 } };
        private List<Objects.User> users = new List<Objects.User>();
        private SurveillanceSettings settings = null;

        private static readonly Guid IntegrationId = new Guid("2B142E14-2082-494D-98C4-40ACB5AEC714");
        private const string IntegrationName = "Surveillance Cloud";
        private const string Version = "1.0";
        private const string ManufacturerName = "Sample Manufacturer";

        [HttpPost]
        public IHttpActionResult Register([FromBody]JObject parameters)
        {
            try
            {
                List<JProperty> properties = null;
                string userName;
                string password;
                string code;

                try
                {
                    //Getting parameters values
                    properties = parameters.Children().Cast<JProperty>().ToList();
                    userName = properties.Where(p => p.Name == "userName").FirstOrDefault()?.Value?.ToString();
                    password = properties.Where(p => p.Name == "password").FirstOrDefault()?.Value?.ToString();
                    code = properties.Where(p => p.Name == "code").FirstOrDefault()?.Value?.ToString();

                    //Checking if userName and password parameters are filled
                    if (string.IsNullOrWhiteSpace(userName) || string.IsNullOrWhiteSpace(password))
                    {
                        //returning error response
                        return Content(HttpStatusCode.BadRequest, "You must fill username and password fields");
                    }
                }
                catch
                {
                    return Content(HttpStatusCode.BadRequest, "Invalid input parameters");
                }

                //Checking if the entered code exists
                if (!codes.ContainsKey(code))
                {
                    //returning error response, because the entered code is not correct
                    return Content(HttpStatusCode.BadRequest, "Wrong code");
                }

                //Checking if the entered username already exists in the file
                if (users?.Where(u => u.Username == userName).Any() ?? false)
                {
                    //returning error response, because the username is already taken
                    return Content(HttpStatusCode.BadRequest, "Username already exists");
                }

                //Checking if the entered username is available in the VMS
                #region Check if user with this username already exists in the VMS
                var basicUserFolder = new BasicUserFolder();
                if (basicUserFolder.BasicUsers.Any(bu => bu.Name == userName))
                {
                    //returning error response, because the username is not available in the VMS.
                    return Content(HttpStatusCode.BadRequest, "Username already exists");
                }
                #endregion

                //Creating new user object and setting it's parameters
                Objects.User tmpUser = new Objects.User() { Id = (users?.Max(u => (int?)u.Id) ?? 0) + 1, Username = userName, Password = password, NumberOfCameras = codes[code], Cameras = new List<SurveillanceCloudSample.SharedObjects.Camera>() };

                //Creating new empty Camera objects. The count depends on the entered code.
                for (int i = 0; i < tmpUser.NumberOfCameras; i++)
                {
                    tmpUser.Cameras.Add(new SurveillanceCloudSample.SharedObjects.Camera());
                }

                #region Create User in VMS
                var addTask = basicUserFolder.AddBasicUser(userName, "Created by SurveillanceCloud sample", password);
                #endregion

                if (addTask.State == StateEnum.Success)
                {
                    var newUser = new BasicUser(EnvironmentManager.Instance.CurrentSite.ServerId, addTask.Path); // read out newly created user

                    #region Create Role in VMS
                    var roleFolder = new RoleFolder();
                    var addRole = roleFolder.AddRole(userName, "Created by SurveillanceCloud sample", false, false, true, true, true, string.Empty, string.Empty);
                    #endregion

                    if (addRole.State == StateEnum.Success)
                    {
                        #region Assign User to the Role
                        var newRole = new Role(EnvironmentManager.Instance.CurrentSite.ServerId, addRole.Path);
                        var newRoleMember = newRole.UserFolder.AddRoleMember(newUser.Sid);
                        #endregion

                        if (newRoleMember.State == StateEnum.Success)
                        {
                            if (users == null)
                            {
                                users = new List<Objects.User>();
                            }

                            //The user is successfully created and set up in the VMS, so we are adding the User object to the collection and write it to the file.
                            users.Add(tmpUser);
                            File.WriteAllText(HttpContext.Current.Server.MapPath("~/data.txt"), JsonConvert.SerializeObject(users, new JsonSerializerSettings() { ContractResolver = new ExcludeCameraUsernameAndPasswordResolver() }));

                            //returning success response
                            return Ok(new LoginResult() { UserId = tmpUser.Id });
                        }
                        else
                        {
                            //returning error response
                            return Content(HttpStatusCode.BadRequest, "Cannot add user to the role");
                        }
                    }
                    else
                    {
                        //returning error response
                        return Content(HttpStatusCode.BadRequest, "Cannot create the role");
                    }
                }
                else
                {
                    //returning error response
                    return Content(HttpStatusCode.BadRequest, "Cannot create the user");
                }

            }
            catch (MIPException ex)
            {
                return Content(HttpStatusCode.BadRequest, ex.Message);
            }
            catch (Exception ex)
            {
                //returning error response
                return Content(HttpStatusCode.InternalServerError, "Unknown error: " + ex.Message);
            }
        }

        [HttpPost]
        public IHttpActionResult Login([FromBody]JObject parameters)
        {
            List<JProperty> properties = null;
            string userName;
            string password;

            try
            {
                //Getting parameters values
                properties = parameters.Children().Cast<JProperty>().ToList();
                userName = properties.Where(p => p.Name == "userName").FirstOrDefault()?.Value?.ToString();
                password = properties.Where(p => p.Name == "password").FirstOrDefault()?.Value?.ToString();
            }
            catch
            {
                //returning error response
                return Content(HttpStatusCode.BadRequest, "Invalid input parameters");
            }

            try
            {
                LoginResult o = new LoginResult();
                Objects.User tmpUser;

                //Searching for such user
                if ((tmpUser = users?.Where(u => u.Username == userName && u.Password == password).FirstOrDefault()) != null)
                {
                    //If the user is found we will return success response
                    o.UserId = tmpUser.Id;
                    return Ok(o);
                }

                //Returning error response, because no such user exists or wrong password is entered
                return Content(HttpStatusCode.BadRequest, "Invalid username or password");
            }
            catch (Exception)
            {
                //returning error response
                return Content(HttpStatusCode.InternalServerError, "Unknown error");
            }
        }

        [HttpPost]
        public IHttpActionResult GetUserCameras([FromBody]JObject parameters)
        {
            List<JProperty> properties = null;
            int userId;

            try
            {
                //Getting parameters values
                properties = parameters.Children().Cast<JProperty>().ToList();
                userId = (properties.Where(p => p.Name == "userId").FirstOrDefault()?.Value as JValue)?.Value<int>() ?? 0;
            }
            catch
            {
                //returning error response
                return Content(HttpStatusCode.BadRequest, "Invalid input parameters");
            }

            try
            {
                GetUserCamerasResult o = new GetUserCamerasResult();
                Objects.User tmpUser;

                //Getting the user object
                if ((tmpUser = users?.Where(u => u.Id == userId).FirstOrDefault()) != null)
                {
                    //Filling the response with user's data
                    o.NumberOfCameras = tmpUser.NumberOfCameras;
                    o.Cameras = tmpUser.Cameras;

                    var recordingServerFolder = new RecordingServerFolder();
                    var recordingServerHardware = recordingServerFolder.RecordingServers.First().HardwareFolder.Hardwares;

                    //Getting each camera's credentials from the VMS
                    foreach (SurveillanceCloudSample.SharedObjects.Camera camera in o.Cameras.Where(c => !string.IsNullOrWhiteSpace(c.Address)))
                    {
                        var cameraHardware = recordingServerHardware.FirstOrDefault(hw => hw.Address.Contains(camera.Address));
                        if (cameraHardware != null)
                        {
                            // Set camera credentials in response
                            camera.Username = cameraHardware.UserName;
                            var readPasswordTask = cameraHardware.ReadPasswordHardware();
                            camera.Password = readPasswordTask.GetProperty("Password");
                        }
                    }

                    //returning success response
                    return Ok(o);
                }
                else
                {
                    //returning error response
                    return Content(HttpStatusCode.BadRequest, "Invalid input parameters");
                }
            }
            catch (Exception)
            {
                //returning error response
                return Content(HttpStatusCode.InternalServerError, "Unknown error");
            }
        }

        [HttpPost]
        public IHttpActionResult AddDevice([FromBody]JObject parameters)
        {
            List<JProperty> properties = null;
            int? userId;
            string userName;
            string password;
            string ipAddress;
            int? cameraId;
            string cameraName;

            try
            {
                //Getting parameters values
                properties = parameters.Children().Cast<JProperty>().ToList();
                userId = (properties.Where(p => p.Name == "userId").FirstOrDefault().Value as JValue).Value<int>();
                userName = properties.Where(p => p.Name == "userName").FirstOrDefault()?.Value?.ToString();
                password = properties.Where(p => p.Name == "password").FirstOrDefault()?.Value?.ToString();
                ipAddress = properties.Where(p => p.Name == "ipAddress").FirstOrDefault()?.Value?.ToString();
                cameraId = (properties.Where(p => p.Name == "cameraId").FirstOrDefault().Value as JValue).Value<int>();
                cameraName = properties.Where(p => p.Name == "name").FirstOrDefault()?.Value?.ToString();

                //Checking if userId and cameraId parameters are entered
                if (userId == null || cameraId == null)
                {
                    //returning error response
                    return Content(HttpStatusCode.BadRequest, "Invalid input parameters");
                }
            }
            catch
            {
                //returning error response
                return Content(HttpStatusCode.BadRequest, "Invalid input parameters");
            }

            try
            {
                //Creating response object
                AddDeviceResult o = new AddDeviceResult();
                //Setting the default response status to Problem
                o.Status = AddDeviceStatus.Problem;

                Objects.User tmpUser;

                //Getting the user object
                if ((tmpUser = users?.Where(u => u.Id == userId).FirstOrDefault()) != null)
                {
                    string cameraAddress;

                    //Checking if there is a camera on the selected slot or not. If a camera exists on the selected slot, we will remove it before adding the new one.
                    bool cameraReplace = !string.IsNullOrWhiteSpace((cameraAddress = tmpUser.Cameras[cameraId.Value].Address));

                    //If a camera exists on the selected slot and we don't provide a new address that is considered as only remove the camera,
                    //but if we don't provide address and there is no camera on the selected slot - this is not correct.
                    if (string.IsNullOrWhiteSpace(ipAddress) && !cameraReplace)
                    {
                        //returning error response
                        return Content(HttpStatusCode.BadRequest, "The address field cannot be empty");
                    }

                    var recordingServerFolder = new RecordingServerFolder();
                    if (cameraReplace)
                    {
                        #region Removing camera
                        var recordingServerHardwareFolder = recordingServerFolder.RecordingServers.First().HardwareFolder;
                        var hardwareWithCamera = recordingServerHardwareFolder.Hardwares.FirstOrDefault(hw => hw.Address.Contains(cameraAddress));
                        var deleteHardwareResult = recordingServerHardwareFolder.DeleteHardware(hardwareWithCamera.Path);
                        if (deleteHardwareResult.State != StateEnum.Error)
                        {
                            do
                            {
                                deleteHardwareResult.UpdateState();
                            }
                            while (deleteHardwareResult.Progress != 100);
                            if (deleteHardwareResult.State != StateEnum.Success)
                            {
                                //returning error response
                                return Content(HttpStatusCode.BadRequest, "Error removing the old camera");
                            }
                        }
                        else
                        {
                            //returning error response
                            return Content(HttpStatusCode.BadRequest, "Error removing the old camera");
                        }
                        #endregion
                    }

                    //If deleting the camera is the only thing we need to do, we just update the User object and seriale it in the file
                    if (string.IsNullOrWhiteSpace(ipAddress))
                    {
                        tmpUser.Cameras[cameraId.Value].Address = null;
                        tmpUser.Cameras[cameraId.Value].Name = null;
                        tmpUser.Cameras[cameraId.Value].Password = null;
                        tmpUser.Cameras[cameraId.Value].Username = null;
                        File.WriteAllText(HttpContext.Current.Server.MapPath("~/data.txt"), JsonConvert.SerializeObject(users, new JsonSerializerSettings() { ContractResolver = new ExcludeCameraUsernameAndPasswordResolver() }));
                        o.Status = AddDeviceStatus.Success;
                        //returning success response
                        return Ok(o);
                    }

                    #region Searching for hardware driver
                    //Trying the default credentials, if we don't provide such
                    bool useDefaultCredentials = string.IsNullOrWhiteSpace(userName) && string.IsNullOrWhiteSpace(password);
                    var hardwareScanTask = recordingServerFolder.RecordingServers.First().HardwareScan(ipAddress, string.Empty, useDefaultCredentials ? string.Empty : userName, useDefaultCredentials ? string.Empty : password, useDefaultCredentials);
                    #endregion
                    if (hardwareScanTask.State != StateEnum.Error)
                    {
                        do
                        {
                            hardwareScanTask.UpdateState();
                        } while (hardwareScanTask.Progress != 100);

                        if (hardwareScanTask.State == StateEnum.Success)
                        {
                            #region Adding Hardware
                            var addHardwareTask = recordingServerFolder.RecordingServers.First().AddHardware(hardwareScanTask.GetProperty("HardwareAddress"), hardwareScanTask.GetProperty("HardwareDriverPath"), hardwareScanTask.GetProperty("UserName"), hardwareScanTask.GetProperty("Password"));
                            #endregion

                            do
                            {
                                addHardwareTask.UpdateState();
                            } while (addHardwareTask.Progress != 100);

                            if (addHardwareTask.State == StateEnum.Success)
                            {
                                #region Enable Hardware
                                var hardware = new Hardware(EnvironmentManager.Instance.CurrentSite.ServerId, addHardwareTask.Path);
                                hardware.Enabled = true;
                                var saveOk = true;
                                try
                                {
                                    hardware.Save();
                                }
                                catch (Exception)
                                {
                                    saveOk = false;
                                }
                                #endregion
                                if (saveOk)
                                {
                                    //If there is no camera-group already then create one
                                    var cameraGroupFolder = new CameraGroupFolder();
                                    if (!cameraGroupFolder.CameraGroups.Any())
                                    {
                                        cameraGroupFolder.AddDeviceGroup("SurveillanceCloud", "Camera(s) for SurveillanceCloud");
                                    }

                                    #region Enable the cameras, adding to first available camera group and adding to role
                                    foreach (var camera in hardware.CameraFolder.Cameras)
                                    {
                                        #region Enable camera
                                        camera.Enabled = true;
                                        camera.Name = string.IsNullOrWhiteSpace(cameraName) ? camera.DisplayName : cameraName;
                                        saveOk = true;
                                        try
                                        {
                                            camera.Save();
                                        }
                                        catch
                                        {
                                            saveOk = false;
                                        }
                                        #endregion
                                        if (saveOk)
                                        {
                                            #region Adding the camera to the first available camera group
                                            var addDeviceGroupMemberTask = cameraGroupFolder.CameraGroups.First().CameraFolder.AddDeviceGroupMember(camera.Path);
                                            #endregion
                                            if (addDeviceGroupMemberTask.State == StateEnum.Success)
                                            {
                                                #region Adding the camera to the role
                                                var roleFolder = new RoleFolder();
                                                var role = roleFolder.Roles.FirstOrDefault(r => r.Name == tmpUser.Username);
                                                if (role != null)
                                                {
                                                    var changeSecurityPermissionsTask = camera.ChangeSecurityPermissions(role.Path); // TODO: should this be user[SID] instead?
                                                    changeSecurityPermissionsTask.SetProperty("GENERIC_READ", true.ToString());
                                                    changeSecurityPermissionsTask.SetProperty("VIEW_LIVE", true.ToString());
                                                    changeSecurityPermissionsTask.SetProperty("PLAYBACK", true.ToString());
                                                    changeSecurityPermissionsTask.ExecuteDefault();
                                                    #endregion
                                                }
                                            }
                                            else
                                            {
                                                //returning error response
                                                return Content(HttpStatusCode.BadRequest, "Error adding the camera to a group");
                                            }
                                        }
                                        else
                                        {
                                            //returning error response
                                            return Content(HttpStatusCode.BadRequest, "Error enabling the camera");
                                        }
                                    }
                                    #endregion

                                    //Updating User data and serializing the collection to the file
                                    tmpUser.Cameras[cameraId.Value].Address = ipAddress;
                                    tmpUser.Cameras[cameraId.Value].Name = cameraName;
                                    File.WriteAllText(HttpContext.Current.Server.MapPath("~/data.txt"), JsonConvert.SerializeObject(users, new JsonSerializerSettings() { ContractResolver = new ExcludeCameraUsernameAndPasswordResolver() }));

                                    //setting the status of the response object to success
                                    o.Status = AddDeviceStatus.Success;
                                }
                                else
                                {
                                    //returning error response
                                    return Content(HttpStatusCode.BadRequest, "Error enabling the device");
                                }
                            }
                            else
                            {
                                //returning error response
                                return Content(HttpStatusCode.BadRequest, "Camera already added to the system");
                            }
                        }
                        else
                        {
                            //returning error response
                            return Content(HttpStatusCode.BadRequest, "Invalid address, camera connection problem or unsupported camera.");
                        }
                    }
                    else
                    {
                        //returning error response
                        return Content(HttpStatusCode.BadRequest, "Invalid address or camera connection problem");
                    }
                }
                else
                {
                    //returning error response
                    return Content(HttpStatusCode.BadRequest, "Invalid input parameters");
                }

                //returning success response
                return Ok(o);
            }
            catch (Exception)
            {
                //returning error response
                return Content(HttpStatusCode.InternalServerError, "Unknown error");
            }
        }

        public ValuesController()
        {
            //Reading registered users from the json formatted file.
            //You can rewrite the code to keep the users in database or some other external source
            if (File.Exists(HttpContext.Current.Server.MapPath("~/data.txt")))
            {
                try
                {
                    users = JsonConvert.DeserializeObject<List<Objects.User>>(File.ReadAllText(HttpContext.Current.Server.MapPath("~/data.txt")));
                }
                catch
                {
                    //No need to do anything. The file will be created when the first user is registered.
                }
            }

            try
            {
                //Getting connection parameters from the VMSSettings/Address section of the config file
                NameValueCollection settingsCollection = ConfigurationManager.GetSection("VMSSettings/Address") as NameValueCollection;
                settings = new SurveillanceSettings();

                //Setting connection parameters to the settings object
                settings.SecureOnly = bool.Parse(settingsCollection["SecureOnly"]);
                settings.SurveillanceScheme = settingsCollection["Scheme"];
                settings.SurveillanceAddress = settingsCollection["Ip"];
                settings.SurveillancePort = int.Parse(settingsCollection["Port"]);

                //Getting connection parameters from the VMSSettings/Credentials section of the config file
                settingsCollection = ConfigurationManager.GetSection("VMSSettings/Credentials") as NameValueCollection;

                //Setting credentials parameters to the settings object
                settings.Username = settingsCollection["Domain"];
                settings.Username += (!string.IsNullOrWhiteSpace(settings.Username) ? "\\" : string.Empty) + settingsCollection["Username"];
                settings.Password = settingsCollection["Password"];
                settings.IsAdUser = settingsCollection["Authtype"].ToLower() == "digest" ? true : false;
            }
            catch
            {
                Debugger.Break();
                //returning error response
                throw new Exception("Could not read all necessary settings from the web.config file.");
            }

            try
            {
                VideoOS.Platform.SDK.Environment.Initialize();
                Login(settings.SecureOnly, new UriBuilder(settings.SurveillanceScheme, settings.SurveillanceAddress, settings.SurveillancePort).Uri, settings.IsAdUser, settings.Username, settings.Password);
            }
            catch
            {
                Debugger.Break();
                //returning error response
                throw new Exception("Could not connect to the VMS Server. Check your settings");
            }
        }

        /// <summary>
        /// Login routine 
        /// </summary>
        /// <returns></returns> True if successfully logged in
        static private void Login(bool secureOnly, Uri uri, bool isAduser, string userName, string password)
        {
            if (isAduser)
            {
                VideoOS.Platform.SDK.Environment.AddServer(secureOnly, uri, new NetworkCredential(userName, password));
            }
            else
            {
                VideoOS.Platform.SDK.Environment.AddServer(secureOnly, uri, VideoOS.Platform.Login.Util.BuildCredentialCache(uri, userName, password, "Basic"));
            }

            VideoOS.Platform.SDK.Environment.Login(uri, IntegrationId, IntegrationName, Version, ManufacturerName);
        }
    }
}
