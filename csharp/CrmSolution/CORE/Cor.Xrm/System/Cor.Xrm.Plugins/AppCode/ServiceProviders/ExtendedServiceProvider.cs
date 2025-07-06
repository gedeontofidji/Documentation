using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.PluginTelemetry;
using System;
using System.Linq;
using Cor.Xrm.Plugins.AppCode.Exceptions;
using Cor.Xrm.Service.Base;

namespace Cor.Xrm.Plugins.AppCode.ServiceProviders
{
    /// <summary>
    /// Cette classe permet de stocker les données d'exécution du plugin pour proposer
    /// des méthodes d'aide à l'utilisation de ses propriétés
    /// </summary>
    public class ExtendedServiceProvider<T> : IDataverseServiceProvider where T : Entity
    {
        #region Variables

        /// <summary>
        /// Factory des services CRM
        /// </summary>
        private readonly IOrganizationServiceFactory serviceFactory;

        /// <summary>
        /// Dataverse service as system user
        /// </summary>
        private IOrganizationService _adminService;

        /// <summary>
        /// Dataverse service as current user
        /// </summary>
        private IOrganizationService _userService;

        #endregion Variables

        #region Constructeur

        /// <summary>
        /// Créé une nouvelle instance de la classe ExtendedServiceProvider
        /// </summary>
        /// <param name="serviceProvider">Service Provider</param>
        public ExtendedServiceProvider(IServiceProvider serviceProvider)
        {
            Context = (IPluginExecutionContext)serviceProvider.GetService(typeof(IPluginExecutionContext));

            TracingService = (ITracingService)serviceProvider.GetService(typeof(ITracingService));

            serviceFactory = (IOrganizationServiceFactory)serviceProvider.GetService(typeof(IOrganizationServiceFactory));

            Logger = (ILogger)serviceProvider.GetService(typeof(ILogger));
        }

        #endregion Constructeur

        #region Propriétés

        /// <summary>
        /// Obtient le service d'organisation pour le compte de service
        /// </summary>
        public IOrganizationService AdminService
        {
            get
            {
                if (_adminService == null)
                    _adminService = serviceFactory.CreateOrganizationService(null);

                return _adminService;
            }
        }

        /// <summary>
        /// Obtient le contexte d'exécution du plugin
        /// </summary>
        public IPluginExecutionContext Context { get; private set; }

        /// <summary>
        /// Obtient le service d'organisation pour le compte de service
        /// </summary>
        public ILogger Logger
        {
            get;
        }

        /// <summary>
        /// Obtient le service de trace
        /// </summary>
        public ITracingService TracingService { get; private set; }

        /// <summary>
        /// Obtient le service d'organisation pour l'utilisateur courant
        /// </summary>
        public IOrganizationService UserService
        {
            get
            {
                if (_userService == null)
                    _userService = serviceFactory.CreateOrganizationService(Context.UserId);

                return _userService;
            }
        }

        #endregion Propriétés

        #region Méthodes

        /// <summary>
        /// Obtient l'image de l'objet en cours (PreImage + Données modifiées)
        /// </summary>
        /// <param name="preImageName">Nom de l'image Pre (optionel uniquement en création)</param>
        /// <returns>Image de l'objet courant</returns>
        public T GetCurrentImage(string preImageName = null)
        {
            return GetCurrentImageEntity(preImageName).ToEntity<T>();
        }

