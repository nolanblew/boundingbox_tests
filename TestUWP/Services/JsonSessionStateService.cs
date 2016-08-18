using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Windows.ApplicationModel.Resources;
using Windows.Security.Cryptography.DataProtection;
using Windows.Storage;
using Windows.UI.Xaml;
using Newtonsoft.Json;
using Prism.Windows.AppModel;
using Prism.Windows.Navigation;

namespace TestUWP.Services
{
    public class JsonSessionStateService : ISessionStateService
    {
        const string _sessionStateFileName = "_sessionState.json";

        const string _infrastructureResourceMapId = "/Prism.Windows/Resources/";

        const string _navigationStateName = "Navigation";

        const string _dataProtectionDescriptor = "LOCAL=user";

        readonly List<Type> _knownTypes = new List<Type>();

        public Dictionary<string, object> SessionState { get; private set; } = new Dictionary<string, object>();

        JsonSerializer _jsonSerializer = new JsonSerializer
        {
            PreserveReferencesHandling = PreserveReferencesHandling.All,
            ReferenceLoopHandling = ReferenceLoopHandling.Serialize,
            ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor,
            TypeNameHandling = TypeNameHandling.Auto,
            MissingMemberHandling = MissingMemberHandling.Error,
        };

        static DependencyProperty FrameSessionStateKeyProperty = DependencyProperty.RegisterAttached(
            "_FrameSessionStateKey",
            typeof(String),
            typeof(JsonSessionStateService),
            null);

        static DependencyProperty FrameSessionStateProperty = DependencyProperty.RegisterAttached(
            "_FrameSessionState",
            typeof(Dictionary<String, Object>),
            typeof(JsonSessionStateService),
            null);

        static List<WeakReference<IFrameFacade>> _registeredFrames = new List<WeakReference<IFrameFacade>>();

        public void RegisterKnownType(Type type)
        {
            if (!_knownTypes.Contains(type)) { _knownTypes.Add(type); }
        }

        public async Task SaveAsync()
        {
            try
            {
                foreach (var weakFrameReference in _registeredFrames)
                {
                    IFrameFacade frame;
                    if (weakFrameReference.TryGetTarget(out frame))
                    {
                        _SaveFrameNavigationState(frame);
                    }
                }

                using (var sessionData = new MemoryStream())
                using (var jsonTextWriter = new JsonTextWriter(new StreamWriter(sessionData)))
                {
                    _jsonSerializer.Serialize(jsonTextWriter, SessionState);
                    jsonTextWriter.Flush();

                    StorageFile file = await ApplicationData.Current.LocalFolder.CreateFileAsync(
                        _sessionStateFileName,
                        CreationCollisionOption.ReplaceExisting);
                    using (var fileStream = await file.OpenAsync(FileAccessMode.ReadWrite))
                    {
                        sessionData.Seek(0, SeekOrigin.Begin);
                        var provider = new DataProtectionProvider(_dataProtectionDescriptor);

                        await provider.ProtectStreamAsync(sessionData.AsInputStream(), fileStream);
                        await fileStream.FlushAsync();
                    }
                }
            }
            catch (Exception e)
            {
                throw new SessionStateServiceException(e);
            }
        }

        public async Task RestoreSessionStateAsync()
        {
            SessionState.Clear();
            try
            {
                StorageFile file = await ApplicationData.Current.LocalFolder.GetFileAsync(_sessionStateFileName);
                using (var inStream = await file.OpenSequentialReadAsync())
                using (var memoryStream = new MemoryStream())
                {
                    var provider = new DataProtectionProvider(_dataProtectionDescriptor);

                    await provider.UnprotectStreamAsync(inStream, memoryStream.AsOutputStream());
                    memoryStream.Seek(0, SeekOrigin.Begin);

                    using (var reader = new JsonTextReader(new StreamReader(memoryStream)))
                    {
                        SessionState = _jsonSerializer.Deserialize<Dictionary<string, object>>(reader);
                    }
                }
            }
            catch (Exception e)
            {
                throw new SessionStateServiceException(e);
            }
        }

        public void RestoreFrameState()
        {
            try
            {
                foreach (var weakFrameReference in _registeredFrames)
                {
                    IFrameFacade frame;
                    if (weakFrameReference.TryGetTarget(out frame))
                    {
                        frame.ClearValue(FrameSessionStateProperty);
                        _RestoreFrameNavigationState(frame);
                    }
                }
            }
            catch (Exception e)
            {
                throw new SessionStateServiceException(e);
            }
        }

        public void RegisterFrame(IFrameFacade frame, String sessionStateKey)
        {
            if (frame == null) { throw new ArgumentNullException(nameof(frame)); }

            var resourceLoader = ResourceLoader.GetForCurrentView(_infrastructureResourceMapId);
            if (frame.GetValue(FrameSessionStateKeyProperty) != null)
            {
                var errorString = resourceLoader.GetString("FrameAlreadyRegisteredWithKey");
                throw new InvalidOperationException(errorString);
            }

            if (frame.GetValue(FrameSessionStateProperty) != null)
            {
                var errorString = resourceLoader.GetString("FrameRegistrationRequirement");
                throw new InvalidOperationException(errorString);
            }

            frame.SetValue(FrameSessionStateKeyProperty, sessionStateKey);
            _registeredFrames.Add(new WeakReference<IFrameFacade>(frame));
            _RestoreFrameNavigationState(frame);
        }

        public void UnregisterFrame(IFrameFacade frame)
        {
            SessionState.Remove((string)frame.GetValue(FrameSessionStateKeyProperty));
            _registeredFrames.RemoveAll(
                (weakFrameReference) =>
                {
                    IFrameFacade testFrame;
                    return !weakFrameReference.TryGetTarget(out testFrame) || testFrame == frame;
                });
        }

        public Dictionary<String, Object> GetSessionStateForFrame(IFrameFacade frame)
        {
            if (frame == null) { throw new ArgumentNullException(nameof(frame)); }

            var frameState = (Dictionary<String, Object>)frame.GetValue(FrameSessionStateProperty);
            if (frameState == null)
            {
                var frameSessionKey = (String)frame.GetValue(FrameSessionStateKeyProperty);
                if (frameSessionKey != null)
                {
                    if (!SessionState.ContainsKey(frameSessionKey))
                    {
                        SessionState[frameSessionKey] = new Dictionary<String, Object>();
                    }
                    frameState = (Dictionary<String, Object>)SessionState[frameSessionKey];
                }
                else
                {
                    frameState = new Dictionary<String, Object>();
                }
                frame.SetValue(FrameSessionStateProperty, frameState);
            }
            return frameState;
        }

        void _RestoreFrameNavigationState(IFrameFacade frame)
        {
            var frameState = GetSessionStateForFrame(frame);
            if (frameState.ContainsKey(_navigationStateName))
            {
                frame.SetNavigationState((string)frameState[_navigationStateName]);
            }
        }

        void _SaveFrameNavigationState(IFrameFacade frame)
        {
            var frameState = GetSessionStateForFrame(frame);
            frameState[_navigationStateName] = frame.GetNavigationState();
        }
    }
}
