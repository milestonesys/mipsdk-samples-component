using Helpers;
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
using VideoOS.ConfigurationAPI;

namespace SurveillanceCloudSampleService.Controllers
{
    public class ValuesController : ApiController
    {
        //Dictionary of codes and coresponding number of cameras. You can change the code so it will take the codes from external file or database.
        //Also you can make codes one time only, so you have differen code for each user.
        private Dictionary<string, int> codes = new Dictionary<string, int> { { "1cam", 1 }, { "2cams", 2 }, { "3cams", 3 }, { "4cams", 4 } };
        private List<User> users = new List<User>();
        private SurveillanceSettings settings = null;
        private ConfigAPIClient configApiClient = null;

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
                if (configApiClient.GetChildItems("/" + ItemTypes.BasicUserFolder).Where(c => c.Properties.Where(p => p.Key == "Name" && p.Value == userName).FirstOrDefault() != null).FirstOrDefault() != null)
                {
                    //returning error response, because the username is not available in the VMS.
                    return Content(HttpStatusCode.BadRequest, "Username already exists");
                }
                #endregion

                //Creating new user object and setting it's parameters
                User tmpUser = new User() { Id = (users?.Max(u => (int?)u.Id) ?? 0) + 1, Username = userName, Password = password, NumberOfCameras = codes[code], Cameras = new List<Camera>() };

                //Creating new empty Camera objects. The count depends on the entered code.
                for (int i = 0; i < tmpUser.NumberOfCameras; i++)
                {
                    tmpUser.Cameras.Add(new Camera());
                }