        /// <summary>
        /// Obtient l'image de l'objet en cours (PreImage + Données modifiées)
        /// </summary>
        /// <param name="preImageName">Nom de l'image Pre (optionel uniquement en création)</param>
        /// <returns>Image de l'objet courant</returns>
        public Entity GetCurrentImageEntity(string preImageName = null)
        {
            if (Context.MessageName != PluginMessage.Update && Context.MessageName != PluginMessage.Create)
                throw new InvalidPluginExecutionException("L'image courante n'est disponible que pour les messages \"Update\" et \"Create\"");

            if (Context.MessageName == PluginMessage.Create)
                return GetInputParameter<Entity>(PluginInputParameters.Target);

            Entity preEntity;

            if (preImageName == null || !Context.PreEntityImages.Contains(preImageName))
            {
                if (Context.PreEntityImages.Any())
                {
                    preEntity = Context.PreEntityImages.First().Value;
                }
                else
                {
                    throw new PluginImageNotFoundException(
                    $"Le nom de l'image ne peut pas être vide et doit être déclaré dans l'enregistrement du plugin. Nom de l'image: {preImageName ?? "(null)"}");
                }
            }
            else
            {
                preEntity = Context.PreEntityImages[preImageName];
            }

            var inputData = GetInputParameter<Entity>(PluginInputParameters.Target);
            var currentImage = new Entity(preEntity.LogicalName) { Id = inputData.Id };

            // Copie des attributs de l'image Pre
            foreach (var attribute in preEntity.Attributes)
            {
                currentImage[attribute.Key] = attribute.Value;
            }

            // Copie des attriburs modifiés
            foreach (var attribute in inputData.Attributes)
            {
                currentImage[attribute.Key] = attribute.Value;
            }

            return currentImage;
        }

        /// <summary>
        /// Obtient la profondeur courante de l'exécution dans la pile d'appel
        /// </summary>
        /// <returns>Entier</returns>
        public int GetDepth()
        {
            return Context.Depth;
        }

        /// <summary>
        /// Obtient l'identifiant unique de l'utilisateur sous lequel
        /// le pipeline de plugin s'exécute
        /// </summary>
        /// <returns>Identifiant unique</returns>
        public Guid GetInitiatingUserId()
        {
            return Context.InitiatingUserId;
        }

        /// <summary>
        /// Obtient les données ayant déclenchées l'exécution du plugin
        /// </summary>
        /// <returns>Données</returns>
        public T GetInputData()
        {
            return GetInputDataEntity().ToEntity<T>();
        }

        /// <summary>
        /// Obtient les données ayant déclenchées l'exécution du plugin
        /// </summary>
        /// <returns>Données</returns>
        public Entity GetInputDataEntity()
        {
            return GetInputParameter<Entity>(PluginInputParameters.Target);
        }

        /// <summary>
        /// Obtient le paramètre d'entrée du pipeline d'exécution du plugin
        /// </summary>
        /// <param name="parameterName">Nom du paramètre</param>
        /// <returns>Valeur du paramètre d'entrée</returns>
        public TE GetInputParameter<TE>(string parameterName)
        {
            if (!Context.InputParameters.Contains(parameterName))
            {
                throw new PluginParameterNotFoundException(string.Format("Les données d'entrée du plugin ne contiennent pas le paramètre '{0}'", parameterName));
            }

            return (TE)Context.InputParameters[parameterName];
        }

        /// <summary>
        /// Obtient une valeur indiquant si le plugin s'exécute dans
        /// une sandbox
        /// </summary>
        /// <returns>Booléen</returns>
        public int GetIsolationMode()
        {
            return Context.IsolationMode;
        }

        /// <summary>
        /// Obtient le mode d'exécution du plugin (Synchrone ou asynchrone)
        /// </summary>
        /// <returns>Entier</returns>
        public int GetMode()
        {
            return Context.Mode;
        }

        /// <summary>
        /// Obtient la date de création de la tâche système assoiée
        /// </summary>
        /// <returns>DateTime</returns>
        public DateTime GetOperationCreatedOn()
        {
            return Context.OperationCreatedOn;
        }

        /// <summary>
        /// Obtient l'identifiant unique de la tâche système assoiée
        /// </summary>
        /// <returns>Guid</returns>
        public Guid GetOperationId()
        {
            return Context.OperationId;
        }

        /// <summary>
        /// Obtient l'identifiant unique de l'organisation CRM
        /// </summary>
        /// <returns>Guid</returns>
        public Guid GetOrganizationId()
        {
            return Context.OrganizationId;
        }

        /// <summary>
        /// Obtient le nom de l'organisation CRM
        /// </summary>
        /// <returns>String</returns>
        public string GetOrganizationName()
        {
            return Context.OrganizationName;
        }

        /// <summary>
        /// Obtient le service d'organisation CRM
        /// </summary>
        /// <param name="isAdmin">Indique si le service doit être instancié en tant que le compte de service</param>
        /// <returns>Service d'organisation CRM</returns>
        public IOrganizationService GetOrganizationService(bool isAdmin)
        {
            return isAdmin ? AdminService : UserService;
        }

