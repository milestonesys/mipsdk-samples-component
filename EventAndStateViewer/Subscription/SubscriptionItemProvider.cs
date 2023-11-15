using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VideoOS.Platform;
using VideoOS.Platform.JsonHandling;

namespace EventAndStateViewer.Subscription
{
    /// <summary>
    /// Class for providing the <see cref="SubscriptionRuleViewModel"/> with data modeled as <see cref="Item"/>s.
    /// </summary>
    class SubscriptionItemProvider
    {
        private readonly CachedRestApiClient _restApiClient;

        public SubscriptionItemProvider()
        {
            _restApiClient = App.DataModel.RestApiClient;
        }

        /// <summary>
        /// Get a hardcode a list of the most common event triggers (this is not an exhaustive list of supported resource types).
        /// The purpose of these items is displaying them in an Item Picker.
        /// </summary>
        public IEnumerable<Item> GetResourceTypes()
        {
            // Random id used for the recording server item to distinguish it from the management server in the Item Picker. This id is not used elsewhere.
            var recorderId = new Guid("80b7fb0a-1d71-4861-8dc9-12ac7031b00e");

            // Only the ObjectIdString is used in the subscription
            return new[]
            {
                new Item(new FQID(new ServerId() { ServerType = ServerId.CorporateManagementServerType }, Guid.Empty, Kind.Server, FolderType.No, Kind.Server) { ObjectIdString = "sites" }, "Management servers"),
                new Item(new FQID(new ServerId() { ServerType = ServerId.CorporateRecordingServerType }, Guid.Empty, recorderId, FolderType.No, Kind.Server) { ObjectIdString = "recordingServers" }, "Recording servers"),
                new Item(new FQID(new ServerId(), Guid.Empty, Kind.Hardware, FolderType.No, Kind.Hardware) { ObjectIdString = "hardware" }, "Hardware"),
                new Item(new FQID(new ServerId(), Guid.Empty, Kind.Camera, FolderType.No, Kind.Camera) { ObjectIdString = "cameras" }, "Cameras"),
                new Item(new FQID(new ServerId(), Guid.Empty, Kind.Microphone, FolderType.No, Kind.Microphone) { ObjectIdString = "microphones"}, "Microphones"),
                new Item(new FQID(new ServerId(), Guid.Empty, Kind.Speaker, FolderType.No, Kind.Speaker) { ObjectIdString = "speakers" }, "Speakers"),
                new Item(new FQID(new ServerId(), Guid.Empty, Kind.Output, FolderType.No, Kind.Output) { ObjectIdString = "outputs" }, "Outputs"),
                new Item(new FQID(new ServerId(), Guid.Empty, Kind.InputEvent, FolderType.No, Kind.InputEvent) { ObjectIdString = "inputEvents" }, "Input events"),
                new Item(new FQID(new ServerId(), Guid.Empty, Kind.TriggerEvent, FolderType.No, Kind.TriggerEvent) { ObjectIdString = "userDefinedEvents" }, "User-defined events"),
            };
        }


        /// <summary>
        /// Get all configuration items.
        /// The purpose of these items is displaying them in an Item Picker.
        /// </summary>
        public async Task<IEnumerable<Item>> GetSourcesAsync()
        {
            // Get items in a separate task as not to block the UI
            return await Task.Run(() => Configuration.Instance.GetItems());
        }


        /// <summary>
        /// Build event type items, grouped by both event type group and state group.
        /// The purpose of these items is displaying them in an Item Picker.
        /// <br/>
        /// <br/>
        /// As an alternative to querying the REST API for event types, you can also use
        /// the pre-defined event ids defined in <see cref="VideoOS.Platform.SDK.StatusClient.KnownStatusEvents"/>.
        /// This will not include hardware-specific events or user-defined events.
        /// </summary>
        public async Task<IEnumerable<Item>> GetEventTypesAsync()
        {
            var tasks = await Task.WhenAll(
                _restApiClient.LookupResourceAsync("eventTypes/"),
                _restApiClient.LookupResourceAsync("stateGroups/"),
                _restApiClient.LookupResourceAsync("eventTypeGroups/"));

            var eventTypes = tasks[0].GetChild("array").GetChildren().Select(x => ToItem(x));
            var stateGroups = tasks[1].GetChild("array").GetChildren().Select(x => ToItem(x)).ToDictionary(x => x.FQID.ObjectId);
            var eventTypeGroups = tasks[2].GetChild("array").GetChildren().Select(x => ToItem(x)).ToDictionary(x => x.FQID.ObjectId);

            foreach (var eventType in eventTypes)
            {
                var eventTypeGroup = eventTypeGroups[eventType.FQID.ParentId];
                if (Guid.TryParse(eventType.Properties["stateGroupId"], out var stateGroupId))
                {
                    // Add eventType to stateGroup and stateGroup to parent eventTypeGroup
                    var stateGroup = stateGroups[stateGroupId];
                    stateGroup.AddChild(eventType);
                    if (!eventTypeGroup.GetChildren().Contains(stateGroup))
                    {
                        eventTypeGroup.AddChild(stateGroup);
                    }
                }
                else
                {
                    // No stateGroup - just add eventType to parent eventTypeGroup
                    eventTypeGroup.AddChild(eventType);
                }
            }
            return eventTypeGroups.Values.Where(x => x.GetChildren().Any());
        }

        /// <summary>
        /// Convert JSON response from the REST API to an Item.
        /// </summary>
        private ConfigItem ToItem(JsonObject jsonObject)
        {
            Guid.TryParse(jsonObject.GetChild("relations")?.GetChild("parent")?.GetString("id"), out var parentId);
            Guid.TryParse(jsonObject.GetChild("relations")?.GetChild("self")?.GetString("id"), out var objectId);
            var folderType = parentId == Guid.Empty ? FolderType.SystemDefined : FolderType.No;
            var name = jsonObject.GetString("displayName");
            var item = new ConfigItem(new FQID(new ServerId(), parentId, objectId, folderType, Kind.TriggerEvent), name);
            if (parentId != Guid.Empty)
            {
                item.Properties.Add("stateGroupId", jsonObject.GetString("stateGroupId"));
            }
            return item;
        }


    }
}