                #region Create User in VMS
                ConfigurationItem tmpConfigurationItem = configApiClient.InvokeMethod(configApiClient.GetItem("/" + ItemTypes.BasicUserFolder), "AddBasicUser");
                tmpConfigurationItem.Properties.Where(p => p.Key == "Name").FirstOrDefault().Value = userName;
                tmpConfigurationItem.Properties.Where(p => p.Key == "Password").FirstOrDefault().Value = password;
                ConfigurationItem resultConfigurationItem = configApiClient.InvokeMethod(tmpConfigurationItem, "AddBasicUser");
                #endregion
                if (resultConfigurationItem.Properties.FirstOrDefault(p => p.Key == InvokeInfoProperty.State).Value == InvokeInfoStates.Success)
                {
                    string newUserSid = resultConfigurationItem.Properties.Where(p => p.Key == "Sid").FirstOrDefault().Value;
                    #region Create Role in VMS
                    tmpConfigurationItem = configApiClient.InvokeMethod(configApiClient.GetItem("/" + ItemTypes.RoleFolder), "AddRole");
                    tmpConfigurationItem.Properties.Where(p => p.Key == "Name").FirstOrDefault().Value = userName;
                    resultConfigurationItem = configApiClient.InvokeMethod(tmpConfigurationItem, "AddRole");
                    #endregion
                    if (resultConfigurationItem.Properties.FirstOrDefault(p => p.Key == InvokeInfoProperty.State).Value == InvokeInfoStates.Success)
                    {
                        #region Assign User to the Role
                        tmpConfigurationItem = configApiClient.InvokeMethod(configApiClient.GetChildItems(resultConfigurationItem.Path)[0], "AddRoleMember");
                        tmpConfigurationItem.Properties.Where(p => p.Key == "Sid").FirstOrDefault().Value = newUserSid;
                        resultConfigurationItem = configApiClient.InvokeMethod(tmpConfigurationItem, "AddRoleMember");
                        #endregion
                        if (resultConfigurationItem.Properties.FirstOrDefault(p => p.Key == InvokeInfoProperty.State).Value == InvokeInfoStates.Success)
                        {
                            if (users == null)
                            {
                                users = new List<User>();
                            }

                            //The user is successfuly created and setted up in the VMS, so we are adding the User object to the collection and write it to the file.
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
            catch (Exception)
            {
                //returning error response
                return Content(HttpStatusCode.InternalServerError, "Unknown error");
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
                User tmpUser;

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
                User tmpUser;

                //Getting the user object
                if ((tmpUser = users?.Where(u => u.Id == userId).FirstOrDefault()) != null)
                {
                    //Filling the response with user's data
                    o.NumberOfCameras = tmpUser.NumberOfCameras;
                    o.Cameras = tmpUser.Cameras;

                    ConfigurationItem[] recordingServerHardware = configApiClient.GetChildItems(configApiClient.GetChildItems(configApiClient.GetItem("/" + ItemTypes.RecordingServerFolder).Path)[0].Path + "/" + ItemTypes.HardwareFolder);

                    //Getting each camera's credentials from the VMS
                    foreach (Camera camera in o.Cameras.Where(c => !string.IsNullOrWhiteSpace(c.Address)))
                    {
                        ConfigurationItem tmpConfigurationItem = recordingServerHardware.Where(c => c.Properties.Where(p => p.Key == "Address" && p.Value.Contains(camera.Address)).FirstOrDefault() != null).FirstOrDefault();
                        if (tmpConfigurationItem != null)
                        {
                            //Filling getted from the VMS camera's credentials to the response
                            camera.Username = tmpConfigurationItem.Properties.Where(p => p.Key == "UserName").FirstOrDefault()?.Value ?? string.Empty;
                            camera.Password = configApiClient.InvokeMethod(configApiClient.InvokeMethod(tmpConfigurationItem, "ReadPasswordHardware"), "ReadPasswordHardware").Properties.Where(p => p.Key == "Password").FirstOrDefault()?.Value ?? string.Empty;
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

                User tmpUser;
                ConfigurationItem tmpConfigurationItem;
                ConfigurationItem resultConfigurationItem;

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

                    if (cameraReplace)
                    {
                        #region Removing camera
                        tmpConfigurationItem = configApiClient.InvokeMethod(configApiClient.GetItem(configApiClient.GetChildItems(configApiClient.GetItem("/" + ItemTypes.RecordingServerFolder).Path)[0].Path + "/" + ItemTypes.HardwareFolder), "DeleteHardware");
                        tmpConfigurationItem.Properties.Where(p => p.Key == "ItemSelection").FirstOrDefault().Value = configApiClient.GetChildItems(configApiClient.GetItem(configApiClient.GetChildItems(configApiClient.GetItem("/" + ItemTypes.RecordingServerFolder).Path)[0].Path + "/" + ItemTypes.HardwareFolder).Path).Where(h => h.Properties.Where(p => p.Key == "Address" && p.Value.Contains(cameraAddress)).FirstOrDefault() != null).FirstOrDefault().Path;
                        resultConfigurationItem = configApiClient.InvokeMethod(tmpConfigurationItem, "DeleteHardware");
                        if (resultConfigurationItem.Properties.FirstOrDefault(p => p.Key == InvokeInfoProperty.State).Value != InvokeInfoStates.Error)
                        {
                            do
                            {
                                tmpConfigurationItem = configApiClient.GetItem(resultConfigurationItem.Properties.Where(p => p.Key == "Path").FirstOrDefault().Value);
                            }
                            while ((tmpConfigurationItem.Properties.FirstOrDefault(p => p.Key == "Progress")?.Value ?? "0") != "100");
                            if (tmpConfigurationItem.Properties.FirstOrDefault(p => p.Key == InvokeInfoProperty.State).Value != InvokeInfoStates.Success)
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

                    //If deleting the camera is the only thing we need to do, we just updating the User object and serializing it in the file
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
                    tmpConfigurationItem = configApiClient.InvokeMethod(configApiClient.GetChildItems(configApiClient.GetItem("/" + ItemTypes.RecordingServerFolder).Path)[0], "HardwareScan");
                    tmpConfigurationItem.Properties.Where(p => p.Key == "HardwareAddress").FirstOrDefault().Value = ipAddress;
                    tmpConfigurationItem.Properties.Where(p => p.Key == "UseDefaultCredentials").FirstOrDefault().Value = useDefaultCredentials.ToString();
                    if (!useDefaultCredentials)
                    {
                        tmpConfigurationItem.Properties.Where(p => p.Key == "UserName").FirstOrDefault().Value = userName;
                        tmpConfigurationItem.Properties.Where(p => p.Key == "Password").FirstOrDefault().Value = password;
                    }
                    resultConfigurationItem = configApiClient.InvokeMethod(tmpConfigurationItem, "HardwareScan");
                    #endregion
                    if (resultConfigurationItem.Properties.FirstOrDefault(p => p.Key == InvokeInfoProperty.State).Value != InvokeInfoStates.Error)
                    {
                        do
                        {
                            tmpConfigurationItem = configApiClient.GetItem(resultConfigurationItem.Properties.Where(p => p.Key == "Path").FirstOrDefault().Value);
                        }
                        while ((tmpConfigurationItem.Properties.FirstOrDefault(p => p.Key == "Progress")?.Value ?? "0") != "100");

                        if (tmpConfigurationItem.Properties.FirstOrDefault(p => p.Key == InvokeInfoProperty.State).Value == InvokeInfoStates.Success)
                        {
                            #region Adding Hardware
                            resultConfigurationItem = configApiClient.InvokeMethod(configApiClient.GetChildItems(configApiClient.GetItem("/" + ItemTypes.RecordingServerFolder).Path)[0], "AddHardware");

                            resultConfigurationItem.Properties.Where(p => p.Key == "HardwareAddress").FirstOrDefault().Value = tmpConfigurationItem.Properties.FirstOrDefault(p => p.Key == "HardwareAddress").Value;
                            resultConfigurationItem.Properties.Where(p => p.Key == "UserName").FirstOrDefault().Value = tmpConfigurationItem.Properties.FirstOrDefault(p => p.Key == "UserName").Value;
                            resultConfigurationItem.Properties.Where(p => p.Key == "Password").FirstOrDefault().Value = tmpConfigurationItem.Properties.FirstOrDefault(p => p.Key == "Password").Value;
                            resultConfigurationItem.Properties.Where(p => p.Key == "HardwareDriverPath").FirstOrDefault().Value = tmpConfigurationItem.Properties.FirstOrDefault(p => p.Key == "HardwareDriverPath").Value;
                            tmpConfigurationItem = configApiClient.InvokeMethod(resultConfigurationItem, "AddHardware");
                            #endregion

                            do
                            {
                                resultConfigurationItem = configApiClient.GetItem(tmpConfigurationItem.Properties.Where(p => p.Key == "Path").FirstOrDefault().Value);
                            }
                            while ((resultConfigurationItem.Properties.FirstOrDefault(p => p.Key == "Progress")?.Value ?? "0") != "100");

                            if (resultConfigurationItem.Properties.FirstOrDefault(p => p.Key == InvokeInfoProperty.State).Value == InvokeInfoStates.Success)
                            {
                                #region Enable Hardware
                                resultConfigurationItem = configApiClient.GetItem(resultConfigurationItem.Properties.FirstOrDefault(p => p.Key == "Path").Value);
                                resultConfigurationItem.EnableProperty.Enabled = true;
                                var result = configApiClient.SetItem(resultConfigurationItem);
                                #endregion
                                if (result.ValidatedOk)
                                {
                                    //If there is no camera-group already then create one
                                    if (configApiClient.GetChildItems("/" + ItemTypes.CameraGroupFolder).Length == 0)
                                    {
                                        var cameraGroupItem = configApiClient.GetItem("/" + ItemTypes.CameraGroupFolder);
                                        cameraGroupItem.Properties = new Property[] { new Property { Key = "GroupName", DisplayName = "SurveillanceCloud", IsSettable = true, Value = "SurveillanceCloud" }, new Property { Key = "GroupDescription", DisplayName = "Camera(s) for SurveillanceCloud", IsSettable = true, Value = "Camera(s) for SurveillanceCloud" } };
                                        cameraGroupItem.ItemCategory = ItemCategories.Item;
                                        cameraGroupItem.ItemType = ItemTypes.InvokeInfo;
                                        configApiClient.InvokeMethod(cameraGroupItem, "AddDeviceGroup");
                                    }

                                    #region Enable the cameras, adding to first available camera group and adding to role
                                    foreach (var configItem in configApiClient.GetChildItems(resultConfigurationItem.Path + "/" + ItemTypes.CameraFolder))
                                    {
                                        #region Enable camera
                                        configItem.EnableProperty.Enabled = true;
                                        configItem.Properties.FirstOrDefault(p => p.Key == "Name").Value = string.IsNullOrWhiteSpace(cameraName) ? configItem.DisplayName : cameraName;
                                        result = configApiClient.SetItem(configItem);
                                        #endregion
                                        if (result.ValidatedOk)
                                        {
                                            #region Adding the camera to the first available camera group
                                            tmpConfigurationItem = configApiClient.InvokeMethod(configApiClient.GetItem(configApiClient.GetChildItems("/" + ItemTypes.CameraGroupFolder)[0].Path + "/" + ItemTypes.CameraFolder), "AddDeviceGroupMember");
                                            tmpConfigurationItem.Properties.Where(p => p.Key == "ItemSelection").FirstOrDefault().Value = configItem.Path;
                                            tmpConfigurationItem = configApiClient.InvokeMethod(tmpConfigurationItem, "AddDeviceGroupMember");
                                            #endregion
                                            if (tmpConfigurationItem.Properties.FirstOrDefault(p => p.Key == InvokeInfoProperty.State).Value == InvokeInfoStates.Success)
                                            {
                                                #region Adding the camera to the role
                                                tmpConfigurationItem = configApiClient.InvokeMethod(configItem, "ChangeSecurityPermissions");
                                                tmpConfigurationItem.Properties.Where(p => p.Key == "UserPath").FirstOrDefault().Value = configApiClient.GetChildItems("/" + ItemTypes.RoleFolder).Where(i => i.ItemType == ItemTypes.Role && i.Properties.Where(p => p.Key == "Name" && p.Value == tmpUser.Username).FirstOrDefault() != null).FirstOrDefault().Path;
                                                resultConfigurationItem = configApiClient.InvokeMethod(tmpConfigurationItem, "ChangeSecurityPermissions");
                                                resultConfigurationItem.Properties.Where(p => p.Key == "GENERIC_READ").FirstOrDefault().Value = true.ToString();
                                                resultConfigurationItem.Properties.Where(p => p.Key == "VIEW_LIVE").FirstOrDefault().Value = true.ToString();
                                                resultConfigurationItem.Properties.Where(p => p.Key == "PLAYBACK").FirstOrDefault().Value = true.ToString();
                                                resultConfigurationItem = configApiClient.InvokeMethod(resultConfigurationItem, "ChangeSecurityPermissions");
                                                #endregion
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
                    users = JsonConvert.DeserializeObject<List<User>>(File.ReadAllText(HttpContext.Current.Server.MapPath("~/data.txt")));
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
                throw new Exception("Could not read all nessesery settings from the web.config file.");
            }

            try
            {
                //Initializing the ConfigAPIClient object
                configApiClient = new ConfigAPIClient();
                configApiClient.ServerAddress = settings.SurveillanceAddress;
                configApiClient.Serverport = settings.SurveillancePort;
                configApiClient.BasicUser = !settings.IsAdUser;
                configApiClient.Username = settings.Username;
                configApiClient.Password = settings.Password;

                configApiClient.Initialize();
            }
            catch
            {
                Debugger.Break();
                //returning error response
                throw new Exception("Could not connect to the VMS Server. Check your settings");
            }
        }
    }
}