        /// <summary>
        /// Obtient le service d'organisation CRM pour l'utilisateur spécifié
        /// </summary>
        /// <param name="userId">Identifiant unique de l'utilisateur</param>
        /// <returns>service d'organisation CRM</returns>
        public IOrganizationService GetOrganizationServiceForUser(Guid userId)
        {
            return serviceFactory.CreateOrganizationService(userId);
        }

        /// <summary>
        /// Obtient le paramètre de sortie du pipeline d'exécution du plugin
        /// </summary>
        /// <param name="parameterName">Nom du paramètre</param>
        /// <returns>Valeur du paramètre de sortie</returns>
        public TE GetOutputParameter<TE>(string parameterName)
        {
            if (!Context.OutputParameters.Contains(parameterName))
            {
                throw new PluginParameterNotFoundException(string.Format("Les données de sortie du plugin ne contiennent pas le paramètre '{0}'", parameterName));
            }

            return (TE)Context.OutputParameters[parameterName];
        }

        /// <summary>
        /// Obtient le context d'exécution parent
        /// </summary>
        /// <returns>Contexte de plugin</returns>
        public IPluginExecutionContext GetParentContext()
        {
            return Context.ParentContext;
        }

        /// <summary>
        /// Obtient l'image Post du pipeline d'exécution du plugin
        /// </summary>
        /// <param name="imageName">Nom de l'image</param>
        /// <returns>Image</returns>
        public T GetPostImage(string imageName)

        {
            return GetPostImageEntity(imageName).ToEntity<T>();
        }

        /// <summary>
        /// Obtient l'image Post du pipeline d'exécution du plugin
        /// </summary>
        /// <param name="imageName">Nom de l'image</param>
        /// <returns>Image</returns>
        public Entity GetPostImageEntity(string imageName)
        {
            if (!Context.PostEntityImages.Contains(imageName))
            {
                throw new PluginImageNotFoundException(string.Format("Il n'existe pas d'image Post avec le nom '{0}'", imageName));
            }

            return Context.PostEntityImages[imageName];
        }

        /// <summary>
        /// Obtient l'image Pre du pipeline d'exécution du plugin
        /// </summary>
        /// <param name="imageName">Nom de l'image</param>
        /// <returns>Image</returns>
        public T GetPreImage(string imageName)
        {
            return GetPreImageEntity(imageName).ToEntity<T>();
        }

        /// <summary>
        /// Obtient l'image Pre du pipeline d'exécution du plugin
        /// </summary>
        /// <param name="imageName">Nom de l'image</param>
        /// <returns>Image</returns>
        public Entity GetPreImageEntity(string imageName)
        {
            if (!Context.PreEntityImages.Contains(imageName))
            {
                throw new PluginImageNotFoundException(string.Format("Il n'existe pas d'image Pre avec le nom '{0}'", imageName));
            }

            return Context.PreEntityImages[imageName];
        }

        /// <summary>
        /// Obtient l'image Pre du pipeline d'exécution du plugin
        /// </summary>
        /// <param name="imageName">Nom de l'image</param>
        /// <returns>Image</returns>
        public T GetPreImage<T>(string imageName) where T : Entity
        {
            return GetPreImage(imageName).ToEntity<T>();
        }

        /// <summary>
        /// Obtient l'identifiant unique de l'enregistrement ayant déclenché
        /// le plugin
        /// </summary>
        /// <returns>Guid</returns>
        public Guid GetPrimaryEntityId()
        {
            return Context.PrimaryEntityId;
        }

        /// <summary>
        /// Obtient le nom d'entité de l'enregistrement ayant déclenché le plugin
        /// </summary>
        /// <returns>String</returns>
        public string GetPrimaryEntityName()
        {
            return Context.PrimaryEntityName;
        }

        /// <summary>
        /// Obtient le nom de l'entité secondaire ayant une relation avec l'entité
        /// primaire
        /// </summary>
        /// <returns>String</returns>
        public string GetSecondaryEntityName()
        {
            return Context.SecondaryEntityName;
        }

        /// <summary>
        /// Obtient les propriétés personnalisées partagées entre les plugins
        /// </summary>
        /// <returns>ParameterCollection</returns>
        public ParameterCollection GetSharedVariables()
        {
            return Context.SharedVariables;
        }

        /// <summary>
        /// Obtient l'identifiant unique de l'utilisateur auquel le traitement
        /// de ce plugin a été délégué
        /// </summary>
        /// <returns></returns>
        public Guid GetUserId()
        {
            return Context.UserId;
        }

        /// <summary>
        /// Obtient une valeur indiquant si le plugin s'exécute dans
        /// le client Outlook pendant qu'il est déconnecté
        /// </summary>
        /// <returns>Booléen</returns>
        public bool IsExecutingOffline()
        {
            return Context.IsExecutingOffline;
        }

        /// <summary>
        /// Détermine si le contexte parent provient d'un message pour une entité donnée.
        /// </summary>
        /// <param name="messageName">Nom du message</param>
        /// <param name="entity">Nom logique de l'entité</param>
        /// <param name="recursive">Indique s'il faut chercher sur plus d'un niveau de contexte parent</param>
        /// <returns>
        ///   <c>true</c> si le contexte parent vient du message et de l'entité donnée; sinon, <c>false</c>.
        /// </returns>
        public bool IsFromParentContext(string messageName, string entity = null, bool recursive = true)
        {
            var parentContext = Context.ParentContext;
            while (parentContext != null)
            {
                if (parentContext.MessageName == messageName && (entity != null && parentContext.PrimaryEntityName == entity || entity == null))
                {
                    return true;
                }

                parentContext = parentContext.ParentContext;
                if (recursive == false) break;
            }

            return false;
        }

        /// <summary>
        /// Obtient une valeur indiquant si le plugin s'exécute dans
        /// une transaction dans la base de données
        /// </summary>
        /// <returns>Booléen</returns>
        public bool IsInTransaction()
        {
            return Context.IsInTransaction;
        }

        /// <summary>
        /// Obtient une valeur indiquant si le plugin s'exécute sur
        /// le serveur pendant que le client Outlook revient en état
        /// connecté
        /// </summary>
        /// <returns>Booléen</returns>
        public bool IsOfflinePlayback()
        {
            return Context.IsOfflinePlayback;
        }

        /// <summary>
        /// Définit les données d'entrées du plugin
        /// </summary>
        /// <param name="record"></param>
        public void SetInputData(Entity record)
        {
            SetInputParameter(PluginInputParameters.Target, record);
        }

        /// <summary>
        /// Définit le paramètre d'entrée du plugin
        /// </summary>
        /// <param name="parameterName">Nom du paramètre</param>
        /// <param name="value">Valeur du paramètre</param>
        public void SetInputParameter(string parameterName, object value)
        {
            if (!Context.InputParameters.Contains(parameterName))
            {
                throw new PluginParameterNotFoundException(string.Format("Le paramètre d'entrée \"{0}\" n'existe pas!", parameterName));
            }

            Context.InputParameters[parameterName] = value;
        }

        /// <summary>
        /// Définit le paramètre de sortie du plugin
        /// </summary>
        /// <param name="parameterName">Nom du paramètre</param>
        /// <param name="value">Valeur du paramètre</param>
        public void SetOutputParameters(string parameterName, object value)
        {
            Context.OutputParameters.Add(parameterName, value);
        }

        /// <summary>
        /// Trace un message
        /// </summary>
        /// <param name="message">Message à tracer</param>
        public void Trace(string message)
        {
            TracingService?.Trace(message);
            Logger?.LogInformation(message);
        }

        /// <summary>
        /// Trace un message
        /// </summary>
        /// <param name="message">Message à tracer</param>
        public void TraceError(string message)
        {
            TracingService?.Trace(message);
            Logger?.LogError(message);
        }

        /// <summary>
        /// Trace un message
        /// </summary>
        /// <param name="message">Message à tracer</param>
        public void TraceWarning(string message)
        {
            TracingService?.Trace(message);
            Logger?.LogWarning(message);
        }

        #endregion Méthodes
    }
}